using FDSim.Game.ViewModels;

namespace FDSim.Game.Views;


using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using Game.ViewModels;


public partial class NewGameView : ReactiveUserControl<NewGameViewModel>
{
    public NewGameView()
    {
        this.WhenActivated(disposables => {});
        AvaloniaXamlLoader.Load(this);
    }
}
