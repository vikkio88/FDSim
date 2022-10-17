namespace FDSim.Tests.Generators.DataProviders;

using FDSim.Generators;
using FDSim.Generators.DataProviders;
using FDSim.Models.Common;

public class PeopleMoneyRangeTests
{
    [Fact]
    public void PeopleMoneyRangeValuesForPlayersTest()
    {
        var pmr = new PeopleMoneyRange(new Dicer(0));

        var percs = new List<Perc> {
            new Perc(100),
            new Perc(95),
            new Perc(90),
            new Perc(80),
            new Perc(70),
            new Perc(60),
            new Perc(50),
            new Perc(40),
            new Perc(30),
        };

        var expected = new List<Money>
        {
            Money.MakeM(90),
            Money.MakeM(90),
            Money.MakeM(90),
            Money.MakeM(50),
            Money.MakeK(200),
            Money.MakeK(200),
            Money.MakeK(100),
            Money.MakeK(10),
            Money.MakeK(10),
        };
        for (int i = 0; i < percs.Count; i++)
        {
            var value = pmr.GetValue(percs[i]);
            Assert.True(value.Value >= expected[i].Value, $"{i} - I was expecting {percs[i].Value} to be >= {expected[i]}, instead it was {value.Value}");
        }
    }


    [Fact]
    public void PeopleMoneyRangeWagesForPlayersTest()
    {
        var pmr = new PeopleMoneyRange(new Dicer(0));

        var percs = new List<Perc> {
            new Perc(100),
            new Perc(95),
            new Perc(90),
            new Perc(80),
            new Perc(70),
            new Perc(60),
            new Perc(50),
            new Perc(40),
            new Perc(30),
        };

        var expected = new List<Money>
        {
            Money.MakeM(5), //100
            Money.MakeM(5), //95
            Money.MakeM(3), //90
            Money.MakeK(600), //80
            Money.MakeK(500), //70
            Money.MakeK(200), //60
            Money.MakeK(90), //50
            Money.MakeK(1), //40
            Money.MakeK(1), //30

        };
        for (int i = 0; i < percs.Count; i++)
        {
            var value = pmr.GetIdealWage(percs[i]);
            Assert.True(value.Value >= expected[i].Value, $"{i} - I was expecting {percs[i].Value} to be >= {expected[i]}, instead it was {value.Value}");
        }
    }


    [Fact]
    public void PeopleMoneyRangeWagesForCoachTest()
    {
        var pmr = new PeopleMoneyRange(new Dicer(0));

        var percs = new List<Perc> {
            new Perc(100),
            new Perc(95),
            new Perc(90),
            new Perc(80),
            new Perc(70),
            new Perc(60),
            new Perc(50),
            new Perc(40),
            new Perc(30),
        };

        var expected = new List<Money>
        {
            Money.MakeH(5), //100
            Money.MakeH(5), //95
            Money.MakeH(3), //90
            Money.MakeH(600), //80
            Money.MakeH(500), //70
            Money.MakeH(200), //60
            Money.MakeH(90), //50
            Money.MakeH(1), //40
            Money.MakeH(1), //30

        };
        for (int i = 0; i < percs.Count; i++)
        {
            var value = pmr.GetIdealWage(percs[i]);
            Assert.True(value.Value >= expected[i].Value, $"{i} - I was expecting {percs[i].Value} to be >= {expected[i]}, instead it was {value.Value}");
        }
    }
}
