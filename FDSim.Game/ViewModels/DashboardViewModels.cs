namespace FDSim.Game.ViewModels;

using FDSim.Game.Services;
using FDSim.Models.Game;
using ReactiveUI;
using System;
using System.Reactive;


public class DashboardViewModel : BaseRxViewModel
{

    public override string UrlPathSegment { get; } = "Dashboard";

    public ReactiveCommand<Unit, IRoutableViewModel> ViewLeague { get; }
    public GamePlayer GamePlayer { get; }

    private DateTime _date;
    public DateTime Date
    {
        get => _date;
        set => this.RaiseAndSetIfChanged(ref _date, value);
    }

    public DashboardViewModel(IScreen screen) : base(screen)
    {
        Back = null;
        GoToDashboard = null;
        Date = GameDb.Instance.Calendar.Date;

        AdvanceDay = ReactiveCommand.Create(() =>
        {
            GameDb.Instance.Calendar.NextDay();
            Date = GameDb.Instance.Calendar.Date;
        });

        AdvanceWeek = ReactiveCommand.Create(() =>
        {
            GameDb.Instance.Calendar.NextWeek();
            Date = GameDb.Instance.Calendar.Date;
        });

        GamePlayer = GameDb.Instance.GamePlayer;
        ViewLeague = ReactiveCommand.CreateFromObservable(() =>
        {
            var leagueView = ViewsStore.Instance.Get(NavigableRoute.League, () => new LeagueViewModel(screen));

            return HostScreen.Router.Navigate.Execute(leagueView);
        });
    }
}