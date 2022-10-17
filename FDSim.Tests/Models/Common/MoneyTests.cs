namespace FDSim.Tests.Models.Common;
using FDSim.Models.Common;
public class MoneyTests
{
    [Fact]
    public void MoneyDoesItsThingTest()
    {
        var hund = new Money(100.0);
        var thou = new Money(1000.0);
        var mil = new Money(1_000_000.0);
        var bil = new Money(1_000_000_000.0);
        Assert.Equal("£ 100", hund.ToString());
        Assert.Equal("£ 1k", thou.ToString());
        Assert.Equal("£ 1m", mil.ToString());
        Assert.Equal("£ 1b", bil.ToString());
    }

    [Fact]
    public void MoneyCanBeBuildWithADifferentCurrencyTest()
    {
        var hund = new Money(100.0, "$");
        var thou = new Money(1000.0, "€");
        Assert.Equal("$ 100", hund.ToString());
        Assert.Equal("€ 1k", thou.ToString());
    }

    [Fact]
    public void MoneyOperationTest()
    {
        var a = new Money(100.0, "$");
        var b = new Money(300.0, "$");

        Assert.NotStrictEqual(new(200.0, "$"), b - a);
        Assert.NotStrictEqual(new(400.0, "$"), b + a);
        Assert.NotStrictEqual(new(-200.0, "$"), a - b);
        Assert.NotStrictEqual(new(0, "$"), a - a);
    }


    [Fact]
    public void MoneyBuildMKFromStaticTest()
    {
        Assert.Equal("£ 100k", Money.MakeK(100).ToString());
        Assert.Equal("£ 9m", Money.MakeM(9).ToString());
    }
}
