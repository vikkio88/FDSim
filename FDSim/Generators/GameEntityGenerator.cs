namespace FDSim.Generators;

using System;
using Bogus;

using Models.Game.Team;
using Models.Enums;
using Models.Enums.Helpers;

using DataProviders;

public class GameEntityGenerator
{
    private int _seed;
    private IIdGenerator _idGen;
    private PeopleGenerator _pGen;
    private Dicer _dicer;

    public GameEntityGenerator(int seed, IIdGenerator? idGen = null, Dicer? dicer = null)
    {
        _seed = seed;
        _idGen = idGen ?? new IdGenerator();
        Randomizer.Seed = new Random(_seed);
        _pGen = new PeopleGenerator(_seed, _idGen);
        _dicer = dicer ?? new Dicer(seed);
    }

    public Team GetTeam(Nationality? forcedNationality = null)
    {
        var nationality = forcedNationality ?? _dicer.Faker.PickRandom<Nationality>();
        var nameTemplate = _dicer.Faker.PickRandom(TeamNameTemplates.ByNationality(nationality));
        var city = _dicer.Faker.PickRandom(CityNames.ByNationality(nationality));
        var t = new Faker<Team>(locale: NationalityHelper.GetLocale(nationality))
        .RuleFor(p => p.Id, _idGen.Generate())
        .RuleFor(p => p.Name, String.Format(nameTemplate, city))
        .RuleFor(p => p.City, city)
        .RuleFor(p => p.Nationality, nationality)
        .RuleFor(t => t.Roster, f =>
        {
            return new(
                _pGen.GetPlayers(
                    amount: f.Random.Number(15, 20),
                    mainNationality: nationality,
                    foreignPlayers: f.Random.Number(0, 5)
                    )
            );
        }).Generate();

        // 20% chance of a Champion
        if (_dicer.Chance(20))
        {
            var champion = _pGen.GetPlayer(forcedSkillPercent: 90);
            t.Roster?.AddPlayer(champion);
        }

        t.Reputation = (new ReputationRange(_dicer)).GetReputation(new(t.Roster.Avg));
        var contractR = new ContractRange(_dicer);
        foreach (var p in t.Roster.Players)
        {
            t.Roster.SetContract(p.Id, contractR.GetContract(p));
        }

        // add finances
        // add youth and training facilities


        return t;
    }
}
