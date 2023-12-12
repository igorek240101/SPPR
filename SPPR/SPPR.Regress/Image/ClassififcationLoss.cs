using SPPR.Abstract;

namespace LinarRegres
{
    internal class ClassififcationLoss : ILoss
    {
        public string Name => "Loss";

        public float GetLoss(float[][] calcRes, float[] trueValue)
        {
            float? min = null, max = null;
            for (int i = 0; i < calcRes.Length; i++)
            {
                for (int j = 0; j < calcRes[i].Length; j++)
                {
                    if (min == null || min > calcRes[i][j])
                        min = calcRes[i][j];
                    if (max == null || max < calcRes[i][j])
                        max = calcRes[i][j];
                }
            }
            float res = 0;
            for (int i = 0; i < calcRes.Length; i++)
            {
                for (int j = 0; j < calcRes[i].Length; j++)
                {
                    if (j == trueValue[0] - 1)
                        res += (float)Math.Pow((calcRes[i][j] - min.Value) / (max.Value - min.Value) - 1, 2);
                    else
                        res += (float)Math.Pow((calcRes[i][j] - min.Value) / (max.Value - min.Value), 2);
                }
            }
            return res;
        }
    }
}
