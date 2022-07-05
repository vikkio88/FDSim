namespace MatchesGame.Services;

using System.Linq;
using System.Collections.ObjectModel;
using MatchesGame.Abstracts;
using FDSim.Models.Game.Team;
public class TeamsDb : Singleton<TeamsDb>
{
    public ObservableCollection<Team> GeneratedTeams { get; set; } = new();

    public Team GetById(string teamId)
    {
        return GeneratedTeams.First(t => t.Id == teamId);
    }

    public void RemoveById(string teamId)
    {
        var t = GetById(teamId);
        GeneratedTeams.Remove(t);
    }
}
