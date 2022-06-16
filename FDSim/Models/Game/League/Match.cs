namespace FDSim.Models.Game.League;

using FDSim.Generators;
using FDSim.Models.Enums;
using FDSim.Models.Game.Team;
public class Match
{
    public Team Home { get; init; }
    public Team Away { get; init; }

    private String _winnerId = String.Empty;
    public String WinnerId { get => _winnerId; }
    private String _loserId = String.Empty;
    public String LoserId { get => _loserId; }
    public bool isFinished { get => _result is not null; }
    private MatchResult? _result;
    public MatchResult? Result { get => _result; }

    public static Match Make(Team home, Team away)
    {
        return new Match { Home = home, Away = away };
    }

    public void Simulate(Dicer dicer)
    {
        if (isFinished) return;
        //TODO: this should depend on team coach
        var coachPreferredLineup = Formation._442;

        var homeLineup = Home.Roster.GetLineup(coachPreferredLineup);
        var awayLineup = Away.Roster.GetLineup(coachPreferredLineup);

        var isHomeAvgMore = Home.Roster.Average() >= Away.Roster.Average();
        var differenceAvg = Math.Abs(Home.Roster.Average() - Away.Roster.Average());

        var goalHome = dicer.Chance(isHomeAvgMore ? Home.Roster.Average() : 50) ? dicer.Faker.Random.Int(0, 1) : 0;
        // bit less of a chance to start with a goal for away team
        // adding home stadium influence
        var goalAway = dicer.Chance(isHomeAvgMore ? differenceAvg : Away.Roster.Average()) ? dicer.Faker.Random.Int(0, 1) : 0;

        var homeAvg = homeLineup.gtAdjustedAvgSkillPerRole();
        var awayAvg = awayLineup.gtAdjustedAvgSkillPerRole();


        goalHome += dicer.Chance(homeAvg[Role.Striker] - awayAvg[Role.Defender]) ? dicer.Faker.Random.Int(0, 2) : dicer.Faker.Random.Int(0, 1);
        goalAway += dicer.Chance(awayAvg[Role.Striker] - homeAvg[Role.Defender]) ? dicer.Faker.Random.Int(0, 2) : dicer.Faker.Random.Int(0, 1);


        goalHome += dicer.Chance(homeAvg[Role.Midfielder]) ? dicer.Faker.Random.Int(0, 1) : 0;
        goalAway += dicer.Chance(awayAvg[Role.Midfielder]) ? dicer.Faker.Random.Int(0, 1) : 0;


        goalHome += dicer.Chance(homeAvg[Role.Striker]) ? dicer.Faker.Random.Int(0, 2) : 0;
        goalAway += dicer.Chance(awayAvg[Role.Striker]) ? dicer.Faker.Random.Int(0, 2) : 0;

        // adding coach influence
        // adding status morale influence
        goalHome += dicer.Chance(homeLineup.AvgMorale) ? dicer.Faker.Random.Int(0, 1) : 0;
        goalAway += dicer.Chance(awayLineup.AvgMorale) ? dicer.Faker.Random.Int(0, 1) : 0;

        goalHome += dicer.Chance(homeLineup.AvgCondition) ? dicer.Faker.Random.Int(0, 1) : 0;
        goalAway += dicer.Chance(awayLineup.AvgCondition) ? dicer.Faker.Random.Int(0, 1) : 0;
        // adding previous victories

        // Defence bonus
        goalHome -= goalHome == 0 ? 0 : (dicer.Chance((awayAvg[Role.Defender] + awayAvg[Role.Goalkeeper]) / 2.0) ? dicer.Faker.Random.Int(0, goalHome) : 0);
        goalAway -= goalAway == 0 ? 0 : (dicer.Chance((homeAvg[Role.Defender] + homeAvg[Role.Goalkeeper]) / 2.0) ? dicer.Faker.Random.Int(0, goalAway) : 0);

        if (goalHome == goalAway && dicer.Chance(30))
        {
            goalHome = goalAway = 0;
        }



        _winnerId = goalHome >= goalAway ? Home.Id : Away.Id;
        _loserId = _winnerId == Home.Id ? Away.Id : Home.Id;
        _result = MatchResult.Make(goalHome, goalAway, homeLineup, awayLineup, dicer);
    }

    public override String ToString()
    {
        var compactResult = isFinished ? $" {_result?.GoalHome} - {_result?.GoalAway}" : "";
        return $"{Home.Name} - {Away.Name}{compactResult}";
    }
}
