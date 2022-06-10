namespace FDSim.Tests.Models.People;

using FDSim.Models.People;
using FDSim.Models.Enums;


public class PlayerTest
{
    [Fact]
    public void ConstructorTest()
    {
        var p = new Player("a", "b", 34, Role.Goalkeeper);

        Assert.True(p.Name == "a");
        Assert.True(p.Surname == "b");
        Assert.True(p.Role == Role.Goalkeeper);
    }
}
