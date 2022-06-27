namespace MatchesGame.Views;


using Avalonia.Markup.Xaml;

using Avalonia.ReactiveUI;
using ReactiveUI;
using MatchesGame.ViewModels;


public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        this.WhenActivated(disposables => {});
        AvaloniaXamlLoader.Load(this);
    }
}
