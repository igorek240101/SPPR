using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SPPR
{
    internal class TreeRegress
    {
        Random random = new Random();
        public string Text { get; set; }
        private int Deep { get; set; }
        private int Split { get; set; }
        private INode Root { get; set; }
        private ITreeRegressRule CreateRule { get; set; }
        public TreeRegress((object[], float)[] train, int deep, int split, int m, ITreeRegressRule createRule) 
        {
            Deep = deep;
            CreateRule = createRule;
            Split = split;
            Root = CreateNode(train, 1, m);
        }

        private INode CreateNode((object[], float)[] train, int level, int m)
        {
            if (level == Deep || train.Length <= Split)
            { // Если достигли глубины или минимального размера узла, то находим и возвращаем самый частый класс
                return new List() { Value = train.Average(t => t.Item2) };
            }
            var ruleInput = GetRandomParametrs(train, m); // Выбор параметров для созадния правила
            var rule = CreateRule.CreateRule(ruleInput); // Созадние правила
            if (rule == (null, null, null) || rule.Item2[0].Length == 0 || rule.Item2[1].Length == 0)
            { // Нет эффективных способов разделеия, ищем самый частый класс
                return new List() { Value = train.Average(t => t.Item2) };
            }
            Text += "(";
            Text += rule.Item3;
            List<INode> nodes = new List<INode>();
            for (int i = 0; i < rule.Item2.Count; i++)
            { // Рекурсивно создаем потомков
                if (i != 0)
                    Text += ":";
                nodes.Add(CreateNode(rule.Item2[i], level + 1, m));
            }
            Text += ")";
            Node node = new Node()
            {
                Rule = rule.Item1,
                Children = nodes
            };
            return node;
        }

        private (object[], float)[] GetRandomParametrs((object[], float)[] train, int m)
        {
            // Выбор n - m случайных параметров (если m равно длине то используются все параметры)
            if (train[0].Item1.Length == m) return train;
            var v = train.ToList().ConvertAll(t => (t.Item1.ToList(), t.Item2));
            for (int j = 0; j < m; j++)
            {
                List<int> ints = new List<int>();
                for (int i = 0; i < v[0].Item1.Count; i++)
                    ints.Add(i);
                int index = random.Next(0, ints.Count);
                for(int i = 0; i < v.Count; i++)
                {
                    v[i].Item1[ints[index]] = "0";
                }
                ints.RemoveAt(index);
            }
            return v.ConvertAll(t => (t.Item1.ToArray(), t.Item2)).ToArray();
        }

        public float GetValue(object[] test)
        { // Прогнозирование класса по модели
            INode now = Root;
            while (now.GetType() != typeof(List))
            {
                Node node = now as Node;
                now = node.Children[node.Rule(test)];
            }
            return (now as List).Value;
        }

        internal interface INode { }
        internal class List : INode
        {
            internal float Value { get; set; }
        }

        internal class Node : INode
        {
            internal Func<object[], int> Rule { get; set; }

            internal List<INode> Children { get; set; }
        }
    }
}
