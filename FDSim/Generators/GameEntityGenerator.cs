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
    private ReputationRange _reputationR;
    private ContractRange _contractR;
    private FinancesProvider _financesP;
    private FacilitiesProvider _facilitiesP;
    private RosterProvider _rosterP;
    private Dicer _dicer;

    public GameEntityGenerator(int seed, IIdGenerator? idGen = null, Dicer? dicer = null)
    {
        _seed = seed;
        _idGen = idGen ?? new IdGenerator();
        Randomizer.Seed = new Random(_seed);
        _pGen = new PeopleGenerator(_seed, _idGen);
        _dicer = dicer ?? new Dicer(seed);
        _reputationR = new ReputationRange(_dicer);
        _contractR = new ContractRange(_dicer);
        _financesP = new FinancesProvider(_dicer);
        _facilitiesP = new FacilitiesProvider(_dicer);
        _rosterP = new RosterProvider(_dicer);
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
        .Generate();

        t.Roster = _rosterP.GetRoster(t, _pGen);

        // 20% chance of a Champion
        if (_dicer.Chance(20))
        {
            var champion = _pGen.GetPlayer(forcedSkillPercent: 90);
            t.Roster?.AddPlayer(champion);
        }


        t.Reputation = _reputationR.GetReputation(new(t.Roster.Avg));
        foreach (var p in t.Roster.Players)
        {
            t.Roster.SetContract(p.Id, _contractR.GetContract(p, t.Id));
        }

        // 30% chance of a foreign Coach
        t.Coach = _pGen.GetCoach(forcedNationality: _dicer.Chance(30) ? _dicer.Faker.PickRandom<Nationality>() : nationality);
        t.Coach.Contract = _contractR.GetContract(t.Coach, t.Id);

        // add finances
        t.Finances = _financesP.GetFinances(t);
        // add youth and training facilities
        var (training, youth) = _facilitiesP.GetFacilities(t);
        t.TrainingFacilities = training;
        t.YouthAcademy = youth;

        return t;
    }
}
