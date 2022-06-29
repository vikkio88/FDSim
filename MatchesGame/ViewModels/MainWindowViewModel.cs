namespace MatchesGame.ViewModels;

using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Avalonia.Threading;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new RoutingState();
    public MainWindowViewModel()
    {
        DispatcherTimer.RunOnce(
            () => Router.Navigate.Execute(new MainMenuViewModel(this)),
            System.TimeSpan.FromSeconds(4)
        );
    }
}
