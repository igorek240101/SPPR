using SimpleFuzzy.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR.Abstract
{
    public interface IActivationFunc : INameable
    {
        float ActivationFunc(float input);
    }
}
