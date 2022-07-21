namespace MatchesGame.Views;

using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using MatchesGame.ViewModels;



public partial class MatchDetailsView : ReactiveUserControl<MatchDetailsViewModel>
{
    public MatchDetailsView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}