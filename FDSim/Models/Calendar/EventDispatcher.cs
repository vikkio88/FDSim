using FDSim.Generators;
using FDSim.Models.Game;

namespace FDSim.Models.Calendar;
public class EventDispatcher
{
    private IDicer _dicer;
    public EventDispatcher(IDicer dicer)
    {
        _dicer = dicer;
    }


    public List<Event> GetEvents(Calendar calendar, GameState gameState)
    {
        var events = new List<Event>();

        return events;
    }


}
