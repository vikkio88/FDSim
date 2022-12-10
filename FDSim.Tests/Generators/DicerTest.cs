namespace FDSim.Tests.Generators;

using FDSim.Generators;


class Thing
{
    public int Id { get; set; }
    public Thing(int id) { Id = id; }
}

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

    [Fact(Skip = "Debugging Test")]
    public void TestWeightedPicker()
    {
        var dicer = new Dicer(0);
        var picks = (0, 0, 0);
        foreach (var _ in Enumerable.Range(0, 1000))
        {
            var pick = dicer.PickWeighted(
                new List<Thing>(){
                    new Thing(1),
                    new Thing(2),
                    new Thing(3),
                },
                new List<int>(){
                    1,
                    1,
                    1000
                }
            );

            switch (pick.Id)
            {
                case 1:
                    picks.Item1 += 1;
                    break;
                case 2:
                    picks.Item2 += 1;
                    break;
                case 3:
                    picks.Item3 += 1;
                    break;
            }
        }
        Assert.True(picks.Item1 < picks.Item3, $"Third pick was not greater than the first ({picks.Item1} < {picks.Item3})");
        Assert.True(picks.Item2 < picks.Item3, $"Third pick was not greater than the second ({picks.Item2} < {picks.Item3})");
    }
}
