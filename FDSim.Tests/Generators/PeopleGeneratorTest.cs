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
            forcedMaxAge: 15,
            forcedSkillPercent: 90
            );
        Assert.True(p.Nationality == FDSim.Models.Enums.Nationality.Italian);
        Assert.True(p.Role == FDSim.Models.Enums.Role.Goalkeeper);
        Assert.True(p.Age == 15);
        Assert.True(p.SkillAvg.Value >= 90);

    }
}
