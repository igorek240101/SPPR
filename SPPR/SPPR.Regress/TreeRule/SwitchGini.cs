using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRule
{
    internal class SwitchGini : ITreeRule
    {
        public string Name => "Не бинарное Gini";

        public (Func<object[], int>, List<(object[], int)[]>, string) CreateRule((object[], int)[] input)
        {
            double[] mainGini = new double[input[0].Item1.Length];
            Dictionary<object, Dictionary<int, int>>[] classesInProperty = new Dictionary<object, Dictionary<int, int>>[mainGini.Length];
            for (int i = 0; i < mainGini.Length; i++)
            {
                classesInProperty[i] = new Dictionary<object, Dictionary<int, int>>();
                if (input[0].Item1[i] is double)
                    Gini.GetNumberGini(input, i, classesInProperty, 0, mainGini);
                else
                    Gini.GetClassGini(input, i, classesInProperty, mainGini);
            }
            double maxInformationGain = mainGini.Min();
            int index = mainGini.ToList().IndexOf(maxInformationGain);

            if (maxInformationGain >= 0.5) return (null, null, null);
            if (input[0].Item1[index] is double)
                return Gini.GetNumberRes(input, index, classesInProperty);
            else
                return Entropy.GetSwitchClassRes(input, index, classesInProperty);
        }
    }
}
