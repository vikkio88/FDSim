namespace MatchesGame.Services;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using MatchesGame.Abstracts;
using FDSim.Models.Game.Team;
using FDSim.Models.Game.League;
public class GameDb : Singleton<GameDb>
{
    public ObservableCollection<Team> GeneratedTeams { get; set; } = new();
    public Dictionary<string, Team> TeamsMap { get; set; } = new();
    public Dictionary<string, Match> MatchesMap { get; set; } = new();

    public List<Match> MakeMatches(Round round)
    {
        var matches = new List<Match>();

        foreach (var m in round.Matches)
        {
            var match = Match.Make(m, TeamsMap);
            matches.Add(match);
            MatchesMap.Add(match.Id, match);
        }

        return matches;
    }

    public Match GetMatchById(string matchId)
    {
        return MatchesMap[matchId];
    }

    public void AddTeam(Team team)
    {
        TeamsMap.Add(team.Id, team);
        GeneratedTeams.Add(team);
    }

    public Team GetTeamById(string teamId)
    {
        return TeamsMap[teamId];
    }

    public void RemoveTeamById(string teamId)
    {
        var t = GetTeamById(teamId);
        GeneratedTeams.Remove(t);
    }
}
