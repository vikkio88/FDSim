using FDSim.Models.Game.League;

namespace FDSim.Models.Game;
public class GameState
{
    public GamePlayer GamePlayer { get; init; }
    public Table Table { get; init; }
    public PlayerStats PlayersStats { get; init; }
    public int RoundPointer { get; init; }
    public List<Round> Rounds { get; init; }
    public Dictionary<string, Team.Team> TeamsMap { get; init; }

    public static GameState Make(
     GamePlayer gamePlayer,
     Table table,
     PlayerStats playersStats,
     int roundPointer,
     List<Round> rounds,
     Dictionary<string, Team.Team> teamsMap)
    {
        return new()
        {
            GamePlayer = gamePlayer,
            Table = table,
            PlayersStats = playersStats,
            RoundPointer = roundPointer,
            Rounds = rounds,
            TeamsMap = teamsMap,
        };
    }
}
