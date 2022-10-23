using FDSim.Game.ViewModels;

namespace FDSim.Game.Views;

using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using Game.ViewModels;



public partial class LeagueView : ReactiveUserControl<LeagueViewModel>
{
    public LeagueView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
