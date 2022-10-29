using System.Collections.Generic;
using FDSim.Game.Abstracts;
using ReactiveUI;

namespace FDSim.Game.Services;

public enum NavigableRoute
{
    Dashboard,
    League,
    MatchDetails,
    PlayerDetails,
    TeamView,
}
public class ViewStore : Singleton<ViewStore>
{
    private Dictionary<NavigableRoute, IRoutableViewModel> _storage = new();
    public IRoutableViewModel? Get(NavigableRoute route)
    {
        if (_storage.ContainsKey(route))
        {
            return _storage[route];
        }

        return null;
    }
    public void Store(NavigableRoute route, IRoutableViewModel view)
    {
        _storage.Add(route, view);
    }
}
