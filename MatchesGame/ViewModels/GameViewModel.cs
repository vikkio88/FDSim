namespace MatchesGame.ViewModels;

using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using FDSim.Models.Game.Team;
using FDSim.Generators;
using FDSim.Models.Enums;
using FDSim.Models.Enums.Helpers;
using System.Collections.Generic;

public class GameViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public GameEntityGenerator _gEg;
    private int _seed = 0;
    private string _seedText = "Change Seed";
    public string SeedText
    {
        get => _seedText;
        set
        {
            this.RaiseAndSetIfChanged(ref _seedText, $"Seed: {value}");
        }
    }

    public List<string> Nations
    {
        get
        {
            var nations = NationalityHelper.GetNations();
            nations.Add("Any");
            return nations;
        }
    }

    private Nationality? _teamNationality = null;
    private string _selectedNation = "Any";
    public string SelectedNation
    {
        get => _selectedNation;
        set
        {
            _teamNationality = NationalityHelper.FromNationString(value);
            _selectedNation = value;
        }
    }
    public string UrlPathSegment { get; } = "gameView";

    public ObservableCollection<Team> GeneratedTeams { get; set; } = Services.GameDb.Instance.GeneratedTeams;
    public ReactiveCommand<Unit, Unit> GenerateTeams { get; set; }
    public ReactiveCommand<string, Unit> RemoveTeam { get; set; }
    public ReactiveCommand<string, IRoutableViewModel> ViewTeam { get; set; }
    public ReactiveCommand<Unit, Unit> ChangeSeed { get; set; }
    public ReactiveCommand<Unit, IRoutableViewModel> StartLeague { get; set; }

    public GameViewModel(IScreen screen)
    {
        _gEg = new GameEntityGenerator(_seed);
        var generateEnabled = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x < 18
        );
        var startMatchesEnabled = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x > 0 && x % 2 == 0
        );
        HostScreen = screen;
        GenerateTeams = ReactiveCommand.Create(() => Services.GameDb.Instance.AddTeam(_gEg.GetTeam(_teamNationality)), generateEnabled);
        ChangeSeed = ReactiveCommand.Create(() =>
        {
            _seed = Dicer.Make().Int(0);
            SeedText = $"{_seed}";
            _gEg = new(_seed);
        });
        RemoveTeam = ReactiveCommand.Create((string teamId) => Services.GameDb.Instance.RemoveTeamById(teamId));
        ViewTeam = ReactiveCommand.CreateFromObservable((string teamId) => HostScreen.Router.Navigate.Execute(new TeamViewModel(HostScreen, teamId)));
        StartLeague = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new LeagueViewModel(HostScreen)), startMatchesEnabled);
    }
}
