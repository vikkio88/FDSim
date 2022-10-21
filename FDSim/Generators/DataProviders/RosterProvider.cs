using FDSim.Models.Game.Team;
using FDSim.Models.People;

namespace FDSim.Generators.DataProviders;
public class RosterProvider
{
    Dicer _dicer;
    public RosterProvider(Dicer dicer)
    {
        _dicer = dicer;
    }

    public Roster GetRoster(Team team, PeopleGenerator peopleGenerator)
    {
        var players = new List<Player>();

        // adding at least some players per each role
        players.AddRange(
            peopleGenerator.GetPlayers(
                amount: _dicer.Faker.Random.Number(2, 4),
                mainNationality: team.Nationality,
                foreignPlayers: _dicer.Faker.Random.Number(0, 1),
                forcedRole: Models.Enums.Role.Goalkeeper
            )
        );

        players.AddRange(
            peopleGenerator.GetPlayers(
                amount: _dicer.Faker.Random.Number(4, 5),
                mainNationality: team.Nationality,
                foreignPlayers: _dicer.Faker.Random.Number(0, 2),
                forcedRole: Models.Enums.Role.Defender
            )
        );

        players.AddRange(
            peopleGenerator.GetPlayers(
                amount: _dicer.Faker.Random.Number(4, 5),
                mainNationality: team.Nationality,
                foreignPlayers: _dicer.Faker.Random.Number(0, 2),
                forcedRole: Models.Enums.Role.Midfielder
            )
        );

        players.AddRange(
            peopleGenerator.GetPlayers(
                amount: _dicer.Faker.Random.Number(3, 4),
                mainNationality: team.Nationality,
                foreignPlayers: _dicer.Faker.Random.Number(0, 2),
                forcedRole: Models.Enums.Role.Striker
            )
        );

        var totalNumber = _dicer.Faker.Random.Number(16, 25);

        var remaining = totalNumber - players.Count;
        if (remaining > 0)
            players.AddRange(
                peopleGenerator.GetBaseRoster(
                    amount: _dicer.Faker.Random.Number(0, remaining),
                    mainNationality: team.Nationality,
                    foreignPlayers: _dicer.Faker.Random.Number(0, remaining / 2)
                )
            );

        return new(players, team.Id);
    }
}
