using SPPR.Abstract;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Image
{
    public class MNIST : IGetClassificationSet
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "MNIST";

        List<float[]> main = new List<float[]>();

        Random random = new Random();

        public MNIST()
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
            main = new List<float[]>();
            List<float[]> simple = new List<float[]>();
            string[] files = new string[] { "mnist_train.csv", "mnist_test.csv" };
            for (int i = 0; i < files.Length; i++)
            {
                FileStream file = new FileStream(files[i], FileMode.Open);
                StreamReader reader = new StreamReader(file);
                var sv = reader.ReadToEnd().Split("\r\n").ToList();
                sv.RemoveAt(0);
                sv.RemoveAt(sv.Count - 1);
                simple.AddRange(sv.ConvertAll(t => t.Split(',').ToList().ConvertAll(v => float.Parse(v)).ToArray()));
                reader.Close();
                file.Close();
            }
            for (int i = 0; i < simple.Count; i++)
            {
                simple[i][0]++;
                for (int j = 1; j < simple[i].Length; j++)
                    simple[i][j] /= 255;
            }
            while (simple.Count > 0)
            {
                int index = random.Next(simple.Count);
                main.Add(simple[index]);
                simple.RemoveAt(index);
            }
        }

    }
}