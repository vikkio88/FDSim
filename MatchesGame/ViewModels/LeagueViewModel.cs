namespace MatchesGame.ViewModels;


using ReactiveUI;
using System;
using System.Reactive;
using System.Collections.ObjectModel;
using FDSim.Models.Game.Team;
using FDSim.Generators;


public class LeagueViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "leagueView";

    public LeagueViewModel(IScreen screen) => HostScreen = screen;
}
