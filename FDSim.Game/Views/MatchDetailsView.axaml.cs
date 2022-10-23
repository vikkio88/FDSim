using FDSim.Game.ViewModels;

namespace FDSim.Game.Views;

using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using Game.ViewModels;



public partial class MatchDetailsView : ReactiveUserControl<MatchDetailsViewModel>
{
    public MatchDetailsView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}