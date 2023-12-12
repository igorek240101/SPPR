using SPPR.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Image
{
    internal class ReLU : IActivationFunc
    {
        public string Name => "ReLU";

        public float ActivationFunc(float input)
            => Math.Max(0, input);
    }
}
