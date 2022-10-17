namespace FDSim.Tests.Models.Game.Team;

using FDSim.Models.Common;
using FDSim.Models.Game.Team;
public class ContractTests
{
    [Fact]
    public void ContractBuiledTest()
    {
        var c = new Contract() { Pay = Money.MakeM(.5) };
        Assert.NotStrictEqual(new Money(500_000), c.Pay);
    }
}
