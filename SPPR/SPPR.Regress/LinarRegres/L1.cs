using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinarRegres
{
    internal class L1 : IRegularization
    {
        public string Name => "Лассо";

        public float Regular(float[][] model)
        {
            float res = 0f;
            foreach (var item in model)
            {
                foreach (var item2 in item)
                    res += Math.Abs(item2);
            }
            return res;
        }
    }
}
