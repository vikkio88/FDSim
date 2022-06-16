namespace FDSim.Models.Game.Team;

using System.Linq;

using FDSim.Models.People;
using FDSim.Models.Enums;
using FDSim.Models.Enums.Helpers;

public class Roster
{
    // -1 means that needs to be calculated
    private double _avg = -1;

    private List<Player> _players { get; init; }
    private Dictionary<Role, List<Player>>? _playersPerRole;

    public Roster(List<Player> list)
    {
        _players = list;
        IndexPlayers();
    }

    private void IndexPlayers()
    {
        _playersPerRole = new(){
            {Role.Goalkeeper, new()},
            {Role.Defender, new()},
            {Role.Midfielder, new()},
            {Role.Striker, new()},
        };

        foreach (var p in _players)
        {
            _playersPerRole[p.Role].Add(p);
        }
    }

    public List<Player>? GetByRole(Role role)
    {
        return _playersPerRole?[role] ?? null;
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

    public Dictionary<Role, int> AvailablePerRole()
    {
        var counter = RoleHelper.GetEmptyRoleCounter();

        foreach (var p in _players)
        {
            counter[p.Role] += 1;
        }

        return counter;
    }

    public Lineup GetLineup(Formation formation)
    {
        return Lineup.Make(_playersPerRole, formation);
    }
}