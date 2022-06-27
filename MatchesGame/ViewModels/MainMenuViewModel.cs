namespace MatchesGame.ViewModels;

using ReactiveUI;
using System.Reactive;


public class MainMenuViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public string UrlPathSegment { get; } = "mainMenu";

    public ReactiveCommand<Unit, IRoutableViewModel> NewGame { get; }


    public MainMenuViewModel(IScreen screen)
    {
        HostScreen = screen;
        NewGame = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new GameViewModel(this.HostScreen)));
    }
}
