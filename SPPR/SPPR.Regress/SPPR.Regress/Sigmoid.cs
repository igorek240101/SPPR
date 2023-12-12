using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPR.Abstract;

namespace SPPR.Regress
{
    internal class Sigmoid : IActivationFunc
    {
        public string Name => "Сигмоида";

        public float ActivationFunc(float input)
            => (float)(1 / (1 + Math.Pow(Math.E, -input)));
    }
}
