namespace FDSim.Generators;

using System;
using Bogus;

public class Dicer
{
    private int _seed;
    public int Seed { get => _seed; }
    private Faker _faker;
    public Faker Faker { get => _faker; }

    public static Dicer Make(int? seed = null)
    {
        var rng = new Random();
        return new(rng.Next());
    }
    public Dicer(int seed)
    {
        _seed = seed;
        Randomizer.Seed = new Random(_seed);
        _faker = new Faker();

    }
    public bool Chance(int chance = 50)
    {
        return Percentage() <= chance;
    }

    public bool Chance(double chance)
    {
        return Percentage() <= chance;
    }

    public int Percentage()
    {
        return _faker.Random.Number(0, 100);
    }

    public int Int(int min = int.MinValue, int max = int.MaxValue)
    {
        return _faker.Random.Int(min, max);
    }
}
