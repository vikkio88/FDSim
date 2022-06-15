namespace FDSim.Tests.Models.Game.Team;


using FDSim.Models.People;
using FDSim.Models.Game.Team;
using FDSim.Models.Enums;
public class RosterTests
{
    [Fact]
    public void RosterPlayerIndexingTest()
    {
        var players = new List<Player>(){
            new Player("a", "b", 1, 10, Role.Goalkeeper),
            new Player("a", "b", 1, 10, Role.Defender),
            new Player("b", "a", 1, 20, Role.Defender),
            new Player("a", "b", 1, 10, Role.Midfielder),
            new Player("a", "b", 1, 10, Role.Stricker),
        };

        var roster = new Roster(players);
        Assert.Equal(2, roster.GetByRole(Role.Defender)?.Count ?? 0);
        Assert.Equal(1, roster.GetByRole(Role.Midfielder)?.Count ?? 0);
    }
    
    [Fact]
    public void RosterPlayerIndexingTestWithNull()
    {
        var players = new List<Player>(){
            new Player("a", "b", 1, 10, Role.Goalkeeper),
            new Player("a", "b", 1, 10, Role.Defender),
            new Player("b", "a", 1, 20, Role.Defender),
            new Player("a", "b", 1, 10, Role.Stricker),
        };

        var roster = new Roster(players);
        Assert.Equal(0, roster.GetByRole(Role.Midfielder)?.Count ?? 0);
    }

    [Fact]
    public void RosterPlayerPerRoleTest()
    {
        var players = new List<Player>(){
            new Player("a", "b", 1, Role.Goalkeeper),
            new Player("a", "b", 1, Role.Defender),
            new Player("b", "a", 1, Role.Defender),
            new Player("a", "b", 1, Role.Midfielder),
            new Player("a", "b", 1, Role.Stricker),
        };

        var roster = new Roster(players);

        var roles = roster.AvailablePerRole();
        Assert.Equal(1, roles[Role.Goalkeeper]);
        Assert.Equal(2, roles[Role.Defender]);
        Assert.Equal(1, roles[Role.Midfielder]);
        Assert.Equal(1, roles[Role.Stricker]);
    }
    
    
    [Fact]
    public void RosterCanPlayFormationTest()
    {
        var players = new List<Player>(){
            new Player("a", "b", 1, Role.Goalkeeper),
            new Player("a", "b", 1, Role.Defender),
            new Player("b", "a", 1, Role.Defender),
            new Player("a", "b", 1, Role.Midfielder),
            new Player("a", "b", 1, Role.Stricker),
        };

        var roster = new Roster(players);
        var lineup = roster.GetLineup(Formation._442);

        var roles = roster.AvailablePerRole();
        Assert.Equal(1, roles[Role.Goalkeeper]);
        Assert.Equal(2, roles[Role.Defender]);
        Assert.Equal(1, roles[Role.Midfielder]);
        Assert.Equal(1, roles[Role.Stricker]);
    }
}
