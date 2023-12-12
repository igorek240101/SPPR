using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinarRegres
{
    internal class L2 : IRegularization
    {
        public string Name => "По Тихонову";

        public float Regular(float[][] model)
        {
            float res = 0f;
            foreach (var item in model)
            {
                foreach (var item2 in item)
                    res += item2 * item2;
            }
            return res;
        }
    }
}
