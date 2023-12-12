using SimpleFuzzy.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SPPR.Abstract
{
    public interface IOptimazer : INameable
    {
        float[][] Optimazer(Func<float[][], float[], float[]> calc, float[][] model, float[][] input, float[] output, float n, float l, float k, ILoss lossFunc, IRegularization regularization);
    }
}
