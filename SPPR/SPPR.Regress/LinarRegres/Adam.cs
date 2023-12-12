using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPR.Abstract;


namespace LinarRegres
{
    internal class Adam : IOptimazer
    {
        public string Name => "Adam";

        public float[][] Optimazer(Func<float[][], float[], float[]> calc, float[][] model,
                                    float[][] input, float[] output, float n, float l, float k, ILoss lossFunc, IRegularization regularization)
        {
            float y = 0.9f;
            float alpha = 0.999f;
            float e = 0.0000001f;
            float[][] v = new float[model.Length][];
            float[][] G = new float[model.Length][];
            for(int i = 0; i < model.Length; i++)
            {
                v[i] = new float[model[i].Length];
                G[i] = new float[model[i].Length];
            }
            for (int a = 0; a < input.Length; a++)
            {
                float[] resValue = calc(model, input[a]);
                float loss = lossFunc.GetLoss(new float[][] { resValue }, new float[] { output[a] }) + l * regularization.Regular(model);
                for (int i = 0; i < model.Length; i++)
                {
                    for (int j = 0; j < model[i].Length; j++)
                    {
                        float bufer = model[i][j];
                        k = n * (float)Math.Pow(alpha, a + 1);
                        model[i][j] += k;
                        var add = lossFunc.GetLoss(new float[][] { calc(model, input[a]) }, new float[] { output[a] }) + l * regularization.Regular(model);
                        float dif = (add - loss) / k;
                        v[i][j] = y * v[i][j] + (1 - y) * dif;
                        G[i][j] = alpha * G[i][j] + (1 - alpha) * dif * dif;
                        float dv = v[i][j] / (1 - (float)Math.Pow(y, a + 1));
                        float dG = G[i][j] / (1 - (float)Math.Pow(alpha, a + 1));
                        model[i][j] = bufer;
                        model[i][j] -= n * dv / ((float)Math.Sqrt(dG) + e);
                        loss = lossFunc.GetLoss(new float[][] { calc(model, input[a]) }, new float[] { output[a] }) + l * regularization.Regular(model);
                    }
                }
            }
            return model;
        }
    }
}
