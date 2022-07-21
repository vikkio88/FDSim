namespace MatchesGame.ViewModels;


using ReactiveUI;
using System.Linq;
using System.Collections.Generic;
using System.Reactive;
using FDSim.Generators;
using FDSim.Models.Game.Team;
using FDSim.Models.Game.League;
using MatchesGame.Services;
using MatchesGame.ViewModels;



public class LeagueViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public League League { get => GameDb.Instance.League; set => GameDb.Instance.League = value; }
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
    public ReactiveCommand<Unit, Unit> SimulateRound { get; }

    public ReactiveCommand<string, IRoutableViewModel> ViewMatchResult { get; set; }
    public string UrlPathSegment { get; } = "leagueView";

    public LeagueViewModel(IScreen screen)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        League = League.Make(GameDb.Instance.GeneratedTeams.Select(t => t.Id).ToList());
        TeamNameMap = GameDb.Instance.TeamsMap;
        ViewMatchResult = ReactiveCommand.CreateFromObservable((string matchId) => HostScreen.Router.Navigate.Execute(new MatchDetailsViewModel(HostScreen, matchId)));
        SimulateRound = ReactiveCommand.Create(() =>
        {
            var round = League.GetNextRound();
            if (round is null) return;

            var results = new Dictionary<string, MatchResult>();
            var matches = GameDb.Instance.MakeMatches(round);
            foreach (var match in matches)
            {
                match.Simulate(Dicer.Make());
                results.Add(match.Id, match.Result);
            }
            round.Played = true;
            ResultMap = ResultMap.Concat(results).ToDictionary(x => x.Key, x => x.Value);
            League = League;
        });

    }
}
