using FDSim.Models.Game.League;
using FDSim.Models.Game.Team;

namespace FDSim.Models.Game.History;
public class LeagueHistory
{
    private Dictionary<string, List<TeamSeason>> _map = new();

    public List<TeamSeason>? GetById(string teamId)
    {
        if (!_map.ContainsKey(teamId))
            return null;

        return _map[teamId];
    }

    public void Add(Team.Team team, TableRow tableRow, int endOfSeasonYear)
    {

        if (!_map.ContainsKey(team.Id))
        {
            _map[team.Id] = new();
        }

        _map[team.Id].Add(new()
        {
            Team = new(team),
            Season = $"{endOfSeasonYear - 1}/{endOfSeasonYear}",
            Played = tableRow.Played,
            FinalPosition = tableRow.Position ?? 0,
            Points = tableRow.Points,
            Won = tableRow.Won,
            Drawn = tableRow.Drawn,
            Lost = tableRow.Lost,
            GoalsConceded = tableRow.GoalsConceded,
            GoalsScored = tableRow.GoalsScored,
        });

    }
}


public class TeamSeason
{
    public TeamPlaceholder Team { get; set; }
    public string Season { get; set; } // this will look like 2022/2023
    public int Played { get; set; }
    public int FinalPosition { get; set; }
    public int Points { get; set; }
    public int Won { get; set; }
    public int Drawn { get; set; }
    public int Lost { get; set; }
    public int GoalsScored { get; set; }
    public int GoalsConceded { get; set; }
}
