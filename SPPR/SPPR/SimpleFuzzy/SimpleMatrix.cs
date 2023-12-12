using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR
{
    internal static class SimpleMatrix
    {
        public static float[,] T(float[,] input)
        {
            float[,] res = new float[input.GetLength(1), input.GetLength(0)];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    res[i, j] = input[j, i];
                }
            }
            return res;
        }

        public static float[,] Mul(float[,] f, float[,] m)
        {
            if (f.GetLength(1) != m.GetLength(0))
                return null;
            float[,] res = new float[f.GetLength(0), m.GetLength(1)];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    for (int k = 0; k < f.GetLength(1); k++)
                    {
                        res[i, j] += f[i, k] * m[k, j];
                    }
                }
            }
            return res;
        }

        public static float[,] Minor(float[,] input, int i, int j)
        {
            float[,] res = new float[input.GetLength(0) - 1, input.GetLength(1) - 1];
            for (int row = 0; row < res.GetLength(0); row++)
            {
                for (int collumn = 0; collumn < res.GetLength(1); collumn++)
                {
                    if (row >= i)
                        if (collumn >= j)
                            res[row, collumn] = input[row + 1, collumn + 1];
                        else
                            res[row, collumn] = input[row + 1, collumn];
                    else
                        if (collumn >= j)
                            res[row, collumn] = input[row, collumn + 1];
                        else
                            res[row, collumn] = input[row, collumn];
                }
            }
            return res;
        }

        public static double Det(float[,] input)
        {
            double res = 1;
            var matrix = Triangle(input);
            for (int i = 0; i < matrix.GetLength(0); i++)
                res *= matrix[i, i];
            return res;
        }

        public static float[,] Reverse(float[,] input)
        {
            double det = Det(input);
            if (det == 0) return null;
            else
            {
                float[,] minors = new float[input.GetLength(0), input.GetLength(1)];
                for (int i = 0; i < input.GetLength(0); i++)
                {
                    for (int j = 0; j < input.GetLength(1); j++)
                    {
                        minors[i, j] = (float)(Det(Minor(input, i, j)) * (i % 2 == j % 2 ? 1 : -1) * (1 / det));
                    }
                }
                return T(minors);
            }
        }

        public static double[,] Triangle(float[,] input)
        {
            double[,] res = new double[input.GetLength(0), input.GetLength(1)];
            for (int i = 0; i < input.GetLength(0); i++)
                for (int j = 0; j < input.GetLength(1); j++)
                    res[i, j] = input[i, j];
            for(int j = 0; j < input.GetLength(1) - 1; j++)
            {
                for (int i = j + 1; i < input.GetLength(0); i++)
                {
                    double mult = res[i, j] / res[j, j];
                    for (int k = 0; k < input.GetLength(1); k++)
                    {
                        res[i, k] -= res[j, k] * mult;
                    }
                }
            }
            return res;
        }
    }
}
