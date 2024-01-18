using SPPR.Abstract;

namespace LinarRegres
{
    internal class CryptoLoader : IGetClassificationSet
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "Крипта";

        List<float[]> main = new List<float[]>();

        Random random = new Random();

        public CryptoLoader()
        {
            Reload();
        }

        public string ClassFazififcation(double _class)
        {
            throw new NotImplementedException();
        }

        public float[,] GetTestSet(int count)
        {
            float[,] res = new float[count, main[0].Length];
            for (int i = 0; i < count && main.Count > 0; i++)
            {
                for (int j = 0; j < main[0].Length; j++)
                {
                    res[i, j] = main[0][j];
                }
                main.RemoveAt(0);
            }
            return res;
        }

        public float[,] GetTestSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            if (main.Count == 0) return new float[0, 0];
            float[,] res = new float[(int)(main.Count * p), main[0].Length];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < main[0].Length; j++)
                {
                    res[i, j] = main[0][j];
                }
                main.RemoveAt(0);
            }
            return res;
        }

        public float[,] GetTrainSet(int count)
        {
            float[,] res = new float[count, main[0].Length];
            for (int i = 0; i < count && main.Count > 0; i++)
            {
                for (int j = 0; j < main[0].Length; j++)
                {
                    res[i, j] = main[0][j];
                }
                main.RemoveAt(0);
            }
            return res;
        }

        public float[,] GetTrainSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            if (main.Count == 0) return new float[0, 0];
            float[,] res = new float[(int)(main.Count * p), main[0].Length];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < main[0].Length; j++)
                {
                    res[i, j] = main[0][j];
                }
                main.RemoveAt(0);
            }
            return res;
        }

        public void Reload()
        {
            List<float[]> simple = new List<float[]>();
            Dictionary<string, float> modelCode = new Dictionary<string, float>();
            FileStream file = new FileStream($"D:\\СГМ\\Лабораторные работы\\crypto-markets.csv", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            reader.ReadLine();
            while (!reader.EndOfStream || simple.Count < 942297)
            {
                var v = reader.ReadLine().Split(',').ToList();
                v = v.ConvertAll(x => x.Replace('.', ','));
                simple.Add(new float[]
                {
                    float.Parse(v[8]),
                    DateTime.Parse(v[3]).Ticks - new DateTime(2010, 01, 01).Ticks,
                    float.Parse(v[5]),
                    float.Parse(v[6]),
                    float.Parse(v[7]),
                    float.Parse(v[9]),
                    float.Parse(v[10]),
                    float.Parse(v[11]),
                    float.Parse(v[12])
                });
            }
            reader.Close();
            file.Close();
            for (int i = 0; i < 9; i++)
            {
                float min = simple.Min(t => t[i]);
                float max = simple.Max(t => t[i]);
                for (int j = 0; j < simple.Count; j++)
                {
                    simple[j][i] = (simple[j][i] - min) / (max - min);
                }
            }
            main = new List<float[]>();
            while (simple.Count > 0)
            {
                int index = random.Next(simple.Count);
                main.Add(simple[index]);
                simple.RemoveAt(index);
            }
        }
    }
}
