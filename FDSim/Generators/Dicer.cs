namespace FDSim.Generators;

using System;
using Bogus;
using FDSim.Generators.Utils;

public class Dicer : IDicer
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

    public T PickOne<T>(IList<T> list)
    {
        return _faker.PickRandom(list);
    }

    public T PickWeighted<T>(IList<T> list, IList<int> weights)
    {
        var options = WeightedOptionsHelper.MakeList(list, weights);
        var totalWeight = weights.Sum();

        var runningTotal = 0;
        foreach (var option in options)
        {
            runningTotal += option.Weight;
            if (runningTotal >= _faker.Random.Int(0, totalWeight))
            {
                return option.Item;
            }
        }

        // If no object is selected, return null
        // this should only happen if the list is empty
        return options[0].Item;
    }
    
    public T PickWeighted<T>(IList<(T, int)> optionsAndWeights)
    {
        var totalWeight = optionsAndWeights.Select(x => x.Item2).Sum();

        var runningTotal = 0;
        foreach (var (option, weight) in optionsAndWeights)
        {
            runningTotal += weight;
            if (runningTotal >= _faker.Random.Int(0, totalWeight))
            {
                return option;
            }
        }

        // If no object is selected, return null
        // this should only happen if the list is empty
        return optionsAndWeights[0].Item1;
    }
}
