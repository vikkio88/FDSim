namespace FDSim.Tests.Models.Game.League;

using FDSim.Generators;
using FDSim.Models.Game.League;

public class PlayerStatsTests
{
    private GameEntityGenerator geG = new(0);

    [Fact]
    public void PlayerStatsDoesUpdateStatsCorrectly()
    {
        var t1 = geG.GetTeam();
        var t2 = geG.GetTeam();
        var match = new Match()
        {
            Home = t1,
            Away = t2,
        };

        match.Simulate(new Dicer(0));

        var stats = new PlayerStats();
        stats.Update(match);


        Assert.Contains(match?.Result.HomeLineup[0], stats.Stats.Select(x => x.Key));
        Assert.Contains(match?.Result.AwayLineup[0], stats.Stats.Select(x => x.Key));

        Assert.True(stats.OrderedScorers.Count < 10);
        if (match.Result.GoalAway > 0)
        {
            Assert.Contains(match.Result?.ScorersAwayIds?[0], stats.OrderedScorers.Select(x => x.PlayerId));
        }

        if (match.Result.GoalHome > 0)
        {
            Assert.Contains(match.Result?.ScorersHomeIds?[0], stats.OrderedScorers.Select(x => x.PlayerId));
        }
    }
}
