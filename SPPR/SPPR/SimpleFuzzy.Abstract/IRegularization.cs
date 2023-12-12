using SimpleFuzzy.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR.Abstract
{
    public interface IRegularization : INameable
    {
        float Regular(float[][] model);
    }
}
