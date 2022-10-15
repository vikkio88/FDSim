namespace MatchesGame.ViewModels;
using ReactiveUI;
using System.Reactive;


public class PlayerDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "PlayerDetails";
    public ReactiveCommand<Unit, Unit> Back { get; }

    public string TeamId { get; init; }
    public string PlayerId { get; init; }

    public PlayerDetailsViewModel(IScreen screen, string combinedId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        var ids = combinedId.Split(".");
        TeamId = ids[0];
        PlayerId = ids[1];
    }
}