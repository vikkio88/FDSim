using System.Globalization;

namespace FDSim.Models.Calendar;
public class Calendar
{
    // League starts the last week of August
    public static int LEAGUE_STARTING_WEEK = 8;
    private int _weekPointer = 0;
    public int WeekPointer => _weekPointer;
    private GregorianCalendar _cal = new();
    private DateTime _date;
    public DateTime Date { get => _date; }

    public Dictionary<int, (DateTime, DateTime)> WeeksMap { get; } = new();

    public int? GetLeagueWeekIndex
    {
        get
        {
            if (_weekPointer < LEAGUE_STARTING_WEEK)
            {
                return null;
            }

            return _weekPointer - LEAGUE_STARTING_WEEK;
        }
    }

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

        var initialDate = _date;
        _date = _date.Add(time);

        var initialWeek = _cal.GetWeekOfYear(initialDate, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        var finalWeek = _cal.GetWeekOfYear(_date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        _weekPointer += finalWeek - initialWeek;

        return _date;
    }



}
