namespace FDSim.Generators;

using System;
using Bogus;


using Models.People;
using Models.Enums;
using Models.Enums.Helpers;

using DataProviders;

public class PeopleGenerator
{
    const int PLAYER_MIN_AGE = 15;
    const int PLAYER_MAX_AGE = 40;
    const int COACH_MIN_AGE = 33;
    const int COACH_MAX_AGE = 75;
    private int _seed;
    private IIdGenerator _idGen;
    private Dicer? _dicer;
    PeopleSkillRange _peopleSR;
    ReputationRange _peopleRR;
    PeopleMoneyRange _peopleMR;

    public PeopleGenerator(Dicer dicer, IIdGenerator? idGen = null)
    {
        _dicer = dicer;
        _idGen = idGen ?? new IdGenerator();
        _peopleSR = new PeopleSkillRange(dicer);
        _peopleRR = new ReputationRange(dicer);
        _peopleMR = new PeopleMoneyRange(dicer);
    }
    public PeopleGenerator(int seed, IIdGenerator? idGen = null)
    {
        _seed = seed;
        _dicer = _dicer ?? new Dicer(_seed);
        _idGen = idGen ?? new IdGenerator();
        Randomizer.Seed = new Random(_seed);
        _peopleSR = new PeopleSkillRange(_dicer);
        _peopleRR = new ReputationRange(_dicer);
        _peopleMR = new PeopleMoneyRange(_dicer);
    }

    public Coach GetCoach(
    Nationality? forcedNationality = null, Formation? forcedModule = null,
    int? forcedMaxAge = null, int? forcedSkillPercent = null
 )
    {
        var nationality = forcedNationality ?? _dicer.Faker.PickRandom<Nationality>();
        // Male Footballers, hardcoded Gender
        var gender = Bogus.DataSets.Name.Gender.Male;
        var status = new Status()
        {
            Morale = new(_dicer.Faker.Random.Int(30, 100))
        };

        // Getting skill first as I need it for other calculations
        var skill = _peopleSR.GetCoachSkill(forcedSkillPercent);

        return new Faker<Coach>(locale: NationalityHelper.GetLocale(nationality))
        .RuleFor(p => p.Id, _idGen.Generate()) // maybe I can add a `c` or something to identify the id
        .RuleFor(p => p.Name, f => f.Name.FirstName(gender: gender))
        .RuleFor(p => p.Surname, f => f.Name.LastName(gender: gender))
        .RuleFor(p => p.Age, f => f.Random.Number(COACH_MIN_AGE, forcedMaxAge ?? COACH_MAX_AGE))
        .RuleFor(p => p.Skill, skill)
        .RuleFor(p => p.IdealWage, _peopleMR.GetIdealWage(skill, isCoach: true))
        .RuleFor(p => p.Reputation, _peopleRR.GetReputation(skill))
        .RuleFor(p => p.Status, status)
        .RuleFor(p => p.Nationality, nationality)
        .RuleFor(p => p.Module, f => forcedModule ?? f.PickRandom<Formation>());
    }

    public Player GetPlayer(
        Nationality? forcedNationality = null, Role? forcedRole = null,
        int? forcedMaxAge = null, int? forcedSkillPercent = null, int? forcedAge = null
     )
    {
        var nationality = forcedNationality ?? _dicer.Faker.PickRandom<Nationality>();
        // Male Footballers, hardcoded Gender
        var gender = Bogus.DataSets.Name.Gender.Male;
        var status = new PlayerStatus()
        {
            Morale = new(_dicer.Faker.Random.Int(30, 100)),
            Condition = new(_dicer.Faker.Random.Int(40, 100)),
            Attachment = new(_dicer.Faker.Random.Int(20, 100)),
        };
        // Getting skill first as I need it for other calculations
        var age = forcedAge ?? _dicer.Faker.Random.Number(PLAYER_MIN_AGE, forcedMaxAge ?? PLAYER_MAX_AGE);
        var skill = _peopleSR.GetPlayerSkill(age, forcedSkillPercent);

        return new Faker<Player>(locale: NationalityHelper.GetLocale(nationality))
        .RuleFor(p => p.Id, _idGen.Generate()) // maybe I can add a `p` or something to identify the id
        .RuleFor(p => p.Name, f => f.Name.FirstName(gender: gender))
        .RuleFor(p => p.Surname, f => f.Name.LastName(gender: gender))
        .RuleFor(p => p.Age, age)
        .RuleFor(p => p.Skill, skill)
        .RuleFor(p => p.Value, _peopleMR.GetValue(skill))
        .RuleFor(p => p.IdealWage, _peopleMR.GetIdealWage(skill))
        .RuleFor(p => p.Reputation, _peopleRR.GetReputation(skill))
        .RuleFor(p => p.Status, status)
        .RuleFor(p => p.Nationality, nationality)
        .RuleFor(p => p.Role, f => forcedRole ?? f.PickRandom<Role>());
    }

    public List<Player> GetPlayers(int amount = 10, Nationality? mainNationality = null, int foreignPlayers = 0, Role? forcedRole = null)
    {
        var list = new List<Player>();
        var nationalPlayers = amount - foreignPlayers;
        foreach (int _ in Enumerable.Range(0, nationalPlayers))
        {
            list.Add(GetPlayer(forcedNationality: mainNationality, forcedRole: forcedRole));
        }

        foreach (int _ in Enumerable.Range(0, foreignPlayers))
        {
            list.Add(GetPlayer(forcedRole: forcedRole));
        }

        return list;

    }
    public List<Player> GetBaseRoster(int amount = 10, Nationality? mainNationality = null, int foreignPlayers = 0)
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
