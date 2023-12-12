using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRule
{
    internal class LabInput : IGetClassificationSetObject
    {
        public string ConnctionString { get => null; set { } }

        public string Name => "Классификация болезни Паркинсона";

        List<object[]> main = new List<object[]>();

        Random random = new Random();

        int[] filterId = new int[] 
        {
            126,
406,
450,
573,
359,
394,
31,
542,
133,
441,
369,
361,
333,
485,
121,
119,
549,
302,
355,
749,
486,
167,
55,
4,
106,
136,
627,
537,
88,
577,
426,
30,
478,
124,
23,
427,
657,
594,
5,
519,
9,
440,
744,
650,
347,
390,
258,
112,
312,
618,
661,
626,
751,
482,
753,
39,
391,
111,
74,
346,
42,
108,
41,
332,
90,
621,
662,
89,
584,
36,
632,
477,
102,
50,
428,
612,
6,
122,
446,
377,
699,
63,
683,
389,
305,
134,
598,
630,
313,
28,
736,
52,
139,
455,
647,
409,
358,
60,
342,
59,
322,
418,
608,
463,
619,
303,
720,
585,
3,
77,
8,
339,
330,
575,
98,
445,
442,
114,
123,
131,
45,
168,
83,
658,
539,
532,
24,
329,
734,
419,
499,
12,
92,
32,
33,
7

        };

        public LabInput()
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
            FileStream file = new FileStream("lab.csv", FileMode.Open);
            StreamReader reader = new StreamReader(file);
            List<object[]> simple = new List<object[]>();
            while (!reader.EndOfStream)
            {
                var v = reader.ReadLine().Split(',').ToList();
                v.Insert(0, v[^1]);
                v.RemoveAt(v.Count - 1);
                List<object> supperList = new List<object>();
                supperList.Add(int.Parse(v[0].Replace('.', ',')) + 1);
                //supperList.Add(v[1]);
                //supperList.Add(v[2]);
                foreach(var value in filterId)
                {
                    supperList.Add(double.Parse(v[value].Replace('.', ',')));
                }
                    /*
                for (int i = 3; i < v.Count; i++)
                {
                    if (filterId.Contains(i + 1))
                        supperList.Add(double.Parse(v[i].Replace('.', ',')));
                    if (i + 1 == 126 || i + 1 == 406 || i + 1 == 450)
                    {
                        supperList.Insert(1, supperList[^1]);
                        supperList.RemoveAt(supperList.Count - 1);
                    }
                }
                    */
                simple.Add(supperList.Take((int)(supperList.Count * 0.8)).ToArray());
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
