namespace FDSim.Generators.Utils;
public interface IWeightedOption<T>
{
    public int Weight { get; set; }
    public T Item { get; set; }
}
