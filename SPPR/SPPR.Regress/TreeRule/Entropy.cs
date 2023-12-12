using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TreeRule
{
    internal class Entropy : ITreeRule
    {
        public string Name => "Энтропия";

        public (Func<object[], int>, List<(object[], int)[]>, string) CreateRule((object[], int)[] input)
        {
            Dictionary<int, int> classes = new Dictionary<int, int>();
            foreach (var item in input)
            {
                if (classes.ContainsKey(item.Item2))
                {
                    classes[item.Item2]++;
                }
                else
                {
                    classes.Add(item.Item2, 1);
                }
            }
            double parentE = 0;
            foreach (var item in classes.Values)
            {
                parentE += -(((double)item) / input.Length * Math.Log2(((double)item) / input.Length));
            }
            double[] informationGain = new double[input[0].Item1.Length];
            Dictionary<object, Dictionary<int, int>>[] classesInProperty = new Dictionary<object, Dictionary<int, int>>[informationGain.Length];
            for (int i = 0; i < informationGain.Length; i++)
            {
                classesInProperty[i] = new Dictionary<object, Dictionary<int, int>>();
                if (input[0].Item1[i] is double)
                    GetNumberEntropy(input, i, classesInProperty, parentE, informationGain);
                else
                    GetClassEntropy(input, i, classesInProperty, parentE, informationGain);
            }
            double maxInformationGain = informationGain.Max();
            int index = informationGain.ToList().IndexOf(maxInformationGain);

            if (maxInformationGain == 0) return (null, null, null);
            if (input[0].Item1[index] is double)
                return GetNumberRes(input, index, classesInProperty, parentE);
            else
                return GetClassRes(input, index, classesInProperty);
        }


        internal static void GetClassEntropy(
            (object[], int)[] input,
            int i,
            Dictionary<object, Dictionary<int, int>>[] classesInProperty,
            double parentE,
            double[] informationGain)
        {
            foreach (var item in input)
            {
                if (classesInProperty[i].Any(t => t.Key.Equals(item.Item1[i])))
                {
                    var list = classesInProperty[i][classesInProperty[i].FirstOrDefault(t => t.Key.Equals(item.Item1[i])).Key];
                    if (list.ContainsKey(item.Item2))
                    {
                        list[item.Item2]++;
                    }
                    else
                    {
                        list.Add(item.Item2, 1);
                    }
                }
                else
                {
                    classesInProperty[i].Add(item.Item1[i], new Dictionary<int, int>() { { item.Item2, 1 } });
                }
            }
            var onlyValue = classesInProperty[i].Values.ToArray();
            informationGain[i] = parentE;
            for (int j = 0; j < onlyValue.Length; j++)
            {
                double entropy = 0;
                int count = onlyValue[j].Sum(t => t.Value);
                foreach (var item in onlyValue[j].Values)
                {
                    entropy += -(((double)item) / count * Math.Log2(((double)item) / count));
                }
                entropy *= ((double)count) / input.Length;
                informationGain[i] -= entropy;
            }
        }

        internal static (Func<object[], int>, List<(object[], int)[]>, string) GetClassRes(
            (object[], int)[] input,
            int index,
            Dictionary<object, Dictionary<int, int>>[] classesInProperty)
        {
            double? minEntropy = null;
            object res = null;
            var onlyRes = classesInProperty[index].Values.ToArray();
            for (int i = 0; i < classesInProperty[index].Count; i++)
            {
                double entropy = 0;
                int count = onlyRes[i].Sum(t => t.Value);
                foreach (var item in onlyRes[i].Values)
                {
                    entropy += -(((double)item) / count * Math.Log2(((double)item) / count));
                }
                if (!minEntropy.HasValue || minEntropy > entropy)
                {
                    minEntropy = entropy;
                    res = classesInProperty[index].Keys.ElementAt(i);
                }
            }

            List<(object[], int)[]> sets = new List<(object[], int)[]>
            {
                input.Where(t => t.Item1[index].Equals(res)).ToArray(),
                input.Where(t => !t.Item1[index].Equals(res)).ToArray()
            };
            return (t => t[index] == res ? 0 : 1, sets, $"t[{index}] == {res}");
        }

        internal static (Func<object[], int>, List<(object[], int)[]>, string) GetSwitchClassRes(
            (object[], int)[] input,
            int index,
            Dictionary<object, Dictionary<int, int>>[] classesInProperty)
        {
            List<(object[], int)[]> sets = new List<(object[], int)[]>();
            for (int i = 0; i < classesInProperty[index].Count; i++)
            {
                sets.Add(input.Where(t => t.Item1[index].Equals(classesInProperty[index].ToArray()[i].Key)).ToArray());
            }
            return (t => Switch(t), sets, $"switch {index}");

            int Switch(object[] input)
            {
                var v = sets.ConvertAll(b => b[0].Item1[index]);
                for (int i = 0; i < v.Count; i++)
                {
                    if (v[i].Equals(input[index]))
                    {
                        return i;
                    }
                }
                return 0;
            }
        }



        internal static void GetNumberEntropy((object[], int)[] input,
            int i,
            Dictionary<object, Dictionary<int, int>>[] classesInProperty,
            double parentE,
            double[] informationGain)
        {
            HashSet<int> classes = new HashSet<int>();
            double left = input.Min(t => (double)t.Item1[i]);
            double right = input.Max(t => (double)t.Item1[i]);

            if (left >= right)
            {
                informationGain[i] = 0;
                return;
            }

            while (true)
            {
                double center = (left + right) / 2;
                double lCenter = (left + center) / 2;
                double rCenter = (center + right) / 2;
                (List<(object[], int)>, List<(object[], int)>) centerArray = (new List<(object[], int)>(), new List<(object[], int)>());
                (List<(object[], int)>, List<(object[], int)>) leftArray = (new List<(object[], int)>(), new List<(object[], int)>());
                (List<(object[], int)>, List<(object[], int)>) rightArray = (new List<(object[], int)>(), new List<(object[], int)>());

                foreach (var item in input)
                {
                    if ((double)item.Item1[i] >= center)
                        centerArray.Item2.Add(item);
                    else centerArray.Item1.Add(item);
                    if ((double)item.Item1[i] >= lCenter)
                        leftArray.Item2.Add(item);
                    else leftArray.Item1.Add(item);
                    if ((double)item.Item1[i] >= rCenter)
                        rightArray.Item2.Add(item);
                    else rightArray.Item1.Add(item);
                    classes.Add(item.Item2);
                }

                double cInformationGain = parentE;
                double lInformationGain = parentE;
                double rInformationGain = parentE;

                foreach (var item in classes)
                {
                    var t1 = (double)centerArray.Item1.Count(t => t.Item2 == item) / centerArray.Item1.Count;
                    if (t1 != 0) t1 = t1 * Math.Log2(t1);
                    var t2 = (double)centerArray.Item2.Count(t => t.Item2 == item) / centerArray.Item2.Count;
                    if (t2 != 0) t2 = t2 * Math.Log2(t2);
                    double cEntropy = -(t1 * ((double)centerArray.Item1.Count) / input.Length +
                                      t2 * ((double)centerArray.Item2.Count) / input.Length);
                    t1 = (double)leftArray.Item1.Count(t => t.Item2 == item) / leftArray.Item1.Count;
                    if (t1 != 0) t1 = t1 * Math.Log2(t1);
                    t2 = (double)leftArray.Item2.Count(t => t.Item2 == item) / leftArray.Item2.Count;
                    if (t2 != 0) t2 = t2 * Math.Log2(t2);
                    double lEntropy = -(t1 * ((double)leftArray.Item1.Count) / input.Length +
                                      t2 * ((double)leftArray.Item2.Count) / input.Length);
                    t1 = (double)rightArray.Item1.Count(t => t.Item2 == item) / rightArray.Item1.Count;
                    if (t1 != 0) t1 = t1 * Math.Log2(t1);
                    t2 = (double)rightArray.Item2.Count(t => t.Item2 == item) / rightArray.Item2.Count;
                    if (t2 != 0) t2 = t2 * Math.Log2(t2);
                    double rEntropy = -(t1 * ((double)rightArray.Item1.Count) / input.Length +
                                      t2 * ((double)rightArray.Item2.Count) / input.Length);
                    cInformationGain -= cEntropy;
                    lInformationGain -= lEntropy;
                    rInformationGain -= rEntropy;
                }
                if (cInformationGain >= lInformationGain && cInformationGain >= rInformationGain)
                {
                    informationGain[i] = cInformationGain;
                    break;
                }
                else if (lInformationGain >= rInformationGain)
                {
                    right = center;
                }
                else
                {
                    left = center;
                }
            }
        }

        internal static (Func<object[], int>, List<(object[], int)[]>, string) GetNumberRes(
            (object[], int)[] input,
            int index,
            Dictionary<object, Dictionary<int, int>>[] classesInProperty,
            double parentE)
        {
            HashSet<int> classes = new HashSet<int>();
            double left = input.Min(t => (double)t.Item1[index]);
            double right = input.Max(t => (double)t.Item1[index]);

            (List<(object[], int)>, List<(object[], int)>) centerArray;
            double center;

            while (true)
            {
                center = (left + right) / 2;
                double lCenter = (left + center) / 2;
                double rCenter = (center + right) / 2;
                centerArray = (new List<(object[], int)>(), new List<(object[], int)>());
                (List<(object[], int)>, List<(object[], int)>) leftArray = (new List<(object[], int)>(), new List<(object[], int)>());
                (List<(object[], int)>, List<(object[], int)>) rightArray = (new List<(object[], int)>(), new List<(object[], int)>());

                foreach (var item in input)
                {
                    if ((double)item.Item1[index] >= center)
                        centerArray.Item2.Add(item);
                    else centerArray.Item1.Add(item);
                    if ((double)item.Item1[index] >= lCenter)
                        leftArray.Item2.Add(item);
                    else leftArray.Item1.Add(item);
                    if ((double)item.Item1[index] >= rCenter)
                        rightArray.Item2.Add(item);
                    else rightArray.Item1.Add(item);
                    classes.Add(item.Item2);
                }

                double cInformationGain = parentE;
                double lInformationGain = parentE;
                double rInformationGain = parentE;

                foreach (var item in classes)
                {
                    var t1 = (double)centerArray.Item1.Count(t => t.Item2 == item) / centerArray.Item1.Count;
                    if (t1 != 0) t1 = t1 * Math.Log2(t1);
                    var t2 = (double)centerArray.Item2.Count(t => t.Item2 == item) / centerArray.Item2.Count;
                    if (t2 != 0) t2 = t2 * Math.Log2(t2);
                    double cEntropy = -(t1 * ((double)centerArray.Item1.Count) / input.Length +
                                      t2 * ((double)centerArray.Item2.Count) / input.Length);
                    t1 = (double)leftArray.Item1.Count(t => t.Item2 == item) / leftArray.Item1.Count;
                    if (t1 != 0) t1 = t1 * Math.Log2(t1);
                    t2 = (double)leftArray.Item2.Count(t => t.Item2 == item) / leftArray.Item2.Count;
                    if (t2 != 0) t2 = t2 * Math.Log2(t2);
                    double lEntropy = -(t1 * ((double)(leftArray.Item1.Count)) / input.Length +
                                      t2 * ((double)leftArray.Item2.Count) / input.Length);
                    t1 = (double)rightArray.Item1.Count(t => t.Item2 == item) / rightArray.Item1.Count;
                    if (t1 != 0) t1 = t1 * Math.Log2(t1);
                    t2 = (double)rightArray.Item2.Count(t => t.Item2 == item) / rightArray.Item2.Count;
                    if (t2 != 0) t2 = t2 * Math.Log2(t2);
                    double rEntropy = -(t1 * ((double)rightArray.Item1.Count) / input.Length +
                                      t2 * ((double)rightArray.Item2.Count) / input.Length);
                    cInformationGain -= cEntropy;
                    lInformationGain -= lEntropy;
                    rInformationGain -= rEntropy;
                }
                if (cInformationGain >= lInformationGain && cInformationGain >= rInformationGain)
                {
                    break;
                }
                else if (lInformationGain >= rInformationGain)
                {
                    right = center;
                }
                else
                {
                    left = center;
                }
            }

            return (t => (double)t[index] < center ? 0 : 1, new List<(object[], int)[]>() { centerArray.Item1.ToArray(), centerArray.Item2.ToArray() }, $"t[{index}] < {center}");
        }
    }
}
