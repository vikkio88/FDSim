namespace FDSim.Tests.Generators.DataProviders;

using FDSim.Generators;
using FDSim.Generators.DataProviders;
using FDSim.Models.Common;

public class ReputationRangeTests
{
    [Fact]
    public void ReputationRangeDoesItsThingTest()
    {
        var rr = new ReputationRange(new Dicer(0));
        Assert.True(new Perc(70).Value <= rr.GetReputation(new Perc(100)).Value);
        Assert.True(new Perc(35).Value <= rr.GetReputation(new Perc(50)).Value);
    }
}
