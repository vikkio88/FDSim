namespace MatchesGame.ViewModels;


using ReactiveUI;
using System.Linq;
using System.Collections.Generic;
using System.Reactive;
using FDSim.Generators;
using FDSim.Models.Game.Team;
using FDSim.Models.Game.League;
using MatchesGame.Services;



public class LeagueViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public League League { get => GameDb.Instance.League; set => GameDb.Instance.League = value; }

    private List<TableRow> _leagueTable;
    public List<TableRow> LeagueTable
    {
        get => _leagueTable;
        set
        {
            this.RaiseAndSetIfChanged(ref _leagueTable, value);
        }
    }

    private List<StatRow> _scorers;
    public List<StatRow> Scorers
    {
        get => _scorers;
        set
        {
            this.RaiseAndSetIfChanged(ref _scorers, value);
        }
    }

    public int SelectedTab { get; set; } = 0;
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

    public bool CanSimulate { get; set; } = true;
    public ReactiveCommand<Unit, Unit> SimulateRound { get; }

    public ReactiveCommand<string, IRoutableViewModel> ViewMatchResult { get; set; }
    public string UrlPathSegment { get; } = "leagueView";

    public LeagueViewModel(IScreen screen)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        League = League.Make(GameDb.Instance.GeneratedTeams.Select(t => t.Id).ToList());
        LeagueTable = League.Table.OrderedTable;
        TeamNameMap = GameDb.Instance.TeamsMap;
        ViewMatchResult = ReactiveCommand.CreateFromObservable((string matchId) => HostScreen.Router.Navigate.Execute(new MatchDetailsViewModel(HostScreen, matchId)));
        var canSimulate = this.WhenAnyValue(l => l.CanSimulate);
        SimulateRound = ReactiveCommand.Create(() =>
        {
            var round = League.GetNextRound();
            if (round is null) return;

            CanSimulate = false;
            var matches = GameDb.Instance.MakeMatches(round);
            var results = Match.SimulateMany(matches);
            League.Table.Update(matches);
            League.Stats.Update(matches);
            Scorers = League.Stats.OrderedScorers;
            LeagueTable = League.Table.OrderedTable;
            round.Played = true;
            ResultMap = ResultMap.Concat(results).ToDictionary(x => x.Key, x => x.Value);
            League = League;

            CanSimulate = true;

        }, canSimulate);

    }
}
