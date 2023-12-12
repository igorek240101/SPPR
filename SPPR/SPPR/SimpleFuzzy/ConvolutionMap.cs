using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR
{
    public abstract class Map
    {
        protected int inputDimension;
        public float[] Output { get; protected set; }

        public Func<float, float> Activation;

        public Map(Func<float, float> activation) 
        {
            Activation = activation;
        }

        public abstract void Calc(float[] w, float[] input);

        protected static int[] VectorSum(int[] v1, int[] v2)
        {
            int[] res = new int[v1.Length];
            for (int i = 0; i < v1.Length; i++)
            {
                res[i] = v1[i] + v2[i];
            }
            return res;
        }

        protected static int[] VectorMul(int[] v1, int q)
        {
            int[] res = new int[v1.Length];
            for (int i = 0; i < v1.Length; i++)
            {
                res[i] = v1[i] * q;
            }
            return res;
        }

        protected static int ToIndex(int[] vector, int max)
        {
            if (vector.Any(t => t < 0)) return -1;
            int res = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                res += vector[i] * (int)Math.Pow(max, i);
            }
            return res;
        }

        protected static int[] ToVector(int index, int dim, int len)
        {
            int[] vector = new int[dim];
            for (int i = 0; i < dim && index > 0; i++)
            {
                vector[i] = index % len;
                index /= len;
            }
            return vector;
        }
    }

    public class ConvolutionMap : Map
    {
        public ConvolutionMap(int inputDimension, Func<float, float> activation) : base(activation)
        {
            this.inputDimension = inputDimension;
        }

        public override void Calc(float[] w, float[] input)
        {
            int count = (int)Math.Round(Math.Pow(w.Length - 1, 1.0 / inputDimension));
            int inputCount = (int)Math.Round(Math.Pow(input.Length, 1.0 / inputDimension));
            float[] output = new float[input.Length];
            for (int i = 0; i < output.Length; i++)
            {
                float res = w[^1];
                for (int j = 0; j < w.Length - 1; j++)
                {
                    int[] vector = ToVector(j, inputDimension, count).ToList().ConvertAll(t => t - ((count - 1) / 2)).ToArray();
                    int[] vectorInput = ToVector(i, inputDimension, inputCount);
                    int index = ToIndex(VectorSum(vector, vectorInput), inputCount);
                    res += w[j] * ((index >= 0 && index < output.Length) ? input[index] : 0);
                }
                output[i] = res;
            }
            Output = output;
        }
    }

    public class PullingMap : Map
    {
        Func<float[], float> agregate;
        public PullingMap(int inputDimension, Func<float[], float> agregate) : base(t => t)
        {
            this.inputDimension = inputDimension;
            this.agregate = agregate;
        }

        public override void Calc(float[] w, float[] input)
        {
            int count = 0;
            for (int i = 2; i <= input.Length; i++)
            {
                if (input.Length % i == 0)
                {
                    count = i;
                    break;
                }
            }
            int len = (int)Math.Round(Math.Pow(input.Length, 1.0 / inputDimension)) / count;
            float[] res = new float[input.Length / (int)Math.Pow(count, inputDimension)];
            for (int i = 0; i < res.Length; i++)
            {
                float[] filter = new float[(int)Math.Pow(count, inputDimension)];
                int[] vector = VectorMul(ToVector(i, inputDimension, len), count);
                for (int j = 0; j < filter.Length; j++)
                {
                    int[] filterVector = VectorSum(vector, ToVector(j, inputDimension, count));
                    int index = ToIndex(filterVector, count * len);
                    filter[j] = input[index];
                }
                res[i] = agregate(filter);
            }
            Output = res;
        }
    }
}
