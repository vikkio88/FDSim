namespace FDSim.Tests.Generators;

using FDSim.Generators;

using Mocks;


public class GameEntityGeneratorTest
{
    //[Fact]
    [Fact(Skip = "Debugging Test")]
    public void TeamGeneratorTest()
    {
        var gEG = new GameEntityGenerator(0, new MockedIdGen());

        foreach (int index in Enumerable.Range(0, 10))
        {
            var t = gEG.GetTeam();
            Console.WriteLine(t);
            Console.WriteLine(t.Roster?.Average());

        }
    }

    [Fact]
    public void TeamGeneratorTestForced()
    {
        var gEG = new GameEntityGenerator(0, new MockedIdGen());

        var t = gEG.GetTeam(forcedNationality: FDSim.Models.Enums.Nationality.Italian);
        Assert.True(t.Nationality == FDSim.Models.Enums.Nationality.Italian);
    }
}
