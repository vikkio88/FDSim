namespace FDSim.Tests.Models.Game.League;

using FDSim.Models.Game.League;

public class LeagueTests
{
    [Fact]
    public void LeagueGenerationTest()
    {
        List<string> teams = new() { "Juventus", "Milan" };
        var league = League.Make(teams);
        Assert.Equal(2, league.Rounds.Count);
        Assert.Equal(1, league.Rounds[0].Matches.Count);
        Assert.Equal(1, league.Rounds[1].Matches.Count);

        Assert.Equal("Juventus", league.Rounds[0].Matches[0].HomeId);
        Assert.Equal("Milan", league.Rounds[0].Matches[0].AwayId);

        Assert.Equal("Milan", league.Rounds[1].Matches[0].HomeId);
        Assert.Equal("Juventus", league.Rounds[1].Matches[0].AwayId);

    }

    [Fact]
    public void LeagueGenerationTestWithManyTeams()
    {
        List<string> teams = new() { "Juventus", "Milan", "Inter", "Lazio" };
        var league = League.Make(teams);
        Assert.Equal(6, league.Rounds.Count);
        Assert.Equal(2, league.Rounds[0].Matches.Count);
        Assert.Equal(2, league.Rounds[1].Matches.Count);
    }
}
