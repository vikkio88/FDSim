namespace MatchesGame.Views;

using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using MatchesGame.ViewModels;



public partial class TeamView : ReactiveUserControl<TeamViewModel>
{
    public TeamView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
