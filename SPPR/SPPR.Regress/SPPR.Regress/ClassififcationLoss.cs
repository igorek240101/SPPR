using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinarRegres
{
    internal class ClassififcationLoss : ILoss
    {
        public string Name => "Loss";

        public float GetLoss(float[][] calcRes, float[] trueValue)
        {

            float res = 0;
            for (int i = 0; i < calcRes.Length; i++)
            {
                for (int j = 0; j < calcRes[i].Length; j++)
                {
                    if (j == trueValue[0] - 1)
                        res += (float)Math.Pow(calcRes[i][j] - 1, 2);
                    else
                        res += (float)Math.Pow(calcRes[i][j], 2);
                }
            }
            return res;
        }
    }
}
