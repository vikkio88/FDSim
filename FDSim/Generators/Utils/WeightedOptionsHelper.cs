using System.Linq;
namespace FDSim.Generators.Utils;

public class WeightedOption<T> : IWeightedOption<T>
{
    public int Weight { get; set; }
    public T Item { get; set; }
}
public static class WeightedOptionsHelper
{
    public static List<WeightedOption<T>> MakeList<T>(IList<T> options, IList<int> weights)
    {
        if (weights.Count != options.Count)
        {
            throw new ArgumentException($"weights should be the same as the options: weights {weights.Count}, opts: {options.Count}");
        }
        return options.Zip(weights).Select(pair => new WeightedOption<T>() { Item = pair.First, Weight = pair.Second }).ToList();
    }
}
