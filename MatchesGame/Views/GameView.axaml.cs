namespace MatchesGame.Views;


using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using MatchesGame.ViewModels;


public partial class GameView : ReactiveUserControl<GameViewModel>
{
    public GameView()
    {
        this.WhenActivated(disposables => {});
        AvaloniaXamlLoader.Load(this);
    }
}
