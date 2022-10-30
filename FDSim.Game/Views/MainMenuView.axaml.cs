using FDSim.Game.ViewModels;

namespace FDSim.Game.Views;


using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using Game.ViewModels;


public partial class MainMenuView : ReactiveUserControl<MainMenuViewModel>
{
    public MainMenuView()
    {
        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}
