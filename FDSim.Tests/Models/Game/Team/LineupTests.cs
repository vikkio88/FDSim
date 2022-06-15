namespace FDSim.Tests.Models.Game.Team;

using FDSim.Models.Game.Team;
using FDSim.Models.People;
using FDSim.Models.Enums;

public class LineupTests
{
    [Fact]
    public void LineupGenerationTests()
    {
        var list = new List<Player>(){
        new Player("a", "b", 1, 10, Role.Goalkeeper),
        new Player("a", "b", 1, 10, Role.Defender),
        new Player("a", "b", 1, 10, Role.Midfielder),
        new Player("a", "b", 1, 10, Role.Midfielder),
        new Player("a", "b", 1, 10, Role.Midfielder),
        new Player("a", "b", 1, 10, Role.Midfielder),
        new Player("a", "b", 1, 10, Role.Stricker),
        new Player("a", "b", 1, 10, Role.Goalkeeper),
        };
        Roster r = new(list);
        var lineup = r.GetLineup(Formation._442);

        Assert.Equal(0, lineup.RolesMissing[Role.Goalkeeper]);
        Assert.Equal(3, lineup.RolesMissing[Role.Defender]);
        Assert.Equal(0, lineup.RolesMissing[Role.Midfielder]);
        Assert.Equal(1, lineup.RolesMissing[Role.Stricker]);


        Assert.Equal(1, lineup.RolesAssigned[Role.Goalkeeper]);
        Assert.Equal(1, lineup.RolesMissing[Role.Defender]);
        Assert.Equal(4, lineup.RolesMissing[Role.Midfielder]);
        Assert.Equal(1, lineup.RolesMissing[Role.Stricker]);

        Assert.Equal(1, lineup.Bench.Count);


    }
}
