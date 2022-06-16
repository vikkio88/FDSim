namespace FDSim.Generators;

using System;
using Bogus;

public class Dicer
{
    private int _seed;
    public int Seed { get => _seed; }
    private Faker _faker;
    public Faker Faker { get => _faker; }
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
}
