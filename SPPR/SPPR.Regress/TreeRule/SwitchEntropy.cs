using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRule
{
    internal class SwitchEntropy : ITreeRule
    {
        public string Name => "Не бинарная энтропия";

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
                    Entropy.GetNumberEntropy(input, i, classesInProperty, parentE, informationGain);
                else
                    Entropy.GetClassEntropy(input, i, classesInProperty, parentE, informationGain);
            }
            double maxInformationGain = informationGain.Max();
            int index = informationGain.ToList().IndexOf(maxInformationGain);

            if (maxInformationGain == 0) return (null, null, null);
            if (input[0].Item1[index] is double)
                return Entropy.GetNumberRes(input, index, classesInProperty, parentE);
            else
                return Entropy.GetSwitchClassRes(input, index, classesInProperty);
        }
    }
}
