using SPPR.Abstract;

namespace LinarRegres
{
    internal class SimpleRegress : IGetClassificationSet
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "Простой датасет";

        List<float[]> main = new List<float[]>();

        Random random = new Random();

        public SimpleRegress()
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
            for (int i = 0; i < 10000; i++)
            {
                simple.Add(new float[]
                {
                    ((float)Math.Sin((i / 1000f)) + 1) / 2, i / 10000f
                });
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
