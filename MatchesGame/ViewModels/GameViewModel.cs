namespace MatchesGame.ViewModels;

using ReactiveUI;
using System;
using System.Reactive;
using System.Collections.ObjectModel;
using FDSim.Models.Game.Team;
using FDSim.Generators;

public class GameViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public GameEntityGenerator _gEg;
    private int _seed = 0;
    public int Seed
    {
        get => _seed;
        set => this.RaiseAndSetIfChanged(ref _seed, value);
    }

    public string UrlPathSegment { get; } = "gameView";

    public ObservableCollection<Team> GeneratedTeams { get; set; } = Services.TeamsDb.Instance.GeneratedTeams;
    public ReactiveCommand<Unit, Unit> GenerateTeams { get; set; }
    public ReactiveCommand<string, Unit> RemoveTeam { get; set; }
    public ReactiveCommand<string, IRoutableViewModel> ViewTeam { get; set; }
    public ReactiveCommand<Unit, Unit> ChangeSeed { get; set; }
    public ReactiveCommand<Unit, IRoutableViewModel> StartLeague { get; set; }

    public GameViewModel(IScreen screen)
    {
        _gEg = new GameEntityGenerator(Seed);
        var generateEnabled = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x < 10
        );
        var startMatchesEnabled = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x > 0 && x % 2 == 0
        );
        HostScreen = screen;
        GenerateTeams = ReactiveCommand.Create(() => Services.TeamsDb.Instance.Add(_gEg.GetTeam()), generateEnabled);
        ChangeSeed = ReactiveCommand.Create(() =>
        {
            Seed = Dicer.Make().Int(0);
            _gEg = new(Seed);
        });
        RemoveTeam = ReactiveCommand.Create((string teamId) => Services.TeamsDb.Instance.RemoveById(teamId));
        ViewTeam = ReactiveCommand.CreateFromObservable((string teamId) => HostScreen.Router.Navigate.Execute(new TeamViewModel(HostScreen, teamId)));
        StartLeague = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new LeagueViewModel(HostScreen)), startMatchesEnabled);
    }
}
