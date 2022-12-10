using Bogus;

namespace FDSim.Tests.Generators;

using FDSim.Generators;

public class DicerStub : IDicer
{
    public int Seed { get; init; }

    public Faker Faker
    {
        get => _faker;
    }

    private readonly Faker _faker;

    public int FixedChance { get; init; } = 50;

    public double FixedDoubleChance { get; init; } = 50;
    public int FixedInt { get; init; } = 50;
    public int FixedPercent { get; init; } = 50;
    public int Pick { get; init; } = 0;

    public DicerStub()
    {
        _faker = new Faker();
    }

    public static IDicer Make(int fixedChance = 50, double fixedDoubleChance = 50, int fixedInt = 100,
        int fixedPercent = 50, int pick = 0) =>
        new DicerStub()
        {
            Seed = 0,
            Pick = 0,
            FixedChance = fixedChance,
            FixedDoubleChance = fixedDoubleChance,
            FixedInt = fixedInt,
            FixedPercent = fixedPercent
        };

    public bool Chance(int chance = 50) => FixedChance >= chance;

    public bool Chance(double chance) => FixedDoubleChance >= chance;

    public int Percentage() => FixedPercent;

    public int Int(int min = Int32.MinValue, int max = Int32.MaxValue) => FixedInt;

    public T PickOne<T>(IList<T> list)
    {
        return list[Pick];
    }
}