namespace FDSim.Models.Calendar;
public class Calendar
{
    private DateTime _date;
    public DateTime Date { get => _date; }

    public Calendar(DateTime startingDate)
    {
        _date = startingDate;
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
