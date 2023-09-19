using SPPR.Abstract;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Text.Json;

namespace SPPR.Wine
{
    public class WineDataSet : IGetClassificationSet
    {
        public string ConnctionString { get => "https://archive.ics.uci.edu/ml/machine-learning-databases/wine/wine.data"; set { } }

        public string Name => "Вина для классификации";

        List<float[]> main = new List<float[]>();

        Random random = new Random();

        public WineDataSet() 
        {
            Reload();
        }

        public string ClassFazififcation(double _class)
        {
            return _class switch
            {
                1 => "Сорт1",
                2 => "Сорт2",
                3 => "Сорт3"
            };
        }

        public float[,] GetTestSet(int count)
        {
            float[,] res = new float[count, 3];
            for (int i = 0; i < count && main.Count > 0; i++)
            {
                res[i, 0] = main[0][0];
                res[i, 1] = main[0][1];
                res[i, 2] = main[0][2];
                main.RemoveAt(0);
            }
            return res;
        }

        public float[,] GetTestSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            float[,] res = new float[(int)(main.Count * p), 3];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                res[i, 0] = main[0][0];
                res[i, 1] = main[0][1];
                res[i, 2] = main[0][2];
                main.RemoveAt(0);
            }
            return res;
        }

        public float[,] GetTrainSet(int count)
        {
            float[,] res = new float[count, 3];
            for (int i = 0; i < count && main.Count > 0; i++)
            {
                res[i, 0] = main[0][0];
                res[i, 1] = main[0][1];
                res[i, 2] = main[0][2];
                main.RemoveAt(0);
            }
            return res;
        }

        public float[,] GetTrainSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            float[,] res = new float[(int)(main.Count * p), 3];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                res[i, 0] = main[0][0];
                res[i, 1] = main[0][1];
                res[i, 2] = main[0][2];
                main.RemoveAt(0);
            }
            return res;
        }

        public void Reload()
        {
            main = new List<float[]>();
            WebRequest req = WebRequest.CreateHttp(ConnctionString);
            req.ContentType = "text/html";
            req.Method = "GET";
            WebResponse resp = req.GetResponse();
            List<float[]> simple = new List<float[]>();
            using (var streamWriter = new StreamReader(resp.GetResponseStream()))
            {
                string[] res = streamWriter.ReadToEnd().Split('\n');
                foreach (var value in res)
                {
                    if (value != string.Empty)
                    {
                        string[] wine = value.Split(",");
                        simple.Add(new float[] { float.Parse(wine[0].Replace('.', ',')), float.Parse(wine[1].Replace('.', ',')), float.Parse(wine[2].Replace('.', ',')) });
                    }
                }
            }
            while (simple.Count > 0)
            {
                int index = random.Next(simple.Count);
                main.Add(simple[index]);
                simple.RemoveAt(index);
            }
            for (int i = 1; i < 3; i++)
            {
                float min = main.Min(t => t[i]);
                float max = main.Max(t => t[i]);
                for (int j = 0; j < main.Count; j++)
                {
                    main[j][i] = (main[j][i] - min) / (max - min);
                }
            }
        }
    }
}