namespace MatchesGame.ViewModels;

using ReactiveUI;
using System.Reactive;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FDSim.Models.Game.Team;
using FDSim.Generators;

public class GameViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public GameEntityGenerator _gEg;

    public string UrlPathSegment { get; } = "gameView";

    public ObservableCollection<Team> GeneratedTeams { get; set; } = new();
    public ReactiveCommand<Unit, Unit> GenerateTeams { get; set; }

    public GameViewModel(IScreen screen)
    {
        _gEg = new GameEntityGenerator(0);
        var generateEnabled = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x < 5
        );
        HostScreen = screen;
        GenerateTeams = ReactiveCommand.Create(() => GeneratedTeams.Add(_gEg.GetTeam()), generateEnabled);
    }
}
