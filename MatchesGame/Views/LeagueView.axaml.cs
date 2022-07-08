namespace MatchesGame.Views;

using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using MatchesGame.ViewModels;



public partial class LeagueView : ReactiveUserControl<LeagueViewModel>
{
    public LeagueView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
