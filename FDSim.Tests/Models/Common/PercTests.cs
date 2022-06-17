namespace FDSim.Tests.Models.Common;

using FDSim.Models.Common;
public class PercTest
{
    class Box
    {
        public Perc Val { get; init; }
    }
    [Fact]
    public void PercCompareWhilstBoxedTest()
    {
        var p1 = new Perc(10);
        var p2 = new Perc(100);

        var list = new List<Box> { new() { Val = p1 }, new() { Val = p2 } };

        list = list.OrderBy(b => b.Val).ToList();
        Assert.Equal(100, list[0].Val.Value);
    }

    [Fact]
    public void PercMaxIs100AndMinIs0()
    {
        var p = new Perc(700);
        Assert.Equal(100, p.Value);

        p.Value = -100;
        Assert.Equal(0, p.Value);
    }
}
