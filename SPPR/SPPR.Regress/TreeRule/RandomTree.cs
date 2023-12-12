using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeRule
{
    internal class RandomTree : ITreeRule
    {
        public string Name => "Случайное дерево";

        public (Func<object[], int>, List<(object[], int)[]>, string) CreateRule((object[], int)[] input)
        {
            Random random = new Random();
            int index = random.Next(input[0].Item1.Length);
            if (input[0].Item1[index] is double)
            {
                double min = input.Min(t => (double)t.Item1[index]);
                double max = input.Max(t => (double)t.Item1[index]);
                double split = random.NextDouble();
                split = split * (max - min) + min;
                return (t => (double)t[index] < split ? 0 : 1, new List<(object[], int)[]>() { input.Where(v => (double)v.Item1[index] < split).ToArray(), input.Where(v => (double)v.Item1[index] >= split).ToArray() }, $"t[{index}] < {split}\r\n");
            }
            else
            {
                int oIndex = random.Next(input.Length);
                List<(object[], int)[]> sets = new List<(object[], int)[]>
                {
                    input.Where(t => t.Item1[index].Equals(input[oIndex].Item1[index])).ToArray(),
                    input.Where(t => !t.Item1[index].Equals(input[oIndex].Item1[index])).ToArray()
                };
                return (t => t[index] == input[oIndex].Item1[index] ? 0 : 1, sets, $"t[{index}] == {input[oIndex].Item1[index]}");
            }
        }
    }
}
