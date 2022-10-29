namespace FDSim.Game;

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
        Splat.Locator.CurrentMutable.Register(() => new NewGameView(), typeof(ReactiveUI.IViewFor<NewGameViewModel>));
        Splat.Locator.CurrentMutable.Register(() => new TeamView(), typeof(ReactiveUI.IViewFor<TeamViewModel>));
        Splat.Locator.CurrentMutable.Register(() => new LeagueView(), typeof(ReactiveUI.IViewFor<LeagueViewModel>));
        Splat.Locator.CurrentMutable.Register(() => new MatchDetailsView(), typeof(ReactiveUI.IViewFor<MatchDetailsViewModel>));
        Splat.Locator.CurrentMutable.Register(() => new PlayerDetailsView(), typeof(ReactiveUI.IViewFor<PlayerDetailsViewModel>));
        Splat.Locator.CurrentMutable.Register(() => new Views.DashboardView(), typeof(ReactiveUI.IViewFor<ViewModels.DashboardViewModel>));

        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithIcons(container => container.Register<FontAwesomeIconProvider>())
            .LogToTrace()
            .UseReactiveUI();
    }
}

