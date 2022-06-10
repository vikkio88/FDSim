namespace FDSim.Generators;

using System;
using Bogus;

using Models.Game;
using Models.Enums;
using Models.Enums.Helpers;

using DataProviders;

public class GameEntityGenerator
{
    private int _seed;
    private IIdGenerator _idGen;
    private PeopleGenerator _pGen;
    private Dicer _dicer;

    public GameEntityGenerator(int seed, IIdGenerator? idGen = null)
    {
        _seed = seed;
        _idGen = idGen ?? new IdGenerator();
        Randomizer.Seed = new Random(_seed);
        _pGen = new PeopleGenerator(_seed, _idGen);
        _dicer = new Dicer(seed);
    }

    public Team GetTeam(Nationality? forcedNationality = null)
    {
        var nameTemplate = _dicer.Chance(50) ? "{0}" : _dicer.Faker.PickRandom(TeamNameTemplates.Data);
        var nationality = forcedNationality ?? _dicer.Faker.PickRandom<Nationality>();
        return new Faker<Team>(locale: NationalityHelper.getLocale(nationality))
        .RuleFor(p => p.Id, _idGen.generate())
        .RuleFor(p => p.Name, f => String.Format(nameTemplate, f.Address.City()))
        .RuleFor(p => p.Nationality, nationality)
        .RuleFor(t => t.Roster, f =>
        {
            return _pGen.GetPlayers(amount: f.Random.Number(15, 20), mainNationality: nationality, foreignPlayers: f.Random.Number(0, 5));
        });
    }
}
