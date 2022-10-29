namespace FDSim.Game.ViewModels;

using ReactiveUI;
using Avalonia.Threading;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new RoutingState();
    public MainWindowViewModel()
    {
        DispatcherTimer.RunOnce(
            // () => Router.Navigate.Execute(new MainMenuViewModel(this)),
            // System.TimeSpan.FromSeconds(4)
            () => Router.Navigate.Execute(new NewGameViewModel(this)),
            System.TimeSpan.FromMilliseconds(1)
        );
    }
}
