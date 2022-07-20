namespace FDSim.Models.Game.League;

public class League : IdEntity
{
    public string Name { get; set; } = string.Empty;
    public List<Round> Rounds { get; set; } = new();

    private int _roundPointer = 0;

    public static League Make(List<string> teamIds, string? name = null)
    {
        var firstHalf = new List<Round>();
        var secondHalf = new List<Round>();
        int numberOfTeams = teamIds.Count;
        var totalRounds = numberOfTeams - 1;
        var matchesPerRound = numberOfTeams / 2;
        for (int round = 0; round < totalRounds; round++)
        {
            var tempRoundFirstHalf = new List<MatchPlaceholder>();
            var tempRoundSecondHalf = new List<MatchPlaceholder>();
            for (var match = 0; match < matchesPerRound; match++)
            {
                var home = (round + match) % (numberOfTeams - 1);
                var away = (numberOfTeams - 1 - match + round) % (numberOfTeams - 1);
                if (match == 0)
                {
                    away = numberOfTeams - 1;
                }
                tempRoundFirstHalf.Add(new() { HomeId = teamIds[home], AwayId = teamIds[away] });
                tempRoundSecondHalf.Add(new() { HomeId = teamIds[away], AwayId = teamIds[home] });
            }
            firstHalf.Add(new()
            {
                Index = round,
                Matches = tempRoundFirstHalf,
            });
            secondHalf.Add(new()
            {
                Index = round + totalRounds,
                Matches = tempRoundSecondHalf
            });

        }

        firstHalf.AddRange(secondHalf);

        return new League()
        {
            Rounds = firstHalf,
            Name = name ?? string.Empty
        };
    }

    public Round GetNextRound()
    {
        return Rounds[_roundPointer];
    }

}
