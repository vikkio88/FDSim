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
        new Player("a", "b", 1, 10, Role.Striker),
        new Player("a", "b", 1, 10, Role.Goalkeeper),
        };
        Roster r = new(list);
        var lineup = r.GetLineup(Formation._442);

        Assert.Equal(0, lineup.RolesMissing[Role.Goalkeeper]);
        Assert.Equal(3, lineup.RolesMissing[Role.Defender]);
        Assert.Equal(0, lineup.RolesMissing[Role.Midfielder]);
        Assert.Equal(1, lineup.RolesMissing[Role.Striker]);


        Assert.Equal(1, lineup.RolesAssigned[Role.Goalkeeper]);
        Assert.Equal(1, lineup.RolesAssigned[Role.Defender]);
        Assert.Equal(4, lineup.RolesAssigned[Role.Midfielder]);
        Assert.Equal(1, lineup.RolesAssigned[Role.Striker]);

        Assert.Equal(1, lineup.Bench.Count);
        
        Assert.Equal(10, lineup.AvgSkillPerRole[Role.Goalkeeper]);
        Assert.Equal(10, lineup.AvgSkillPerRole[Role.Defender]);
        Assert.Equal(10, lineup.AvgSkillPerRole[Role.Midfielder]);
        Assert.Equal(10, lineup.AvgSkillPerRole[Role.Striker]);
        
        var adjustedSkill = lineup.gtAdjustedAvgSkillPerRole();
        Assert.Equal(10, adjustedSkill[Role.Goalkeeper]);
        Assert.Equal(2.5, adjustedSkill[Role.Defender]);
        Assert.Equal(10, adjustedSkill[Role.Midfielder]);
        Assert.Equal(5, adjustedSkill[Role.Striker]);
    }
}
