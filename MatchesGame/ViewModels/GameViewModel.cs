namespace MatchesGame.ViewModels;

using ReactiveUI;

public class GameViewModel: ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen {get;}

    public string UrlPathSegment {get;} = "gameView";

    public GameViewModel(IScreen screen) => HostScreen = screen;
}
