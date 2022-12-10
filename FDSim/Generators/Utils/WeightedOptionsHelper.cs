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

        var result = new List<WeightedOption<T>>();

        for (var i = 0; i < weights.Count; i++)
        {
            result.Add(new() { Weight = weights[i], Item = options[i] });
        }

        return result;
    }
}
