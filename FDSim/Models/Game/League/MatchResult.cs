namespace FDSim.Models.Game.League;

using FDSim.Generators;
using FDSim.Models.Enums;
using FDSim.Models.Game.Team;
using FDSim.Models.People;

public class MatchResult : IdEntity
{
    public string MatchId { get; set; } = string.Empty;
    public int GoalHome { get; init; } = 0;
    public int GoalAway { get; init; } = 0;
    public bool isDraw { get; init; } = false;
    private List<String>? _scorersHomeIds;
    public List<String>? ScorersHomeIds { get => _scorersHomeIds; private init => _scorersHomeIds = value; }
    private List<String>? _scorersAwayIds;
    public List<String>? ScorersAwayIds { get => _scorersAwayIds; private init => _scorersAwayIds = value; }
    public List<string> HomeLineup { get; set; }
    public List<string> AwayLineup { get; set; }

    public static MatchResult Make(int goalHome, int goalAway, Lineup homeLineup, Lineup awayLineup, Dicer dicer)
    {
        var scorersHomeIds = new List<string>();
        var scorersAwayIds = new List<string>();
        foreach (var i in Enumerable.Range(0, goalHome))
        {
            var scorer = GetScorer(homeLineup, dicer);
            scorersHomeIds.Add(scorer.Id);
        }

        foreach (var i in Enumerable.Range(0, goalAway))
        {
            var scorer = GetScorer(awayLineup, dicer);
            scorersAwayIds.Add(scorer.Id);
        }

        return new MatchResult()
        {
            GoalHome = goalHome,
            GoalAway = goalAway,
            isDraw = goalAway == goalHome,
            ScorersHomeIds = scorersHomeIds,
            ScorersAwayIds = scorersAwayIds,
            // maybe here need the bench too??
            HomeLineup = homeLineup.Starters.Select(p => p.Id).ToList(),
            AwayLineup = awayLineup.Starters.Select(p => p.Id).ToList(),
        };
    }

    private static Player GetScorer(Lineup lineup, Dicer dicer)
    {
        var sts = lineup.Starters.FindAll(p => p.Role == Role.Striker);
        var nonGK = lineup.Starters.FindAll(p => p.Role != Role.Goalkeeper);
        return dicer.Percentage() switch
        {
            > 90 => dicer.Faker.PickRandom(nonGK),
            <= 80 => dicer.Faker.PickRandom(sts.Count > 0 ? sts : nonGK),
            _ => dicer.Faker.PickRandom(nonGK),
        };
    }

    public override string ToString()
    {
        return $"{GoalHome} - {GoalAway}";
    }
}
