using SimpleFuzzy.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR.Abstract
{
    public interface ITreeRule : INameable
    {
        (Func<object[], int>, List<(object[], int)[]>, string) CreateRule((object[], int)[] input);
    }

    public interface ITreeRegressRule : INameable
    {
        (Func<object[], int>, List<(object[], float)[]>, string) CreateRule((object[], float)[] input);
    }
}
