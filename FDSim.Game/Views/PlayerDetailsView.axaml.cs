using FDSim.Game.ViewModels;

namespace FDSim.Game.Views;
using Game.ViewModels;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ReactiveUI;

public partial class PlayerDetailsView : ReactiveUserControl<PlayerDetailsViewModel>
{
    public PlayerDetailsView()
    {
        this.WhenActivated(disposables => {});
        AvaloniaXamlLoader.Load(this);
    }
}
