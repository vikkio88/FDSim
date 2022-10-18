namespace FDSim.Models.Game.Team;

using System.Linq;

using FDSim.Models.People;
using FDSim.Models.Enums;
using FDSim.Models.Enums.Helpers;

public class Roster : IdEntity
{
    // -1 means that needs to be calculated
    private double _avg = -1;

    // TODO: remove the Average function
    public double Avg { get => Average(); }

    private List<Player> _players { get; init; }
    public List<Player> Players { get => _players; }

    private Dictionary<String, Player>? _playersById;
    private Dictionary<String, Contract?>? _contracts;
    public Dictionary<String, Contract?>? Contracts { get => _contracts; }
    public Dictionary<String, Player>? IndexedPlayers { get => _playersById; }
    private Dictionary<Role, List<Player>>? _playersPerRole;

    public int Count { get => _players.Count; }

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

        _playersById = new();
        var oldContracts = _contracts is not null ? _contracts : null;
        _contracts = new();

        foreach (var p in _players)
        {
            _playersPerRole[p.Role].Add(p);
            _playersById[p.Id] = p;
            _contracts[p.Id] = (oldContracts?.ContainsKey(p.Id) ?? false) ? oldContracts[p.Id] : null;
        }
    }

    public Contract? GetContract(string playerId)
    {
        return _contracts?.GetValueOrDefault(playerId, null) ?? null;
    }

    public void SetContract(string playerId, Contract contract)
    {
        if (_contracts is not null)
        {
            _contracts[playerId] = contract;
        }
    }

    public List<Player>? GetByRole(Role role)
    {
        return _playersPerRole?[role] ?? null;
    }

    public double Average()
    {
        if (_avg < 0)
        {
            _avg = _players.Average(p => p.Skill.Value);
        }

        return _avg;
    }

    public void AddPlayer(Player player, Contract? contract = null)
    {
        _players.Add(player);
        if (contract is not null)
        {
            SetContract(player.Id, contract);
        }

        _avg = -1;
        IndexPlayers();
    }

    public Player? GetById(String id)
    {
        return (_playersById?.ContainsKey(id) ?? false) ? _playersById[id] : null;
    }


    public List<Player> GetByIds(List<String> ids)
    {
        var result = new List<Player>();
        foreach (var id in ids)
        {
            if (_playersById?.ContainsKey(id) ?? false)
            {
                result.Add(_playersById[id]);
            }
        }

        return result;
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