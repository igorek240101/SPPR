namespace SimpleFuzzy.Abstract
{
    public interface IGetSets : INameable
    {
        double[] GetTrainSet(int count);
        double[] GetTestSet(int count);
    }
}