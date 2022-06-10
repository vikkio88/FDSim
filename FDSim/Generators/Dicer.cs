namespace FDSim.Generators;

using System;
using Bogus;

public class Dicer
{
    int _seed;
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
        return _faker.Random.Number(0,100) <= chance;
    }
}
