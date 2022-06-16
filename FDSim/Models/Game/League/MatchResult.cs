namespace FDSim.Models.Game.League;

using FDSim.Generators;
using FDSim.Models.Game.Team;
public class MatchResult
{
    public int GoalHome { get; init; } = 0;
    public int GoalAway { get; init; } = 0;
    public bool isDraw { get; init; } = false;
    private List<String>? _scorersHomeIds;
    public List<String>? ScorersHomeIds { get => _scorersHomeIds; }
    private List<String>? _scorersAwayIds;
    public List<String>? ScorersAwayIds { get => _scorersAwayIds; }

    public static MatchResult Make(int goalHome, int goalAway, Lineup homeLineup, Lineup awayLineup, Dicer dicer)
    {

        //TODO: Pick Scorers
        
        return new MatchResult()
        {
            GoalHome = goalHome,
            GoalAway = goalAway,
            isDraw = goalAway == goalHome,
        };
    }
}
