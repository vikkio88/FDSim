namespace MatchesGame.Services;

using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MatchesGame.Abstracts;
using FDSim.Models.Game.Team;
public class TeamsDb : Singleton<TeamsDb>
{
    public ObservableCollection<Team> GeneratedTeams { get; set; } = new();
    public Dictionary<string, Team> TeamsMap { get; set; } = new();

    public void Add(Team team)
    {
        TeamsMap.Add(team.Id, team);
        GeneratedTeams.Add(team);
    }

    public Team GetById(string teamId)
    {
        return TeamsMap[teamId];
    }

    public void RemoveById(string teamId)
    {
        var t = GetById(teamId);
        GeneratedTeams.Remove(t);
    }
}
