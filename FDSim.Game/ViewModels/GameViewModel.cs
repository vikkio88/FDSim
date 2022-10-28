namespace FDSim.Game.ViewModels;

using System;
using ReactiveUI;
using System.Reactive;
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
    public int SelectedTab { get; set; } = 0;
    private string? _playerFullName = "Mario Rossi";
    public string? PlayerFullName
    {
        get => _playerFullName;
        set => this.RaiseAndSetIfChanged(ref _playerFullName, value);
    }
    private DateTime _playerDOB = new(1988, 05, 01);
    public DateTime PlayerDOB
    {
        get => _playerDOB;
        set => this.RaiseAndSetIfChanged(ref _playerDOB, value);
    }

    private string? _playerTeamId = null;
    public string? PlayerTeamId
    {
        get => _playerTeamId;
        set => this.RaiseAndSetIfChanged(ref _playerTeamId, value);
    }

    public List<string> Nationalities
    {
        get
        {
            var nations = NationalityHelper.GetNationalities();
            return nations;
        }
    }
    private string _selectedPlayerNationality = "Italian";
    public string SelectedPlayerNationality
    {
        get => _selectedPlayerNationality;
        set => this.RaiseAndSetIfChanged(ref _selectedPlayerNationality, value);
    }

    public List<string> Nations
    {
        get
        {
            var nations = NationalityHelper.GetNations();
            nations.Add("Any Country");
            return nations;
        }
    }

    private Nationality? _generatedTeamsCountry = Nationality.Italian;
    private string _selectedNation = "Italy";
    public string SelectedNation
    {
        get => _selectedNation;
        set
        {
            _generatedTeamsCountry = NationalityHelper.FromNationString(value);
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

        var canDelete = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x > 1
        );

        var isGenerating = this.WhenAnyValue(
            x => x.CanClickGenerate
        );

        // check player readiness

        canGenerate.CombineLatest(isGenerating);

        var startMatchesEnabled = this.WhenAnyValue(
            x => x.GeneratedTeams.Count,
            x => x > 0 && x % 2 == 0
        );

        GenerateTeams = ReactiveCommand.CreateFromTask(() =>
        {
            CanClickGenerate = false;
            var task = new Task(() =>
            {
                var teamsToGenerate = MAX_TEAMS - GeneratedTeams.Count;
                var list = new List<Team>();
                foreach (var _ in Enumerable.Range(0, teamsToGenerate))
                {
                    list.Add(_gEg.GetTeam(_generatedTeamsCountry));
                }
                list.AddRange(GeneratedTeams);
                GameDb.Instance.SetTeams(list);
                Dispatcher.UIThread.InvokeAsync(() =>
                {
                    GeneratedTeams = list;
                    CanClickGenerate = true;
                }, DispatcherPriority.Background);
            });
            task.Start();

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
        }, canDelete);

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
            var thisYear = DateTime.Now.Year;
            GameDb.Instance.HasGameStarted = true;
            GameDb.Instance.StartingYear = thisYear;
            GameDb.Instance.GameDate = new(thisYear, 7, 1);// starts always in july this year
            GameDb.Instance.GamePlayer = new(PlayerFullName, thisYear - PlayerDOB.Year);
            return HostScreen.Router.Navigate.Execute(new DashboardViewModel(HostScreen));
        }, startMatchesEnabled);
    }
}
