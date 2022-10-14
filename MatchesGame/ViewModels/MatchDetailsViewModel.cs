namespace MatchesGame.ViewModels;

using ReactiveUI;
using System.Reactive;
using FDSim.Models.Game.League;
using FDSim.Models.People;
using MatchesGame.Services;
using System.Collections.Generic;

public class MatchDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public ReactiveCommand<Unit, Unit> Back { get; }
    public MatchResult MatchResult { get; set; }
    public Match Match { get; set; }
    public Dictionary<string, Player> HomeRoster { get; init; }
    public Dictionary<string, Player> AwayRoster { get; init; }
    public ReactiveCommand<string, IRoutableViewModel> ViewTeam { get; set; }
    public string UrlPathSegment { get; } = "matchDetailsView";

    public MatchDetailsViewModel(IScreen screen, string matchId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        MatchResult = GameDb.Instance.ResultMap[matchId];
        Match = GameDb.Instance.MatchesMap[matchId];
        HomeRoster = Match.Home.Roster.IndexedPlayers;
        AwayRoster = Match.Away.Roster.IndexedPlayers;

        ViewTeam = ReactiveCommand.CreateFromObservable((string teamId) => HostScreen.Router.Navigate.Execute(new TeamViewModel(HostScreen, teamId)));
    }
}