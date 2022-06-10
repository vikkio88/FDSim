namespace FDSim.Tests.Generators;

using FDSim.Generators;


public class DicerTest
{
    [Fact]
    public void DicerChanceTest()
    {
        var dicer = new Dicer(0);
        Assert.True(dicer.Chance(100));
        Assert.False(dicer.Chance(0));
    }


    [Fact(Skip = "Debugging Test")]
    public void DicerChanceMultipleTest()
    {
        var dicer = new Dicer(0);
        foreach (int _ in Enumerable.Range(0, 100))
        {
            Console.WriteLine(dicer.Chance(50) ? "Yup" : "Nope");
        }
    }
}
