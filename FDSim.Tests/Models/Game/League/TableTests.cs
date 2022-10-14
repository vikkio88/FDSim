namespace FDSim.Tests.Models.Game.League;
using FDSim.Models.Game.League;
using FDSim.Models.Game.Team;

public class TableTests
{

    private MatchResult MR(int goalHome, int goalAway) => new MatchResult() { GoalHome = goalHome, GoalAway = goalAway };
    private Match M(Team home, Team away, MatchResult? result = null) => new Match() { Home = home, Away = away, Result = result };

    [Fact]
    public void TableBuildsCorrectlyTest()
    {
        var teams = new List<string>() { "juventus", "milan" };
        var table = new Table(teams);
        Assert.NotStrictEqual(table.OrderedTable, new List<TableRow>() { new("juventus"), new("milan") });
    }

    [Fact]
    public void TableUpdatesCorrectlyGivenMatchTest()
    {
        var teams = new List<string>() { "juventus", "milan" };
        var t1 = new Team() { Id = "juventus", Name = "Juventus" };
        var t2 = new Team() { Id = "milan", Name = "Milan" };
        var match = M(t1, t2, MR(1, 0));


        var table = new Table(teams);
        Assert.NotStrictEqual(table.OrderedTable, new List<TableRow>() { new("juventus"), new("milan") });

        table.Update(match);
        Assert.NotStrictEqual(table.OrderedTable, new List<TableRow>() { new("juventus"), new("milan") });
        Assert.Equal(3, table.OrderedTable[0].Points);
        Assert.Equal(1, table.OrderedTable[0].Played);
        Assert.Equal(0, table.OrderedTable[0].Drawn);
        Assert.Equal(0, table.OrderedTable[0].Lost);
        Assert.Equal(1, table.OrderedTable[0].Won);
        Assert.Equal(1, table.OrderedTable[0].GoalsScored);
        Assert.Equal(0, table.OrderedTable[0].GoalsConceded);

        Assert.Equal(0, table.OrderedTable[1].Points);
        Assert.Equal(1, table.OrderedTable[1].Played);
        Assert.Equal(0, table.OrderedTable[1].Drawn);
        Assert.Equal(1, table.OrderedTable[1].Lost);
        Assert.Equal(0, table.OrderedTable[1].Won);
        Assert.Equal(0, table.OrderedTable[1].GoalsScored);
        Assert.Equal(1, table.OrderedTable[1].GoalsConceded);

        match = M(t2, t1, MR(3, 0));
        table.Update(match);
        Assert.Equal(3, table.OrderedTable[0].Points);
        Assert.Equal(2, table.OrderedTable[0].Played);
        Assert.Equal(0, table.OrderedTable[0].Drawn);
        Assert.Equal(1, table.OrderedTable[0].Lost);
        Assert.Equal(1, table.OrderedTable[0].Won);
        Assert.Equal(3, table.OrderedTable[0].GoalsScored);
        Assert.Equal(1, table.OrderedTable[0].GoalsConceded);

        Assert.Equal(3, table.OrderedTable[1].Points);
        Assert.Equal(2, table.OrderedTable[1].Played);
        Assert.Equal(0, table.OrderedTable[1].Drawn);
        Assert.Equal(1, table.OrderedTable[1].Lost);
        Assert.Equal(1, table.OrderedTable[1].Won);
        Assert.Equal(1, table.OrderedTable[1].GoalsScored);
        Assert.Equal(3, table.OrderedTable[1].GoalsConceded);

        match = M(t1, t2, MR(0, 0));
        table.Update(match);
        Assert.Equal(4, table.OrderedTable[0].Points);
        Assert.Equal(3, table.OrderedTable[0].Played);
        Assert.Equal(1, table.OrderedTable[0].Drawn);
        Assert.Equal(1, table.OrderedTable[0].Lost);
        Assert.Equal(1, table.OrderedTable[0].Won);
        Assert.Equal(3, table.OrderedTable[0].GoalsScored);
        Assert.Equal(1, table.OrderedTable[0].GoalsConceded);

        Assert.Equal(4, table.OrderedTable[1].Points);
        Assert.Equal(3, table.OrderedTable[1].Played);
        Assert.Equal(1, table.OrderedTable[1].Drawn);
        Assert.Equal(1, table.OrderedTable[1].Lost);
        Assert.Equal(1, table.OrderedTable[1].Won);
        Assert.Equal(1, table.OrderedTable[1].GoalsScored);
        Assert.Equal(3, table.OrderedTable[1].GoalsConceded);


        // reset and simulate list of same matches
        table = new Table(teams);
        table.Update(
            new List<Match>() {
                M(t1, t2, MR(1, 0)),
                M(t2, t1, MR(3, 0)),
                M(t1, t2, MR(0, 0))
        });
        Assert.Equal(4, table.OrderedTable[0].Points);
        Assert.Equal(3, table.OrderedTable[0].Played);
        Assert.Equal(1, table.OrderedTable[0].Drawn);
        Assert.Equal(1, table.OrderedTable[0].Lost);
        Assert.Equal(1, table.OrderedTable[0].Won);
        Assert.Equal(3, table.OrderedTable[0].GoalsScored);
        Assert.Equal(1, table.OrderedTable[0].GoalsConceded);

        Assert.Equal(4, table.OrderedTable[1].Points);
        Assert.Equal(3, table.OrderedTable[1].Played);
        Assert.Equal(1, table.OrderedTable[1].Drawn);
        Assert.Equal(1, table.OrderedTable[1].Lost);
        Assert.Equal(1, table.OrderedTable[1].Won);
        Assert.Equal(1, table.OrderedTable[1].GoalsScored);
        Assert.Equal(3, table.OrderedTable[1].GoalsConceded);
    }
}
