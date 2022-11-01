using FDSim.Generators;
using FDSim.Models.Game;

namespace FDSim.Models.Calendar;
public class EventDispatcher
{
    private Dicer _dicer;
    public EventDispatcher(Dicer dicer)
    {
        _dicer = dicer;
    }


    public List<Event> GetEvents(DateTime date, GameState gameState)
    {
        var events = new List<Event>();
        return events;
    }


}
