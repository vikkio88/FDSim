namespace FDSim.Game.ViewModels;
using ReactiveUI;
using System.Reactive;


public class DashboardViewModel : BaseRxViewModel
{

    public override string UrlPathSegment { get; } = "Dashboard";

    public DashboardViewModel(IScreen screen) : base(screen)
    {
        //OtherRoute = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.Navigate.Execute(new OtherRouteViewModel(this.HostScreen)));
    }
}