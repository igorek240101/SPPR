namespace SPPR
{
    public class CNN
    {
        public float[][] w;
        public float[][] W
        {
            get
            {
                return w.Concat(MLP.W).ToArray();
            }
            set
            {
                List<float[]> w = new List<float[]>();
                List<float[]> mlp = new List<float[]>();
                for (int i = 0; i < mlpStart && i < value.Length; i++)
                {
                    w.Add(value[i]);
                }
                for (int i = mlpStart; i < value.Length; i++)
                {
                    mlp.Add(value[i]);
                }
                this.w = w.ToArray();
                MLP.W = mlp.ToArray();
            }
        }

        // 1-й уровень - слой
        // 2-й уровень - фильтр
        // 3-й уровень - ядро (каждый канал отдельно)
        public Map[][][] Maps;

        public Neron[][] levels;

        public MLP MLP { get; private set; }

        int mlpStart;

        int ChannelsCount;

        public CNN(CNNInput[] input, int[] counts, int channelsCount, int dimension, Func<float, float> activation)
        {
            ChannelsCount = channelsCount;
            Random random = new Random();
            List<float[]> w = new List<float[]>();
            Maps = new Map[input.Length][][];
            int? lastConvolation = null;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] is CInput cInput)
                {
                    Maps[i] = new Map[cInput.FilterCount][];
                    for (int j = 0; j < cInput.FilterCount; j++)
                    {
                        Maps[i][j] = new Map[!lastConvolation.HasValue ? ChannelsCount : Maps[lastConvolation.Value].Length];
                        for (int k = 0; k < Maps[i][j].Length; k++)
                        {
                            Maps[i][j][k] = new ConvolutionMap(dimension, cInput.activation);
                            w.Add(new float[(int)Math.Pow(cInput.KernelDimension * 2 + 1, dimension) + 1]);
                            for (int a = 0; a < w[^1].Length; a++)
                            {
                                w[^1][a] = (float)random.NextDouble() * 2 - 1;
                            }
                        }
                    }
                    lastConvolation = i;
                }
                else if (input[i] is RInput rInput && i != 0)
                {
                    Maps[i] = new Map[1][];
                    Maps[i][0] = new Map[Maps[i - 1].Length];
                    for (int k = 0; k < Maps[i - 1].Length; k++)
                    {
                        Maps[i][0][k] = new PullingMap(dimension, rInput.agregate);
                        w.Add(new float[] { });
                    }
                }
            }
            mlpStart = w.Count;
            MLP = new MLP(counts, activation);
            this.w = w.ToArray();
        }

        public float[] Calc(float[][] w, float[] input)
        {
            int count = 0;
            float[][] stepInput = new float[ChannelsCount][];
            for (int i = 0; i < stepInput.Length; i++)
            {
                stepInput[i] = new float[input.Length / ChannelsCount];
                for (int j = 0; j < stepInput[i].Length; j++)
                {
                    stepInput[i][j] = input[i * (input.Length / ChannelsCount) + j];
                }
            }
            for (int i = 0; i < Maps.Length; i++)
            {
                for (int j = 0; j < Maps[i].Length; j++)
                {
                    for (int k = 0; k < Maps[i][j].Length; k++, count++)
                    {   
                        Maps[i][j][k].Calc(w[count], stepInput[k]);
                    }
                }
                if (Maps[i][0][0].GetType() == typeof(ConvolutionMap))
                    stepInput = Maps[i].ToList().ConvertAll(
                            v => MatrixSum(v)).ToArray();
                else if (Maps[i][0][0].GetType() == typeof(PullingMap))
                    stepInput = Maps[i][0].ToList().ConvertAll(t => t.Output).ToArray();
            }
            float[] mlpInput = new float[stepInput.Sum(t => t.Length)];
            for (int i = 0, j = 0; j < stepInput.Length; j++)
            {
                for (int k = 0; k < stepInput[j].Length; k++, i++)
                {
                    mlpInput[i] = stepInput[j][k];
                }
            }
            return MLP.Calc(MLP.W, mlpInput);
        }

        private float[] MatrixSum(Map[] matrix)
        {
            float[] res = new float[matrix[0].Output.Length];
            for (int i = 0; i < res.Length; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    res[i] += matrix[j].Output[i];
                }
                res[i] = matrix[0].Activation(res[i]);
            }
            return res;
        }

        public abstract class CNNInput
        {

        }

        public class CInput : CNNInput
        {
            public int ConvolutionStep { get; set; } // Шаг свртки
            public int KernelDimension { get; set; } // Размерность ядра
            public int FilterCount { get; set; } // Количество фильтров
            //public int ChannelsCount { get; set; } // Количество каналов

            public Func<float, float> activation;

        }

        public class RInput : CNNInput
        {
            public Func<float[], float> agregate;
        }
    }
}
