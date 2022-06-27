﻿namespace MatchesGame;

using System;
using Avalonia;
using Avalonia.ReactiveUI;
using Views;
using ViewModels;

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
        
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .UseReactiveUI()
            .LogToTrace()
            .UseReactiveUI();
    }
}

