using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPR.Abstract;


namespace LinarRegres
{
    internal class SGD : IOptimazer
    {
        public string Name => "SGD";

        public float[][] Optimazer(Func<float[][], float[], float[]> calc, float[][] model,
                                    float[][] input, float[] output, float n, float l, float k, ILoss lossFunc, IRegularization regularization)
        {
            for (int a = 0; a < input.Length; a++)
            {
                float[] resValue = calc(model, input[a]);
                float loss = lossFunc.GetLoss(new float[][] { resValue }, new float[] { output[a] }) + l * regularization.Regular(model);
                for (int i = 0; i < model.Length; i++)
                {
                    for (int j = 0; j < model[i].Length; j++)
                    {
                        float bufer = model[i][j];
                        model[i][j] += k;
                        var add = lossFunc.GetLoss(new float[][] { calc(model, input[a]) }, new float[] { output[a] }) + l * regularization.Regular(model);
                        float dif = add - loss;
                        model[i][j] = bufer;
                        model[i][j] -= n * Math.Sign(dif);
                        loss = lossFunc.GetLoss(new float[][] { calc(model, input[a]) }, new float[] { output[a] }) + l * regularization.Regular(model);
                    }
                }
            }
            return model;
        }
    }
}
