using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPPR
{
    public class Neron
    {
        public Neron[] Input { get; private set; }
        public int InputCount { get => Input.Length; }
        private Func<float, float> Activation { get; set; }
        public float Output { get; private set; }

        public Neron(Neron[] input, Func<float, float> activation) 
        {
            Input = input;
            Activation = activation;
        }

        public Neron(Func<float, float> activation)
        {
            Activation = activation;
        }

        public void CalcNeron(float[] w)
        {
            float res = 0;
            for (int i = 0; i < Input.Length; i++)
            {
                res += Input[i].Output * w[i];
            }
            Output = Activation(res);
        }

        public void CalcNeron(float[] w, float[] inputs)
        {
            float res = 0;
            for (int i = 0; i < inputs.Length; i++)
            {
                res += inputs[i] * w[i];
            }
            Output = Activation(res);
        }
    }
}
