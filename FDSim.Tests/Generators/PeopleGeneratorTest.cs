namespace FDSim.Tests.Generators;

using FDSim.Generators;

using Mocks;

public class PeopleGeneratorTest
{
    //[Fact]
    [Fact(Skip = "Debugging Test")]
    public void PlayerGeneratorTest()
    {
        var pg = new PeopleGenerator(0, new MockedIdGen());

        foreach (int index in Enumerable.Range(0, 10))
        {
            var p = pg.GetPlayer();
            Console.WriteLine(p);
            Console.WriteLine(p.Status);
        }
    }

    [Fact]
    public void PlayerGeneratorTestForced()
    {
        var pg = new PeopleGenerator(0, new MockedIdGen());


        var p = pg.GetPlayer(
            forcedNationality: FDSim.Models.Enums.Nationality.Italian,
            forcedRole: FDSim.Models.Enums.Role.Goalkeeper,
            forcedAge: 24,
            forcedSkillPercent: 90
            );
        Assert.True(p.Nationality == FDSim.Models.Enums.Nationality.Italian);
        Assert.True(p.Role == FDSim.Models.Enums.Role.Goalkeeper);
        Assert.True(p.Age == 24);
        Assert.True(p.Skill.Value >= 90);
    }

    [Fact]
    public void PlayeGeneratorChecksAgeOnSkillValueTest()
    {
        var pg = new PeopleGenerator(0, new MockedIdGen());
        var p15 = pg.GetPlayer(forcedAge: 15, forcedSkillPercent: 90);
        var p24 = pg.GetPlayer(forcedAge: 24, forcedSkillPercent: 90);
        var p40 = pg.GetPlayer(forcedAge: 40, forcedSkillPercent: 90);
        Assert.True(p15.Skill.Value < 70);
        Assert.True(p24.Skill.Value > 90);
        Assert.True(p40.Skill.Value < 80);
    }
}
