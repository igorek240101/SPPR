using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SPPR.Wine
{
    internal class WineDataSetObject : IGetClassificationSetObject
    {
        public string ConnctionString { get => "https://archive.ics.uci.edu/ml/machine-learning-databases/wine/wine.data"; set { } }

        public string Name => "Вина для классификации - дерево";

        List<object[]> main = new List<object[]>();

        Random random = new Random();

        public WineDataSetObject()
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

        public object[,] GetTestSet(int count)
        {
            object[,] res = new object[count, 14];
            for (int i = 0; i < count && main.Count > 0; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    res[i, j] = main[0][j];
                }
                main.RemoveAt(0);
            }
            return res;
        }

        public object[,] GetTestSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            object[,] res = new object[(int)(main.Count * p), 14];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    res[i, j] = main[0][j];
                }
                main.RemoveAt(0);
            }
            return res;
        }

        public object[,] GetTrainSet(int count)
        {
            object[,] res = new object[count, 14];
            for (int i = 0; i < count && main.Count > 0; i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    res[i, j] = main[0][j];
                }
                main.RemoveAt(0);
            }
            return res;
        }

        public object[,] GetTrainSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            object[,] res = new object[(int)(main.Count * p), 14];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    res[i, j] = main[0][j];
                }
                main.RemoveAt(0);
            }
            return res;
        }

        public void Reload()
        {
            main = new List<object[]>();
            WebRequest req = WebRequest.CreateHttp(ConnctionString);
            req.ContentType = "text/html";
            req.Method = "GET";
            WebResponse resp = req.GetResponse();
            List<object[]> simple = new List<object[]>();
            using (var streamWriter = new StreamReader(resp.GetResponseStream()))
            {
                string[] res = streamWriter.ReadToEnd().Split('\n');
                foreach (var value in res)
                {
                    if (value != string.Empty)
                    {
                        string[] wine = value.Split(",");
                        object[] array = new object[wine.Length];
                        array[0] = int.Parse(wine[0].Replace('.', ','));
                        for(int i = 1; i < wine.Length; i++)
                        {
                            array[i] = double.Parse(wine[i].Replace('.', ','));
                        }
                        simple.Add(array);
                    }
                }
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
