using FDSim.Game.Abstracts;
using FDSim.Game.Models;

namespace FDSim.Game.Services;

using System.Collections.Generic;
using Game.Abstracts;
using FDSim.Models.People;
using FDSim.Models.Game.Team;
using FDSim.Models.Game.League;
using System;
using Game.Models;

public class GameDb : Singleton<GameDb>
{
    public int StartingYear { get; set; } = DateTime.Now.Year;
    public int CurrentYear { get; set; } = DateTime.Now.Year;
    public bool HasGameStarted { get; set; } = false;
    public string? PlayerTeamId { get; set; } = null;
    public Dictionary<string, Team> TeamsMap { get; set; } = new();
    public Dictionary<string, TeamStat> TeamStatsMap { get; set; } = new();
    public Dictionary<string, Match> MatchesMap { get; set; } = new();
    public League? League { get; set; }
    public Dictionary<string, MatchResult> ResultMap { get; set; } = new();

    public Match MakeMatch(MatchPlaceholder match) => Match.Make(match, TeamsMap);

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

    public void SetTeams(List<Team> teams)
    {
        TeamsMap.Clear();
        foreach (var team in teams)
        {
            TeamsMap.Add(team.Id, team);
            TeamStatsMap.Add(team.Id, new());
        }
    }

    public Team GetTeamById(string teamId)
    {
        return TeamsMap[teamId];
    }

    public Player? GetPlayerFromTeamById(string teamId, string playerId)
    {
        return TeamsMap[teamId]?.Roster?.GetById(playerId);
    }

    public (Team?, Player?) GetPlayerAndTeamById(string teamId, string playerId)
    {
        var team = TeamsMap.ContainsKey(teamId) ? TeamsMap[teamId] : null;
        var player = team?.Roster?.GetById(playerId);

        return (team, player);
    }

    public TableRow? GetTableRowByTeamId(string teamId)
    {
        return League?.Table.GetRow(teamId) ?? null;
    }
    public TeamStat? GetSeasonStatHistoryByTeamId(string teamId)
    {
        if (!TeamStatsMap.ContainsKey(teamId))
            return null;

        return TeamStatsMap[teamId];
    }

    public void OnAfterRound(League league)
    {
        // after each round
        foreach (var row in league.Table.OrderedTable)
        {
            if (TeamStatsMap.ContainsKey(row.TeamId))
            {
                TeamStatsMap[row.TeamId].Positions.Add(row.Position ?? 0);
            }
        }
    }
}