using FDSim.Models.Game.League;
using FDSim.Models.Game.Team;
using FDSim.Models.People;

namespace FDSim.Models.Game.History;
public class PlayersHistory
{
    private Dictionary<string, List<PlayerSeason>> _map = new();

    public List<PlayerSeason>? GetById(string playerId)
    {
        if (!_map.ContainsKey(playerId))
            return null;

        return _map[playerId];
    }

    public void Add(Player player, Team.Team team, StatRow statRow, int endOfSeasonYear)
    {

        if (!_map.ContainsKey(player.Id))
        {
            _map[player.Id] = new();
        }

        _map[player.Id].Add(new()
        {
            Player = new(player),
            Team = new(team),
            Season = $"{endOfSeasonYear - 1}/{endOfSeasonYear}",
            ScorersPosition = statRow.Position ?? 0,
            Played = statRow.Played,
            Goals = statRow.Goals,
            Rate = statRow.Rate,
        });

    }
}


public class PlayerSeason
{
    public PlayerPlaceholder Player { get; set; }
    public TeamPlaceholder Team { get; set; }
    public string Season { get; set; } // this will look like 2022/2023
    public int ScorersPosition { get; set; }
    public int Played { get; set; }
    public int Goals { get; set; }
    public double Rate { get; set; }

}
