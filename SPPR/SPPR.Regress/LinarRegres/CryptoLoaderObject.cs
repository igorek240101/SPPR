using SPPR.Abstract;

namespace LinarRegres
{
    internal class CryptoLoaderObject : IGetClassificationSetObject
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "Крипта - объект";


        List<object[]> main = new List<object[]>();

        Random random = new Random();


        public CryptoLoaderObject()
        {
            Reload();
        }

        public string ClassFazififcation(double _class)
        {
            throw new NotImplementedException();
        }

        public object[,] GetTestSet(int count)
        {
            object[,] res = new object[count, main[0].Length];
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

        public object[,] GetTestSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            if (main.Count == 0) return new object[0, 0];
            object[,] res = new object[(int)(main.Count * p), main[0].Length];
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

        public object[,] GetTrainSet(int count)
        {
            object[,] res = new object[count, main[0].Length];
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

        public object[,] GetTrainSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            if (main.Count == 0) return new object[0, 0];
            object[,] res = new object[(int)(main.Count * p), main[0].Length];
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
            List<object[]> simple = new List<object[]>();
            Dictionary<string, float> modelCode = new Dictionary<string, float>();
            FileStream file = new FileStream($"D:\\СГМ\\Лабораторные работы\\crypto-markets.csv", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            reader.ReadLine();
            while (!reader.EndOfStream || simple.Count < 942297)
            {
                var v = reader.ReadLine().Split(',').ToList();
                v = v.ConvertAll(x => x.Replace('.', ','));
                simple.Add(new object[]
                {
                    double.Parse(v[8]),
                    //v[0],
                    (double)(DateTime.Parse(v[3]).Ticks - new DateTime(2010, 01, 01).Ticks),
                    double.Parse(v[5]),
                    double.Parse(v[6]),
                    double.Parse(v[7]),
                    double.Parse(v[9]),
                    double.Parse(v[10]),
                    double.Parse(v[11]),
                    double.Parse(v[12])
                });
            }
            reader.Close();
            file.Close();
            main = new List<object[]>();
            while (simple.Count > 0)
            {
                int index = random.Next(simple.Count);
                main.Add(simple[index]);
                simple.RemoveAt(index);
            }
        }
    }
}
