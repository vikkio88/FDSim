namespace FDSim.Game.ViewModels;

using FDSim.Game.Models.Game;
using FDSim.Game.Services;
using ReactiveUI;
using System.Reactive;


public class DashboardViewModel : BaseRxViewModel
{

    public override string UrlPathSegment { get; } = "Dashboard";

    public ReactiveCommand<Unit, IRoutableViewModel> ViewLeague { get; }
    public GamePlayer GamePlayer { get; }

    public DashboardViewModel(IScreen screen) : base(screen)
    {
        Back = null;
        GoToDashboard = null;
        GamePlayer = GameDb.Instance.GamePlayer;
        ViewLeague = ReactiveCommand.CreateFromObservable(() =>
        {
            var leagueView = ViewsStore.Instance.Get(NavigableRoute.League, () => new LeagueViewModel(screen));

            return HostScreen.Router.Navigate.Execute(leagueView);
        });
    }
}