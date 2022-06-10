namespace FDSim.Tests.Generators;

using FDSim.Generators;

using Mocks;


public class GameEntityGeneratorTest
{
    //[Fact(Skip = "Debugging Test")]
    [Fact]
    public void TeamGeneratorTest()
    {
        var gEG = new GameEntityGenerator(0, new MockedIdGen());

        foreach (int index in Enumerable.Range(0, 10))
        {
            var t = gEG.GetTeam();
            Console.WriteLine(t);
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
