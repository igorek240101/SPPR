using SPPR.Abstract;

namespace LinarRegres
{
    public class Auto : IGetClassificationSetObject
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "Автомобили";


        List<object[]> main = new List<object[]>();

        Random random = new Random();


        public Auto()
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
            string[] fileNames = { "audi", "bmw", "ford", "hyundi", "merc", "skoda", "toyota", "vauxhall", "vw" };
            List<object[]> simple = new List<object[]>();
            foreach (var name in fileNames)
            {
                FileStream file = new FileStream($"Auto\\{name}.csv", FileMode.Open);
                StreamReader reader = new StreamReader(file);
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var v = reader.ReadLine().Split(',').ToList();
                    v[0] = name + v[0];
                    v.Insert(0, v[2]);
                    v.RemoveAt(3);
                    simple.Add(new object[] 
                    {
                        double.Parse(v[0].Replace('.', ',')), // Цена
                        v[1], // Модель
                        v[2], // Год
                        DateTime.Now.Year - double.Parse(v[2]), // Возраст
                        v[3], // КПП
                        double.Parse(v[4].Replace('.', ',')), // Пробег
                        v[5], // Тип топлива
                        double.Parse(v[6].Replace('.', ',')), // Дорожный налог
                        double.Parse(v[7].Replace('.', ',')), // Расход топлива
                        double.Parse(v[8].Replace('.', ',')) // Объем двигателя
                    });
                }
                reader.Close();
                file.Close();
            }
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