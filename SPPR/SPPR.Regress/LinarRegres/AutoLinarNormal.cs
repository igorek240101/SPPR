using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LinarRegres
{
    internal class AutoLinarNormal : IGetClassificationSet
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "Автомобили, нормал численные";

        List<float[]> main = new List<float[]>();

        Random random = new Random();

        public AutoLinarNormal()
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
            string[] fileNames = { "audi", "bmw", "ford", "hyundi", "merc", "skoda", "toyota", "vauxhall", "vw" };
            List<float[]> simple = new List<float[]>();
            Dictionary<string, float> modelCode = new Dictionary<string, float>();
            FileStream modelCodeFile = new FileStream($"Auto\\ModelCode.txt", FileMode.Open);
            StreamReader readerCodeFile = new StreamReader(modelCodeFile);
            while(!readerCodeFile.EndOfStream)
            {
                var value = readerCodeFile.ReadLine().Split('%');
                modelCode.Add(value[0], float.Parse(value[1]));
            }
            readerCodeFile.Close();
            modelCodeFile.Close();
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
                    simple.Add(new float[]
                    {
                        float.Parse(v[0].Replace('.', ',')), // Цена
                        modelCode[v[1]], // Модель
                        DateTime.Now.Year - float.Parse(v[2]), // Возраст
                        v[3] switch
                        {
                            "Manual" => 12112.063991496147f,
                            "Automatic" => 21558.217790187475f,
                            "Semi-Auto" => 24284.031309256075f,
                            "Other" => 16219.111111111111f
                        }, // КПП
                        float.Parse(v[4].Replace('.', ',')), // Пробег
                        v[5] switch
                        {
                            "Diesel" => 19339.488516419078f,
                            "Petrol" => 14775.045659772793f,
                            "Hybrid" => 19289.586094866798f,
                            "Other" => 17443.344129554655f,
                            "Electric" => 16645.333333333332f
                        }, // Тип топлива
                        float.Parse(v[6].Replace('.', ',')), // Дорожный налог
                        float.Parse(v[7].Replace('.', ',')), // Расход топлива
                        float.Parse(v[8].Replace('.', ',')) // Объем двигателя
                    });
                }
                reader.Close();
                file.Close();
            }
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
