using SPPR.Abstract;

namespace TreeRule
{
    public class Gini : ITreeRule
    {
        public string Name => "Gini";

        public (Func<object[], int>, List<(object[], int)[]>, string) CreateRule((object[], int)[] input)
        {
            double[] mainGini = new double[input[0].Item1.Length];
            Dictionary<object, Dictionary<int, int>>[] classesInProperty = new Dictionary<object, Dictionary<int, int>>[mainGini.Length];
            for (int i = 0; i < mainGini.Length; i++)
            {
                classesInProperty[i] = new Dictionary<object, Dictionary<int, int>>();
                if (input[0].Item1[i] is double)
                    GetNumberGini(input, i, classesInProperty, 0, mainGini);
                else
                    GetClassGini(input, i, classesInProperty, mainGini);
            }
            double maxInformationGain = mainGini.Min();
            int index = mainGini.ToList().IndexOf(maxInformationGain);

            if (maxInformationGain >= 0.5) return (null, null, null);
            if (input[0].Item1[index] is double)
                return GetNumberRes(input, index, classesInProperty);
            else
                return GetClassRes(input, index, classesInProperty);
        }

        internal static void GetClassGini(
            (object[], int)[] input,
            int i,
            Dictionary<object, Dictionary<int, int>>[] classesInProperty,
            double[] mainGini)
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
            mainGini[i] = 0;
            for (int j = 0; j < onlyValue.Length; j++)
            {
                double localGini = 1;
                foreach (var item in onlyValue[j].Values)
                {
                    localGini -= Math.Pow(((double)item) / onlyValue[j].Sum(t => t.Value), 2);
                }
                mainGini[i] += localGini * onlyValue[j].Sum(t => t.Value) / input.Length;
            }
        }

        internal static (Func<object[], int>, List<(object[], int)[]>, string) GetClassRes(
            (object[], int)[] input,
            int index,
            Dictionary<object, Dictionary<int, int>>[] classesInProperty)
        {
            var onlyValue = classesInProperty[index].Values.ToArray();
            double? minGini = null;
            object res = null;
            for (int j = 0; j < onlyValue.Length; j++)
            {
                double localGini = 1;
                foreach (var item in onlyValue[j].Values)
                {
                    localGini -= Math.Pow(((double)item) / onlyValue[j].Sum(t => t.Value), 2);
                }
                if (!minGini.HasValue || minGini > localGini)
                {
                    minGini = localGini;
                    res = classesInProperty[index].Keys.ElementAt(j);
                }
            }

            List<(object[], int)[]> sets = new List<(object[], int)[]>
            {
                input.Where(t => t.Item1[index].Equals(res)).ToArray(),
                input.Where(t => !t.Item1[index].Equals(res)).ToArray()
            };
            return (t => t[index] == res ? 0 : 1, sets, $"t[{index}] == {res}");
        }

        internal static void GetNumberGini((object[], int)[] input,
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
                informationGain[i] = 0.5;
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

                var classesList = classes.ToList();

                double cInformationGainF = 1;
                double lInformationGainF = 1;
                double rInformationGainF = 1;
                double cInformationGainS = 1;
                double lInformationGainS = 1;
                double rInformationGainS = 1;

                for (int j = 0; j < classesList.Count; j++)
                {
                    cInformationGainF -= Math.Pow((double)centerArray.Item1.Count(t => t.Item2 == classesList[j]) / centerArray.Item1.Count, 2);
                    lInformationGainF -= Math.Pow((double)leftArray.Item1.Count(t => t.Item2 == classesList[j]) / leftArray.Item1.Count, 2);
                    rInformationGainF -= Math.Pow((double)rightArray.Item1.Count(t => t.Item2 == classesList[j]) / rightArray.Item1.Count, 2);
                    cInformationGainS -= Math.Pow((double)centerArray.Item2.Count(t => t.Item2 == classesList[j]) / centerArray.Item2.Count, 2);
                    lInformationGainS -= Math.Pow((double)leftArray.Item2.Count(t => t.Item2 == classesList[j]) / leftArray.Item2.Count, 2);
                    rInformationGainS -= Math.Pow((double)rightArray.Item2.Count(t => t.Item2 == classesList[j]) / rightArray.Item2.Count, 2);
                }

                double cInformationGain = cInformationGainF * centerArray.Item1.Count / input.Length + cInformationGainS * centerArray.Item2.Count / input.Length;
                double lInformationGain = lInformationGainF * centerArray.Item1.Count / input.Length + lInformationGainS * centerArray.Item2.Count / input.Length;
                double rInformationGain = rInformationGainF * centerArray.Item1.Count / input.Length + rInformationGainS * centerArray.Item2.Count / input.Length;

                if (cInformationGain <= lInformationGain && cInformationGain <= rInformationGain)
                {
                    informationGain[i] = cInformationGain;
                    break;
                }
                else if (lInformationGain <= rInformationGain)
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
            Dictionary<object, Dictionary<int, int>>[] classesInProperty)
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

                var classesList = classes.ToList();

                double cInformationGainF = 1;
                double lInformationGainF = 1;
                double rInformationGainF = 1;
                double cInformationGainS = 1;
                double lInformationGainS = 1;
                double rInformationGainS = 1;

                for (int j = 0; j < classesList.Count; j++)
                {
                    cInformationGainF -= Math.Pow((double)centerArray.Item1.Count(t => t.Item2 == classesList[j]) / centerArray.Item1.Count, 2);
                    lInformationGainF -= Math.Pow((double)leftArray.Item1.Count(t => t.Item2 == classesList[j]) / leftArray.Item1.Count, 2);
                    rInformationGainF -= Math.Pow((double)rightArray.Item1.Count(t => t.Item2 == classesList[j]) / rightArray.Item1.Count, 2);
                    cInformationGainS -= Math.Pow((double)centerArray.Item2.Count(t => t.Item2 == classesList[j]) / centerArray.Item2.Count, 2);
                    lInformationGainS -= Math.Pow((double)leftArray.Item2.Count(t => t.Item2 == classesList[j]) / leftArray.Item2.Count, 2);
                    rInformationGainS -= Math.Pow((double)rightArray.Item2.Count(t => t.Item2 == classesList[j]) / rightArray.Item2.Count, 2);
                }

                double cInformationGain = cInformationGainF * centerArray.Item1.Count / input.Length + cInformationGainS * centerArray.Item2.Count / input.Length;
                double lInformationGain = lInformationGainF * centerArray.Item1.Count / input.Length + lInformationGainS * centerArray.Item2.Count / input.Length;
                double rInformationGain = rInformationGainF * centerArray.Item1.Count / input.Length + rInformationGainS * centerArray.Item2.Count / input.Length;

                if (cInformationGain <= lInformationGain && cInformationGain <= rInformationGain)
                {
                    break;
                }
                else if (lInformationGain <= rInformationGain)
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