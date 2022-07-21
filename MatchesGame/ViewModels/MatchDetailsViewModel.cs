namespace MatchesGame.ViewModels;

using ReactiveUI;
using System.Reactive;
using FDSim.Models.Game.League;
using MatchesGame.Services;


public class MatchDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public ReactiveCommand<Unit, Unit> Back { get; }
    public MatchResult MatchResult { get; set; }
    public Match Match { get; set; }
    public string UrlPathSegment { get; } = "matchDetailsView";

    public MatchDetailsViewModel(IScreen screen, string matchId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        MatchResult = GameDb.Instance.ResultMap[matchId];
        Match = GameDb.Instance.MatchesMap[matchId];
    }
}