namespace FDSim.Models.Calendar;

public interface IEvent
{
    public EventType EventType { get; init; }
}