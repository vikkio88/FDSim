namespace FDSim.Models.Calendar;
public class Calendar
{
    private DateTime _date;
    public DateTime Date { get => _date; }

    public Dictionary<int, (DateTime, DateTime)> WeeksMap { get; } = new();

    public Calendar(DateTime startingDate)
    {
        _date = startingDate;
        calculateWeeksMap();
    }

    private void calculateWeeksMap()
    {
        var startingDay = _date.DayOfWeek;
        var offsetForFirstWeek = startingDay switch
        {
            DayOfWeek.Monday => 0,
            DayOfWeek.Tuesday => -1,
            DayOfWeek.Wednesday => -2,
            DayOfWeek.Thursday => -3,
            DayOfWeek.Friday => -4,
            DayOfWeek.Saturday => -5,
            DayOfWeek.Sunday => -6,
        };

        var firstWeekDay = _date.Add(TimeSpan.FromDays(offsetForFirstWeek));
        foreach (var week in Enumerable.Range(0, 53))
        {
            var startingWeekDay = firstWeekDay.Add(TimeSpan.FromDays(7 * week));
            var finishingWeekDay = startingWeekDay.Add(TimeSpan.FromDays(6));
            WeeksMap.Add(week, (startingWeekDay, finishingWeekDay));
        }

    }

    public DateTime NextDay()
    {
        return Advance(TimeSpan.FromDays(1));
    }

    public DateTime NextWeek()
    {
        return Advance(TimeSpan.FromDays(7));
    }

    public DateTime Advance(TimeSpan time)
    {
        _date = _date.Add(time);
        return _date;
    }

}
