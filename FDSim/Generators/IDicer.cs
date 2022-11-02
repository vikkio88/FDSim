using Bogus;

namespace FDSim.Generators;

public interface IDicer
{
    int Seed { get; }
    Faker Faker { get; }
    bool Chance(int chance = 50);
    bool Chance(double chance);
    int Percentage();
    int Int(int min = int.MinValue, int max = int.MaxValue);
    public T PickOne<T>(IList<T> list);
}