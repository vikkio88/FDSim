namespace MatchesGame;

using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Views;
using ViewModels;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.FontAwesome;

class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        // .Start<MainWindow>(() => new MainWindowViewModel());
        .StartWithClassicDesktopLifetime(args);


    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        Splat.Locator.CurrentMutable.Register(() => new MainMenuView(), typeof(ReactiveUI.IViewFor<MainMenuViewModel>));
        Splat.Locator.CurrentMutable.Register(() => new GameView(), typeof(ReactiveUI.IViewFor<GameViewModel>));
        Splat.Locator.CurrentMutable.Register(() => new TeamView(), typeof(ReactiveUI.IViewFor<TeamViewModel>));
        Splat.Locator.CurrentMutable.Register(() => new LeagueView(), typeof(ReactiveUI.IViewFor<LeagueViewModel>));

        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseReactiveUI()
            .WithIcons(container => container.Register<FontAwesomeIconProvider>())
            .LogToTrace()
            .UseReactiveUI();
    }
}

