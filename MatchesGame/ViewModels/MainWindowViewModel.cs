namespace MatchesGame.ViewModels;

using System.Reactive;
using ReactiveUI;
using Avalonia.ReactiveUI;
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
    }
}
