using SimpleFuzzy.Abstract;

namespace SPPR.Regress
{
    public class SimpleSet : IGetSets
    {
        Random random = new Random();
        public string Name => "От -10 до 10";

        public double[] GetTrainSet(int count)
        {
            double[] result = new double[count];
            for(int i = 0; i < count; i++)
            {
                result[i] = random.NextDouble() * 20 - 10;
            }
            return result;
        }

        public double[] GetTestSet(int count)
        {
            if(count < 2) count = 2;
            double[] result = new double[count];
            for (int i = 0; i < count - 1; i++)
            {
                result[i] = i * 20.0 / (count - 1) - 10;
            }
            result[^1] = 10;
            return result;
        }
    }
}