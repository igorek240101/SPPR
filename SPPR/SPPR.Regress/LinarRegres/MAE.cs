using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinarRegres
{
    internal class MAE : ILoss
    {
        public string Name => "MAE";

        public float GetLoss(float[][] calcRes, float[] trueValue)
        {
            float res = 0;
            for (int i = 0; i < calcRes.Length; i++)
            {
                res += (float)Math.Abs(calcRes[i][0] - trueValue[i]);
            }
            return res / calcRes.Length;
        }
    }
}
