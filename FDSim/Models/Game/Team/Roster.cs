namespace FDSim.Models.Game.Team;

using System.Linq;

using FDSim.Models.People;

public class Roster
{
    // -1 means that needs to be calculated
    private double _avg = -1;

    private List<Player> _players { get; init; }

    public Roster(List<Player> list)
    {
        _players = list;
    }

    public int Count() => _players.Count;

    public double Average()
    {
        if (_avg < 0)
        {
            _avg = _players.Average(p => p.SkillAvg);
        }

        return _avg;
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
        _avg = -1;
    }
}