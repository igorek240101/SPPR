using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SPPR.Abstract;

namespace SPPR.Regress
{
    internal class LinarActivation : IActivationFunc
    {
        public string Name => "Линейная активация с насыщнием";

        public float ActivationFunc(float input)
            => input < 0 ? 0 : (input > 1 ? 1 : input);
    }
}
