namespace FDSim.Game.ViewModels;

using Avalonia.Controls.ApplicationLifetimes;
using ReactiveUI;
using System.Reactive;


public class MainMenuViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }

    public string UrlPathSegment { get; } = "mainMenu";

    public ReactiveCommand<Unit, IRoutableViewModel> NewGame { get; }
    public ReactiveCommand<Unit, Unit> Exit { get; }


    public MainMenuViewModel(IScreen screen)
    {
        HostScreen = screen;
        NewGame = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new NewGameViewModel(this.HostScreen)));
        Exit = ReactiveCommand.Create(() =>
        {
            if (App.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.TryShutdown();
            }
        });
    }
}
