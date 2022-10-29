using System;
using System.Collections.Generic;
using FDSim.Game.Abstracts;
using ReactiveUI;

namespace FDSim.Game.Services;

public enum NavigableRoute
{
    Dashboard,
    League,
    Calendar,
    MatchDetails,
    PlayerDetails,
    TeamDetails,
    ManagerProfile,
}
public class ViewsStore : Singleton<ViewsStore>
{
    private Dictionary<NavigableRoute, IRoutableViewModel> _storage = new();

    public IRoutableViewModel? Get(NavigableRoute route, Func<IRoutableViewModel>? generator = null)
    {
        if (_storage.ContainsKey(route))
        {
            return _storage[route];
        }

        if (generator is not null)
        {
            _storage[route] = generator();

            return _storage[route];
        }

        System.Console.Error.WriteLine($"Cannot find in store nor generate viewmodel for {route}");

        return null;
    }
}
