namespace FDSim.Tests.Generators.Utils;
using FDSim.Generators.Utils;

class Stuff
{
    public int Thing { get; set; }
}
public class WeightedOptionsHelperTests
{
    [Fact]
    public void WeightedOptionsHelperDoesItsThingTest()
    {
        var weights = new List<int>() { 1, 2, 300 };
        var things = new List<Stuff>(){
            new(){Thing=1},
            new(){Thing=2},
            new(){Thing=3},
        };

        var result = WeightedOptionsHelper.MakeList(things, weights);

        Assert.Equal(1, result[0].Item.Thing);
        Assert.Equal(1, result[0].Weight);


        Assert.Equal(2, result[1].Item.Thing);
        Assert.Equal(2, result[1].Weight);


        Assert.Equal(3, result[2].Item.Thing);
        Assert.Equal(300, result[2].Weight);

    }
}
