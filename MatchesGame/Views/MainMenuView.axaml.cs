namespace MatchesGame.Views;


using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using MatchesGame.ViewModels;


public partial class MainMenuView : ReactiveUserControl<MainMenuViewModel>
{
    public MainMenuView()
    {
        this.WhenActivated(disposables => {});
        AvaloniaXamlLoader.Load(this);
    }
}
