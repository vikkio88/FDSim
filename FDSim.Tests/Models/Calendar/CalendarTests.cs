namespace FDSim.Tests.Models.Calendar;
using FDSim.Models.Calendar;
public class CalendarTests
{
    [Fact]
    public void CalendarAdvancesTest()
    {
        var today = DateTime.Today;
        var cal = new Calendar(today);
        cal.Advance(TimeSpan.FromDays(3));
        var nextDate = DateTime.Today + TimeSpan.FromDays(3);

        Assert.Equal(nextDate.Day, cal.Date.Day);

    }

    [Fact]
    public void CalendarAdvanceDayTest()
    {
        var today = DateTime.Today;
        var cal = new Calendar(today);
        var tomorrow = DateTime.Today + TimeSpan.FromDays(1);

        Assert.Equal(tomorrow.Day, cal.NextDay().Day);
        Assert.Equal(tomorrow.Day, cal.Date.Day);
    }

    [Fact]
    public void CalendarAdvanceWeekTest()
    {
        var today = DateTime.Today;
        var cal = new Calendar(today);
        var nextWeek = DateTime.Today + TimeSpan.FromDays(7);

        Assert.Equal(nextWeek.Day, cal.NextWeek().Day);
        Assert.Equal(nextWeek.Day, cal.Date.Day);
    }
}
