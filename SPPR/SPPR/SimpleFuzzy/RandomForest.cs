using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SPPR.Tree;

namespace SPPR
{
    internal class RandomForest
    {
        Random random = new Random();
        (Tree, double)[] trees { get; set; }
        public double[] Price { get; set; }
        public string Text { get; set; }
        private ITreeRule CreateRule { get; set; }
        public RandomForest((object[], int)[] train, int treeCount,
            int deep, int split, int m,
            ITreeRule createRule)
        {

            trees = new (Tree, double)[treeCount];
            Price = new double[train[0].Item1.Length];
            for (int i = 0; i < treeCount; i++)
            {
                (object[], int)[] subTrain = new (object[], int)[train.Length];
                HashSet<int> valid = new HashSet<int>();
                List<(object[], int)> subTest = new List<(object[], int)>();
                for (int j = 0; j < train.Length; j++)
                {
                    subTest.Add(train[j]);
                    int index = random.Next(train.Length);
                    subTrain[j] = train[index];
                    valid.Add(index);
                    subTest.Remove(train[index]);
                }
                Tree tree = new Tree(train, deep, split, m, createRule);
                int trueCount = 0;
                foreach (var value in subTest)
                {
                    if (tree.GetClass(value.Item1) == value.Item2)
                        trueCount++;
                }
                for (int j = 0; j < train[0].Item1.Length; j++)
                {
                    int notTrueCount = 0;
                    for (int k = 0; k < subTest.Count; k++)
                    {
                        var input = subTest[k].Item1.ToList();
                        input[j] = subTest[random.Next(subTest.Count)].Item1[j];
                        if (tree.GetClass(input.ToArray()) == subTest[k].Item2)
                            notTrueCount++;
                    }
                    Price[j] += (double)trueCount / subTest.Count - (double)notTrueCount / subTest.Count;
                }
                trees[i] = (tree, (double)trueCount / subTest.Count);
            }
            foreach (var price in Price)
            {
                Text += price + "\r\n";
            }
        }

        public int GetClass(object[] test, bool isWeight)
        {
            Dictionary<int, double> keys = new Dictionary<int, double>();
            foreach (var value in trees)
            {
                int res = value.Item1.GetClass(test);
                if (keys.ContainsKey(res))
                {
                    keys[res] += isWeight ? value.Item2 : 1;
                }
                else
                {
                    keys.Add(res, isWeight ? value.Item2 : 1);
                }
            }
            return keys.FirstOrDefault(v => v.Value == keys.Max(t => t.Value)).Key;
        }
    }
}
