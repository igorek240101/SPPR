using SimpleFuzzy.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR.Regress
{
    internal class Sin : IHasFunc
    {

        public string Name => "y = sin(x)";

        public double GetResult(double input)
            => Math.Sin(input);
    }
}
