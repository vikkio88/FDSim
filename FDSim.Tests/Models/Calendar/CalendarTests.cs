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

    [Fact]
    public void CalendarWeeksMapCalculationTest()
    {
        var firstDay = new DateTime(2022, 7, 1); // Friday 01/07/2022
        var cal = new Calendar(firstDay);

        Assert.Equal(53, cal.WeeksMap.Count);

        Assert.Equal(new DateTime(2022, 06, 27), cal.WeeksMap[0].Item1);
        Assert.Equal(new DateTime(2022, 7, 3), cal.WeeksMap[0].Item2);

        foreach (var (_, (weekStart, weekEnd)) in cal.WeeksMap)
        {
            Assert.Equal(DayOfWeek.Monday, weekStart.DayOfWeek);
            Assert.Equal(DayOfWeek.Sunday, weekEnd.DayOfWeek);
        }

        Assert.Equal(6, cal.WeeksMap[52].Item1.Month); //starts in june
        Assert.Equal(26, cal.WeeksMap[52].Item1.Day); //starts in the last week of June
        Assert.Equal(2023, cal.WeeksMap[52].Item1.Year); //finishes in 2023

        Assert.Equal(7, cal.WeeksMap[52].Item2.Month); //finishes in july
        Assert.Equal(2, cal.WeeksMap[52].Item2.Day); //starts in the last week of June
        Assert.Equal(2023, cal.WeeksMap[52].Item2.Year); //finishes in 2023
    }

    [Fact]
    public void CalendarWeeksMapCalculationNextYearsTest()
    {
        var firstDay = new DateTime(2023, 7, 1);
        var cal = new Calendar(firstDay);

        Assert.Equal(53, cal.WeeksMap.Count);

        Assert.Equal(new DateTime(2023, 06, 26), cal.WeeksMap[0].Item1);
        Assert.Equal(new DateTime(2023, 7, 2), cal.WeeksMap[0].Item2);

        Assert.Equal(6, cal.WeeksMap[52].Item1.Month); //finishes start of week in june
        Assert.Equal(2024, cal.WeeksMap[52].Item1.Year); //finishes in 2024

        Assert.Equal(6, cal.WeeksMap[52].Item2.Month); //finishes in july
        Assert.Equal(2024, cal.WeeksMap[52].Item2.Year); //finishes in 2023
    }

    [Fact]
    public void CalendarWeekWhereItStartsSeasonIsAlwaysInAugust()
    {
        List<Calendar> cals = new();
        foreach (var i in Enumerable.Range(0, 100))
        {
            cals.Add(new Calendar(new(2022 + i, 7, 1)));
        }

        foreach (var cal in cals)
        {
            Assert.True(8 == cal.WeeksMap[Calendar.LEAGUE_STARTING_WEEK].Item1.Month, $"Start of {Calendar.LEAGUE_STARTING_WEEK}th week on year {cal.WeeksMap[Calendar.LEAGUE_STARTING_WEEK].Item1.Year} - {cal.WeeksMap[Calendar.LEAGUE_STARTING_WEEK].Item1}");
            Assert.True(8 <= cal.WeeksMap[Calendar.LEAGUE_STARTING_WEEK].Item2.Month, $"End of {Calendar.LEAGUE_STARTING_WEEK}th week on year {cal.WeeksMap[Calendar.LEAGUE_STARTING_WEEK].Item2.Year} - {cal.WeeksMap[Calendar.LEAGUE_STARTING_WEEK].Item2}");
        }
    }

    [Fact]
    public void CalendarGetRoundIndexGivenWeek()
    {
        var initialDate = new DateTime(2022, 07, 01);
        var cal = new Calendar(initialDate);

        cal.Advance(TimeSpan.FromDays(1));
        Assert.Equal(null, cal.GetLeagueWeekIndex);
        Assert.Equal(0, cal.WeekPointer);

        cal.Advance(TimeSpan.FromDays(6));
        Assert.Equal(1, cal.WeekPointer);
        Assert.Equal(null, cal.GetLeagueWeekIndex);

        Assert.Equal(initialDate.DayOfWeek, cal.Date.DayOfWeek);

        cal.NextWeek();
        Assert.Equal(2, cal.WeekPointer);
        Assert.Equal(null, cal.GetLeagueWeekIndex);
        Assert.Equal(initialDate.DayOfWeek, cal.Date.DayOfWeek);


        cal.Advance(TimeSpan.FromDays(6 * 7)); // 6 weeks so it is the 8th week
        Assert.Equal(8, cal.WeekPointer);
        Assert.Equal(0, cal.GetLeagueWeekIndex);
        Assert.Equal(initialDate.DayOfWeek, cal.Date.DayOfWeek);

        cal.Advance(TimeSpan.FromDays(5 * 7)); // 5 weeks more
        Assert.Equal(13, cal.WeekPointer);
        Assert.Equal(5, cal.GetLeagueWeekIndex);
        Assert.Equal(initialDate.DayOfWeek, cal.Date.DayOfWeek);
    }
}
