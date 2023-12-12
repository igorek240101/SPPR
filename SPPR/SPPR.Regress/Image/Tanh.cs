using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image
{
    internal class Tanh : IActivationFunc
    {
        public string Name => "Гипреболический тангенс";

        public float ActivationFunc(float input)
            => (float)((Math.Exp(input) - Math.Exp(-input)) / (Math.Exp(input) + Math.Exp(-input)));
    }
}
