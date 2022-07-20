namespace MatchesGame.ViewModels;


using ReactiveUI;
using System.Linq;
using System.Collections.Generic;
using System.Reactive;
using FDSim.Models.Game.Team;
using FDSim.Models.Game.League;
using MatchesGame.Services;



public class LeagueViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public League League { get; }
    public Dictionary<string, Team> TeamNameMap { get; }
    public ReactiveCommand<Unit, Unit> Back { get; }
    public ReactiveCommand<Unit, League> SimulateRound { get; }
    public string UrlPathSegment { get; } = "leagueView";

    public LeagueViewModel(IScreen screen)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        League = League.Make(GameDb.Instance.GeneratedTeams.Select(t => t.Id).ToList());
        TeamNameMap = GameDb.Instance.TeamsMap;

        SimulateRound = ReactiveCommand.Create(() =>
        {
            var round = League.GetNextRound();
            GameDb.Instance.MakeMatches(round);
            return League;
        });

    }
}
