namespace MatchesGame.ViewModels;

using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Avalonia.Threading;

public class MainWindowViewModel : ReactiveObject, IScreen
{
    public RoutingState Router { get; } = new RoutingState();

    public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }
    public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

    public MainWindowViewModel()
    {
        // var nextEnabled = this.WhenAnyValue(x => true);
        GoNext = ReactiveCommand.CreateFromObservable(
            () => Router.Navigate.Execute(new MainMenuViewModel(this))
            // , nextEnabled
            );
        var task = Task.Run(async () =>
        {
            await Task.Delay(4000);
            await Dispatcher.UIThread.InvokeAsync(() => GoNext.Execute());
        });

    }
}
