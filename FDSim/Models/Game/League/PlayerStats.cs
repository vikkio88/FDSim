namespace FDSim.Models.Game.League;
public class PlayerStats
{
    const int MAX_SCORERS = 10;
    public Dictionary<string, StatRow> _stats;
    public Dictionary<string, StatRow> Stats { get => _stats; }

    public List<StatRow> OrderedScorers
    {
        get => _stats.OrderByDescending(x => x.Value.Goals)
        .ThenBy(x => x.Value.Played)
        .Select((x, i) =>
        {
            x.Value.Position = i + 1;
            return x.Value;
        })
        .Where(x => x.Goals > 0)
        .Take(MAX_SCORERS).ToList();
    }
    public PlayerStats()
    {
        _stats = new();
    }

    public void Update(Match match)
    {
        var result = match.Result;
        var homeLineup = result?.HomeLineup ?? new();
        var homeScorers = result?.ScorersHomeIds ?? new();


        var awayLineup = result?.AwayLineup ?? new();
        var awayScorers = result?.ScorersAwayIds ?? new();

        Update(homeLineup, homeScorers, match.Home.Id);
        Update(awayLineup, awayScorers, match.Away.Id);
    }

    public void Update(List<string> lineup, List<string>? scorersIds, string? teamId = null)
    {
        foreach (var p in lineup)
        {
            if (_stats.ContainsKey(p))
            {
                _stats[p].Played += 1;
                // this might be pointless if I index players by themselves
                _stats[p].TeamId = teamId;
                continue;
            }
            // maybe lineup will also have ratings here
            // _statsp[p.Id].Rating calculate

            _stats.Add(p, new() { PlayerId = p, Played = 1, TeamId = teamId });

        }

        if (scorersIds is null) return;

        foreach (var p in scorersIds)
        {
            if (_stats.ContainsKey(p))
            {
                _stats[p].Goals += 1;
                continue;
            }
            _stats.Add(p, new() { PlayerId = p, Goals = 1, TeamId = teamId });
        }
    }
}

public class StatRow
{
    public int? Position { get; set; } = null;
    public string PlayerId { get; set; } = string.Empty;
    public string? TeamId { get; set; } = null;
    public int Goals { get; set; } = 0;
    public int Played { get; set; } = 0;
    public double Rate { get; set; } = 0.0;

}
