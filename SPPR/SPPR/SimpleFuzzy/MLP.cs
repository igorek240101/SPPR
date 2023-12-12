using Accord.Math.Optimization;
using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR
{
    public class MLP
    {
        public Neron[][] levels;
        public float[][] W { get; set; }
        public MLP(int[] counts, Func<float, float> activation) 
        {
            levels = new Neron[counts.Length - 1][];
            levels[0] = new Neron[counts[1]];
            for (int i = 0; i < levels[0].Length; i++) 
            {
                levels[0][i] = new Neron(activation);
            }
            for (int i = 1; i < counts.Length - 2; i++)
            {
                levels[i] = new Neron[counts[i + 1]];
                for (int j = 0; j < counts[i + 1]; j++)
                {
                    levels[i][j] = new Neron(levels[i - 1], activation);
                }
            }
            levels[^1] = new Neron[counts[^1]];
            for (int i = 0; i < levels[^1].Length; i++)
            {
                levels[^1][i] = new Neron(levels[^2], t => t);
            }
            W = new float[levels.Sum(t => t.Length)][];
            Random random = new Random();
            int count = 0;
            for (int i = 0; i < levels.Length; i++)
            {
                for (int j = 0; j < levels[i].Length; j++)
                {
                    if (i == 0)
                    {
                        W[count] = new float[counts[0]];
                        for (int k = 0; k < counts[0]; k++)
                            W[count][k] = (float)random.NextDouble();
                    }
                    else
                    {
                        W[count] = new float[levels[i][j].InputCount];
                        for (int k = 0; k < W[count].Length; k++)
                            W[count][k] = (float)random.NextDouble();
                    }
                    count++;
                }
            }
        }

        public float[] Calc(float[][] w, float[] input)
        {
            int count = 0;
            for (int i = 0; i < levels[0].Length; i++)
            {
                levels[0][i].CalcNeron(w[count], input);
                count++;
            }
            for (int i = 1; i < levels.Length; i++)
            {
                for (int j = 0; j < levels[i].Length; j++)
                {
                    levels[i][j].CalcNeron(w[count]);
                    count++;
                }
            }
            float[] res = new float[levels[^1].Length];
            for (int i = 0; i < res.Length; i ++)
            {
                res[i] = levels[^1][i].Output;
            }
            return res;
        }
    }
}
