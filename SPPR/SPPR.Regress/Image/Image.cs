using SPPR.Abstract;
using System.Drawing;

namespace Image
{
    public class Image : IGetClassificationSet
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "Простое изображение";

        List<float[]> main = new List<float[]>();

        Random random = new Random();

        public Image()
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
            Bitmap image = System.Drawing.Image.FromFile("simple.png") as Bitmap;
            List<float[]> simple = new List<float[]>();
            simple.Add(new float[401]);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                     simple[^1][i * 20 + j + 1] = (255 - image.GetPixel(j, i).R) / 255;
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