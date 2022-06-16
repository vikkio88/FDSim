namespace FDSim.Generators;

using System;
using Bogus;


using Models.People;
using Models.Enums;
using Models.Enums.Helpers;

using DataProviders;

public class PeopleGenerator
{
    private int _seed;
    private IIdGenerator _idGen;
    private Dicer? _dicer;

    public PeopleGenerator(int seed, IIdGenerator? idGen = null)
    {
        _seed = seed;
        _idGen = idGen ?? new IdGenerator();
        Randomizer.Seed = new Random(_seed);
    }

    public Player GetPlayer(
        Nationality? forcedNationality = null, Role? forcedRole = null,
        int? forcedMaxAge = null, int? forcedSkillPercent = null
     )
    {
        _dicer = _dicer ?? new Dicer(_seed);
        var peopleSR = new PeopleSkillRange(_dicer);
        var nationality = forcedNationality ?? _dicer.Faker.PickRandom<Nationality>();
        // Male Footballers, hardcoded Gender
        var gender = Bogus.DataSets.Name.Gender.Male;
        var status = new PlayerStatus()
        {
            Morale = new(_dicer.Faker.Random.Int(30, 100)),
            Condition = new(_dicer.Faker.Random.Int(40, 100)),
            Attachment = new(_dicer.Faker.Random.Int(20, 100)),
        };
        return new Faker<Player>(locale: NationalityHelper.GetLocale(nationality))
        .RuleFor(p => p.Id, _idGen.Generate())
        .RuleFor(p => p.Name, f => f.Name.FirstName(gender: gender))
        .RuleFor(p => p.Surname, f => f.Name.LastName(gender: gender))
        .RuleFor(p => p.Age, f => f.Random.Number(15, forcedMaxAge ?? 39))
        .RuleFor(p => p.SkillAvg, peopleSR.GetSkill(forcedSkillPercent))
        .RuleFor(p => p.Status, status)
        .RuleFor(p => p.Nationality, nationality)
        .RuleFor(p => p.Role, f => forcedRole ?? f.PickRandom<Role>());
    }

    public List<Player> GetPlayers(int amount = 10, Nationality? mainNationality = null, int foreignPlayers = 0)
    {
        var list = new List<Player>();
        foreach (int _ in Enumerable.Range(0, amount))
        {
            list.Add(GetPlayer(forcedNationality: mainNationality));
        }

        foreach (int _ in Enumerable.Range(0, foreignPlayers))
        {
            list.Add(GetPlayer());
        }

        // adding GK
        list.Add(GetPlayer(forcedRole: Role.Goalkeeper));

        return list;

    }
}
