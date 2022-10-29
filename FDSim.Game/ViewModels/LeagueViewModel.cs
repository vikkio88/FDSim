using FDSim.Game.Services;

namespace FDSim.Game.ViewModels;


using ReactiveUI;
using System.Linq;
using System.Collections.Generic;
using System.Reactive;
using FDSim.Models.Game.Team;
using FDSim.Models.Game.League;
using Game.Services;
using Avalonia.Threading;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

public class LeagueViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public League? League { get => GameDb.Instance.League; set => GameDb.Instance.League = value; }

    private ObservableCollection<Round> PlayedRounds { get; } = new();
    public ObservableCollection<Round>? UnPlayedRounds { get; } = null;

    private List<TableRow>? _leagueTable = null;
    public List<TableRow>? LeagueTable
    {
        get => _leagueTable;
        set
        {
            this.RaiseAndSetIfChanged(ref _leagueTable, value);
        }
    }

    private List<StatRow>? _scorers = null;
    public List<StatRow>? Scorers
    {
        get => _scorers;
        set
        {
            this.RaiseAndSetIfChanged(ref _scorers, value);
        }
    }

    public string? PlayerTeamId { get; }
    public int SelectedTab { get; set; } = 0;
    private int _selectedRoundTab = 0;
    public int SelectedRoundTab
    {
        get => _selectedRoundTab;
        set => this.RaiseAndSetIfChanged(ref _selectedRoundTab, value);
    }
    public Dictionary<string, MatchResult> _resultMap = new();
    public Dictionary<string, MatchResult> ResultMap
    {
        get => _resultMap;
        set
        {
            GameDb.Instance.ResultMap = value;
            this.RaiseAndSetIfChanged(ref _resultMap, value);
        }
    }
    public Dictionary<string, Team> TeamNameMap { get; }
    public ReactiveCommand<Unit, Unit> Back { get; }

    public bool _canSimulate = true;
    public bool CanSimulate { get => _canSimulate; set => this.RaiseAndSetIfChanged(ref _canSimulate, value); }
    public ReactiveCommand<Unit, Unit> SimulateRound { get; }

    public ReactiveCommand<string, IRoutableViewModel> ViewMatchResult { get; init; }
    public ReactiveCommand<string, IRoutableViewModel> ViewTeam { get; init; }
    public ReactiveCommand<string, IRoutableViewModel> ViewPlayer { get; init; }
    public string UrlPathSegment { get; } = "leagueView";

    public LeagueViewModel(IScreen screen)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        League = League.Make(GameDb.Instance.TeamsMap.Select(kv => kv.Key).ToList());
        UnPlayedRounds = new(League.Rounds);
        // here need to save league

        LeagueTable = League.Table.OrderedTable;
        TeamNameMap = GameDb.Instance.TeamsMap;
        PlayerTeamId = GameDb.Instance.PlayerTeamId;
        ViewMatchResult = ReactiveCommand.CreateFromObservable((string matchId) => HostScreen.Router.Navigate.Execute(new MatchDetailsViewModel(HostScreen, matchId)));
        ViewTeam = ReactiveCommand.CreateFromObservable((string teamId) => HostScreen.Router.Navigate.Execute(new TeamViewModel(HostScreen, teamId)));
        ViewPlayer = ReactiveCommand.CreateFromObservable(
            (string combinedId) => HostScreen.Router.Navigate.Execute(new PlayerDetailsViewModel(HostScreen, combinedId))
        );
        var canSimulate = this.WhenAnyValue(l => l.CanSimulate);
        SimulateRound = ReactiveCommand.CreateFromTask(() =>
        {
            var round = League.GetNextRound();
            if (round is null) return Task.CompletedTask;

            CanSimulate = false;
            Dispatcher.UIThread.InvokeAsync(() =>
            {
                var matches = GameDb.Instance.MakeMatches(round);
                try
                {
                    var results = Match.SimulateMany(matches);
                    League.Table.Update(matches);
                    League.Stats.Update(matches);
                    round.Played = true;
                    League = League;
                    PlayedRounds.Insert(0, round);
                    UnPlayedRounds.Remove(round);
                    GameDb.Instance.OnAfterRound(League);
                    ResultMap = ResultMap.Concat(results).ToDictionary(x => x.Key, x => x.Value);
                    Scorers = League.Stats.OrderedScorers;
                    LeagueTable = League.Table.OrderedTable;
                }
                catch
                {
                    System.Console.WriteLine($"Error while simulating");
                }
                finally
                {
                    CanSimulate = true;
                    // moving to played on rounds
                    SelectedRoundTab = 1;
                }
            }, DispatcherPriority.Background);

            return Task.CompletedTask;

        }, canSimulate);

    }
}
