using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SPPR.Tree;

namespace SPPR
{
    internal class RegressForest
    {
        Random random = new Random();
        (TreeRegress, double)[] trees { get; set; }
        public double[] Price { get; set; }
        public string Text { get; set; }
        double treeNormales = 0;
        private ITreeRule CreateRule { get; set; }
        public RegressForest((object[], float)[] train, int treeCount,
            int deep, int split, int m,
            ITreeRegressRule createRule)
        {

            trees = new (TreeRegress, double)[treeCount];
            Price = new double[train[0].Item1.Length];
            for (int i = 0; i < treeCount; i++)
            {
                (object[], float)[] subTrain = new (object[], float)[train.Length];
                HashSet<int> valid = new HashSet<int>();
                List<(object[], float)> subTest = new List<(object[], float)>();
                for (int j = 0; j < train.Length; j++)
                {
                    subTest.Add(train[j]);
                    int index = random.Next(train.Length);
                    subTrain[j] = train[index];
                    valid.Add(index);
                    subTest.Remove(train[index]);
                }
                TreeRegress tree = new TreeRegress(train, deep, split, m, createRule);
                float sko = 0;
                foreach (var value in subTest)
                {
                    sko += (float)Math.Pow(tree.GetValue(value.Item1) - value.Item2, 2);
                }
                sko /= subTest.Count;
                for (int j = 0; j < train[0].Item1.Length; j++)
                {
                    float notTrueSKO = 0;
                    for (int k = 0; k < subTest.Count; k++)
                    {
                        var input = subTest[k].Item1.ToList();
                        input[j] = subTest[random.Next(subTest.Count)].Item1[j];
                        notTrueSKO += (float)Math.Pow(tree.GetValue(input.ToArray()) - subTest[k].Item2, 2);
                    }
                    Price[j] += Math.Abs(sko - (notTrueSKO / subTest.Count));
                }
                trees[i] = (tree, 1/sko);
                treeNormales += trees[i].Item2;
            }
            foreach (var price in Price)
            {
                Text += price + "\r\n";
            }
        }

        public float GetValue(object[] test, bool isWeight)
        {
            if (isWeight)
            {
                double avg = 0f;
                foreach (var value in trees)
                {
                    avg += value.Item1.GetValue(test) * (value.Item2 / treeNormales);
                }
                return (float)avg;
            }
            else
            {
                float avg = 0f;
                foreach (var value in trees)
                {
                    avg += value.Item1.GetValue(test);
                }
                return avg / trees.Length;
            }
        }
    }
}
