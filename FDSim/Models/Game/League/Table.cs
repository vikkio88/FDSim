namespace FDSim.Models.Game.League;
public class Table
{
    const int POINTS_WIN = 3;
    const int POINTS_DRAW = 1;
    public Dictionary<string, TableRow> _table;
    public List<TableRow> OrderedTable
    {
        get => _table.OrderByDescending(x => x.Value.Points)
        .ThenByDescending(x => x.Value.GoalsDiff)
        .Select((x, i) =>
        {
            x.Value.Position = i + 1;
            return x.Value;
        }).ToList();
    }
    public Table(List<string> teamIds)
    {
        _table = new();
        foreach (var tId in teamIds)
        {
            _table.Add(tId, new(tId));
        }
    }

    public TableRow? GetRow(string teamId)
    {
        if (!_table.ContainsKey(teamId)) return null;
        return _table[teamId];
    }

    public void Update(List<Match> matches)
    {

        foreach (var match in matches)
        {
            Update(match);
        }
    }
    public void Update(Match match)
    {
        if (!match.isFinished) return;


        var homeTeam = match.Home.Id;
        var awayTeam = match.Away.Id;
        if (!_table.ContainsKey(homeTeam) || !_table.ContainsKey(awayTeam)) return;

        var result = match.Result;

        _table[homeTeam].Played += 1;
        _table[awayTeam].Played += 1;

        if (result.GoalHome > result.GoalAway)
        {
            _table[homeTeam].Won += 1;
            _table[awayTeam].Lost += 1;
            _table[homeTeam].Points += POINTS_WIN;
        }
        else if (result.GoalHome == result.GoalAway)
        {
            _table[homeTeam].Drawn += 1;
            _table[awayTeam].Drawn += 1;
            _table[homeTeam].Points += POINTS_DRAW;
            _table[awayTeam].Points += POINTS_DRAW;
        }
        else
        {
            _table[awayTeam].Won += 1;
            _table[homeTeam].Lost += 1;
            _table[awayTeam].Points += POINTS_WIN;
        }

        _table[homeTeam].GoalsScored += result.GoalHome;
        _table[homeTeam].GoalsConceded += result.GoalAway;
        _table[awayTeam].GoalsScored += result.GoalAway;
        _table[awayTeam].GoalsConceded += result.GoalHome;

        // here I could also parse scorers for goals
    }
}

public class TableRow
{
    public int? Position { get; set; } = null;
    public string TeamId { get; set; } = string.Empty;
    public int Points { get; set; } = 0;
    public int Played { get; set; } = 0;
    public int Won { get; set; } = 0;
    public int Drawn { get; set; } = 0;
    public int Lost { get; set; } = 0;
    public int GoalsScored { get; set; } = 0;
    public int GoalsConceded { get; set; } = 0;
    public int GoalsDiff { get => GoalsScored - GoalsConceded; }
    public TableRow(string teamId)
    {
        TeamId = teamId;
    }

}
