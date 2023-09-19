using SimpleFuzzy.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR.Regress
{
    internal class HardFunc : IHasFunc
    {
        public string Name => "y = (2^x) * sin(2^-x)";

        public double GetResult(double input)
            => Math.Pow(2, input) * Math.Sin(Math.Pow(2, -input));
    }
}
