using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TreeRule
{
    internal class SimpleInput : IGetClassificationSetObject
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "Тест";

        List<object[]> main = new List<object[]>();

        Random random = new Random();

        public SimpleInput()
        {
            Reload();
        }

        public string ClassFazififcation(double _class)
        {
            return _class switch
            {
                1 => "Pass",
                2 => "Fail",
            };
        }

        public object[,] GetTestSet(int count)
        {
            object[,] res = new object[count, 4];
            for (int i = 0; i < count && main.Count > 0; i++)
            {
                res[i, 0] = main[0][0];
                res[i, 1] = main[0][1];
                res[i, 2] = main[0][2];
                res[i, 3] = main[0][3];
                main.RemoveAt(0);
            }
            return res;
        }

        public object[,] GetTestSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            object[,] res = new object[(int)(main.Count * p), 4];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                res[i, 0] = main[0][0];
                res[i, 1] = main[0][1];
                res[i, 2] = main[0][2];
                res[i, 3] = main[0][3];
                main.RemoveAt(0);
            }
            return res;
        }

        public object[,] GetTrainSet(int count)
        {
            object[,] res = new object[count, 4];
            for (int i = 0; i < count && main.Count > 0; i++)
            {
                res[i, 0] = main[0][0];
                res[i, 1] = main[0][1];
                res[i, 2] = main[0][2];
                res[i, 3] = main[0][3];
                main.RemoveAt(0);
            }
            return res;
        }

        public object[,] GetTrainSet(double? p)
        {
            if (p > 1) p = 1;
            if (p < 0) p = 0;
            object[,] res = new object[(int)(main.Count * p), 4];
            for (int i = 0; i < res.GetLength(0); i++)
            {
                res[i, 0] = main[0][0];
                res[i, 1] = main[0][1];
                res[i, 2] = main[0][2];
                res[i, 3] = main[0][3];
                main.RemoveAt(0);
            }
            return res;
        }

        public void Reload()
        {
            main = new List<object[]>();
            List<object[]> simple = new List<object[]>()
            {
                new object[] {2, 1, 0, 0}, // 1
                new object[] {1, 0, 0, 1}, // 2
                new object[] {1, 1, 0, 1}, // 3
                new object[] {2, 1, 1, 0}, // 4
                new object[] {1, 0, 2, 1}, // 5
                new object[] {1, 1, 2, 1}, // 6
                new object[] {2, 1, 0, 0}, // 7
                new object[] {2, 1, 1, 0}, // 8
                new object[] {2, 0, 0, 1}, // 9
                new object[] {2, 0, 1, 1}, // 10
                new object[] {2, 1, 1, 1}, // 11
                new object[] {2, 0, 0, 0}, // 12
                new object[] {1, 1, 2, 1}, // 13
                new object[] {1, 0, 2, 0}, // 14
                new object[] {1, 0, 0, 1}  // 15
            };
            while (simple.Count > 0)
            {
                int index = random.Next(simple.Count);
                main.Add(simple[index]);
                simple.RemoveAt(index);
            }
        }
    }
}
