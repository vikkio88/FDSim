using FDSim.Game.Services;

namespace FDSim.Game.ViewModels;

using ReactiveUI;
using System.Reactive;
using System.Collections.ObjectModel;
using FDSim.Models.Game.Team;
using FDSim.Generators;
using FDSim.Models.Enums;
using FDSim.Models.Enums.Helpers;
using System.Collections.Generic;
using System.Linq;
using Game.Services;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Avalonia.Threading;

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

    private string? _playerTeamId = null;
    public string? PlayerTeamId
    {
        get => _playerTeamId;
        set => this.RaiseAndSetIfChanged(ref _playerTeamId, value);
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

    private List<Team> _generatedTeams = new();
    public List<Team> GeneratedTeams
    {
        get => _generatedTeams;
        set => this.RaiseAndSetIfChanged(ref _generatedTeams, value);
    }
    public ReactiveCommand<Unit, Unit> GenerateTeams { get; }
    public ReactiveCommand<string?, Unit> SelectTeam { get; }
    public ReactiveCommand<string, Unit> RemoveTeam { get; }
    public ReactiveCommand<Unit, Unit> RemoveAllTeams { get; }
    public ReactiveCommand<string, IRoutableViewModel> ViewTeam { get; set; }
    public ReactiveCommand<Unit, Unit> ChangeSeed { get; set; }
    public ReactiveCommand<Unit, IRoutableViewModel> StartLeague { get; set; }

    const int MAX_TEAMS = 18;
    public bool _canClickGenerate = true;
    public bool CanClickGenerate { get => _canClickGenerate; set => this.RaiseAndSetIfChanged(ref _canClickGenerate, value); }

    public GameViewModel(IScreen screen)
    {
        HostScreen = screen;

        _gEg = new GameEntityGenerator(_seed);
        var canGenerate = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x < MAX_TEAMS
        );

        var isGenerating = this.WhenAnyValue(
            x => x.CanClickGenerate
        );
        canGenerate.CombineLatest(isGenerating);

        var startMatchesEnabled = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x > 0 && x % 2 == 0
        );

        GenerateTeams = ReactiveCommand.CreateFromTask(() =>
        {
            CanClickGenerate = false;
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                var teamsToGenerate = MAX_TEAMS - GeneratedTeams.Count;
                var list = new List<Team>();
                foreach (var _ in Enumerable.Range(0, teamsToGenerate))
                {
                    list.Add(_gEg.GetTeam(_teamNationality));
                }
                list.AddRange(GeneratedTeams);
                GeneratedTeams = list;
                GameDb.Instance.SetTeams(GeneratedTeams);
                CanClickGenerate = true;
            }, DispatcherPriority.Background);

            return Task.CompletedTask;

        }, canGenerate);

        ChangeSeed = ReactiveCommand.Create(() =>
        {
            _seed = Dicer.Make().Int(0);
            SeedText = $"{_seed}";
            _gEg = new(_seed);
        });

        RemoveAllTeams = ReactiveCommand.Create(() =>
        {
            GeneratedTeams = new();
        });

        RemoveTeam = ReactiveCommand.Create((string teamId) =>
        {
            GeneratedTeams = GeneratedTeams.Where(t => t.Id != teamId).ToList();
        });

        SelectTeam = ReactiveCommand.Create((string? teamId) =>
        {
            GameDb.Instance.PlayerTeamId = teamId;
            PlayerTeamId = teamId;
        });

        ViewTeam = ReactiveCommand.CreateFromObservable((string teamId) => HostScreen.Router.Navigate.Execute(new TeamViewModel(HostScreen, teamId)));
        StartLeague = ReactiveCommand.CreateFromObservable(() =>
        {
            GameDb.Instance.HasGameStarted = true;
            GameDb.Instance.StartingYear = System.DateTime.Now.Year;
            GameDb.Instance.CurrentYear = System.DateTime.Now.Year;
            return HostScreen.Router.Navigate.Execute(new LeagueViewModel(HostScreen));
        }, startMatchesEnabled);
    }
}
