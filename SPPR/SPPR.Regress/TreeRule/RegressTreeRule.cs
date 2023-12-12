using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace TreeRule
{
    internal class RegressTreeRule : ITreeRegressRule
    {
        public string Name => "Регрессия";

        public (Func<object[], int>, List<(object[], float)[]>, string) CreateRule((object[], float)[] input)
        {
            float avg = input.Average(t => t.Item2);
            float sko = (float)input.Average(t => Math.Pow(avg - t.Item2, 2));
            double[] informationGain = new double[input[0].Item1.Length];
            Dictionary<object, Dictionary<int, int>>[] classesInProperty = new Dictionary<object, Dictionary<int, int>>[informationGain.Length];
            for (int i = 0; i < informationGain.Length; i++)
            {
                classesInProperty[i] = new Dictionary<object, Dictionary<int, int>>();
                if (input[0].Item1[i] is double)
                    GetNumberEntropy(input, i, sko, informationGain);
                else
                    GetCategrySKO(input, i, sko, informationGain);
            }
            double maxInformationGain = informationGain.Max();
            int index = informationGain.ToList().IndexOf(maxInformationGain);

            if (maxInformationGain == 0) return (null, null, null);
            if (input[0].Item1[index] is double)
                return GetNumberRes(input, index, sko);
            else
                return GetCategryRes(input, index);
        }


        internal static void GetCategrySKO((object[], float)[] input, int i, double parentE, double[] informationGain)
        {
            informationGain[i] = parentE;
            var values = input.ToList().ConvertAll(t => t.Item1[i]).ToHashSet().ToArray();
            for (int j = 0; j < values.Length; j++)
            {
                var trueSet = input.Where(t => t.Item1[i].Equals(values[j]));
                if (trueSet.Count() == input.Length)
                {
                    informationGain[i] = 0;
                    continue;
                }
                float trueAvg = trueSet.Average(t => t.Item2);
                float trueSko = (float)trueSet.Average(t => Math.Pow(trueAvg - t.Item2, 2));
                informationGain[i] -= trueSko / trueSet.Count();
            }
        }

        internal static (Func<object[], int>, List<(object[], float)[]>, string) GetCategryRes((object[], float)[] input, int index)
        {
            double? minEntropy = null;
            object res = null;
            var values = input.ToList().ConvertAll(t => t.Item1[index]).ToHashSet().ToArray();
            for (int i = 0; i < values.Length; i++)
            {
                var trueSet = input.Where(t => t.Item1[index].Equals(values[i]));
                float trueAvg = trueSet.Average(t => t.Item2);
                float trueSko = (float)trueSet.Average(t => Math.Pow(trueAvg - t.Item2, 2));
                var falseSet = input.Where(t => !t.Item1[index].Equals(values[i]));
                float falseAvg = falseSet.Average(t => t.Item2);
                float falseSko = (float)falseSet.Average(t => Math.Pow(falseAvg - t.Item2, 2));
                float entropy = trueSko / trueSet.Count() + falseSko / falseSet.Count();
                if (!minEntropy.HasValue || minEntropy > entropy)
                {
                    minEntropy = entropy;
                    res = values[i];
                }
            }

            List<(object[], float)[]> sets = new List<(object[], float)[]>
            {
                input.Where(t => t.Item1[index].Equals(res)).ToArray(),
                input.Where(t => !t.Item1[index].Equals(res)).ToArray()
            };
            return (t => t[index] == res ? 0 : 1, sets, $"t[{index}] == {res}");
        }

        internal static (Func<object[], int>, List<(object[], float)[]>, string) GetSwitchClassRes((object[], float)[] input, int index)
        {
            List<(object[], float)[]> sets = new List<(object[], float)[]>();
            var values = input.ToList().ConvertAll(t => t.Item1[index]).ToHashSet().ToArray();
            for (int i = 0; i < values.Length; i++)
            {
                sets.Add(input.Where(t => t.Item1[index].Equals(values[i])).ToArray());
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



        internal static void GetNumberEntropy((object[], float)[] input, int i, double parentE, double[] informationGain)
        {
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
                var centerArray = (new List<(object[], float)>(), new List<(object[], float)>());
                var leftArray = (new List<(object[], float)>(), new List<(object[], float)>());
                var rightArray = (new List<(object[], float)>(), new List<(object[], float)>());

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
                }

                float centerAvgLeft = centerArray.Item1.Average(t => t.Item2);
                float centerAvgRight = centerArray.Item2.Average(t => t.Item2);
                float leftAvgLeft = leftArray.Item1.Average(t => t.Item2);
                float leftAvgRight = leftArray.Item2.Average(t => t.Item2);
                float rightAvgLeft = rightArray.Item1.Average(t => t.Item2);
                float rightAvgRight = rightArray.Item2.Average(t => t.Item2);

                float centerSKOLeft = (float)centerArray.Item1.Average(t => Math.Pow(centerAvgLeft - t.Item2, 2));
                float centerSKORight = (float)centerArray.Item1.Average(t => Math.Pow(centerAvgLeft - t.Item2, 2));
                float leftSKOLeft = (float)leftArray.Item1.Average(t => Math.Pow(leftAvgLeft - t.Item2, 2));
                float leftSKORight = (float)leftArray.Item1.Average(t => Math.Pow(leftAvgLeft - t.Item2, 2));
                float rightSKOLeft = (float)rightArray.Item1.Average(t => Math.Pow(rightAvgLeft - t.Item2, 2));
                float rightSKORight = (float)rightArray.Item1.Average(t => Math.Pow(rightAvgLeft - t.Item2, 2));

                double cInformationGain = parentE - (centerSKOLeft / centerArray.Item1.Count + centerSKORight / centerArray.Item2.Count);
                double lInformationGain = parentE - (leftSKOLeft / leftArray.Item1.Count + leftSKORight / leftArray.Item2.Count); ;
                double rInformationGain = parentE - (rightSKOLeft / rightArray.Item1.Count + rightSKORight / rightArray.Item2.Count);

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

        internal static (Func<object[], int>, List<(object[], float)[]>, string) GetNumberRes(
            (object[], float)[] input,
            int index,
            double parentE)
        {
            double left = input.Min(t => (double)t.Item1[index]);
            double right = input.Max(t => (double)t.Item1[index]);

            (List<(object[], float)>, List<(object[], float)>) centerArray;
            double center;

            while (true)
            {
                center = (left + right) / 2;
                double lCenter = (left + center) / 2;
                double rCenter = (center + right) / 2;
                centerArray = (new List<(object[], float)>(), new List<(object[], float)>());
                var leftArray = (new List<(object[], float)>(), new List<(object[], float)>());
                var rightArray = (new List<(object[], float)>(), new List<(object[], float)>());

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
                }

                float centerAvgLeft = centerArray.Item1.Average(t => t.Item2);
                float centerAvgRight = centerArray.Item2.Average(t => t.Item2);
                float leftAvgLeft = leftArray.Item1.Average(t => t.Item2);
                float leftAvgRight = leftArray.Item2.Average(t => t.Item2);
                float rightAvgLeft = rightArray.Item1.Average(t => t.Item2);
                float rightAvgRight = rightArray.Item2.Average(t => t.Item2);

                float centerSKOLeft = (float)centerArray.Item1.Average(t => Math.Pow(centerAvgLeft - t.Item2, 2));
                float centerSKORight = (float)centerArray.Item1.Average(t => Math.Pow(centerAvgLeft - t.Item2, 2));
                float leftSKOLeft = (float)leftArray.Item1.Average(t => Math.Pow(leftAvgLeft - t.Item2, 2));
                float leftSKORight = (float)leftArray.Item1.Average(t => Math.Pow(leftAvgLeft - t.Item2, 2));
                float rightSKOLeft = (float)rightArray.Item1.Average(t => Math.Pow(rightAvgLeft - t.Item2, 2));
                float rightSKORight = (float)rightArray.Item1.Average(t => Math.Pow(rightAvgLeft - t.Item2, 2));

                double cInformationGain = parentE - (centerSKOLeft / centerArray.Item1.Count + centerSKORight / centerArray.Item2.Count);
                double lInformationGain = parentE - (leftSKOLeft / leftArray.Item1.Count + leftSKORight / leftArray.Item2.Count); ;
                double rInformationGain = parentE - (rightSKOLeft / rightArray.Item1.Count + rightSKORight / rightArray.Item2.Count);


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

            return (t => (double)t[index] < center ? 0 : 1, new List<(object[], float)[]>() { centerArray.Item1.ToArray(), centerArray.Item2.ToArray() }, $"t[{index}] < {center}");
        }
    }
}
