using System.Reactive;
using FDSim.Game.Services;
using ReactiveUI;

namespace FDSim.Game.ViewModels;


abstract public class BaseRxViewModel : ReactiveObject, IRoutableViewModel
{
    virtual public string? UrlPathSegment { get; }

    public IScreen HostScreen { get; }
    // this is for ReactiveUi Route on avalonia 11
    // public ReactiveCommand<Unit, IRoutableViewModel?>? Back { get; set; } = null;
    public ReactiveCommand<Unit, Unit>? Back { get; set; } = null;
    public ReactiveCommand<Unit, IRoutableViewModel>? GoToDashboard { get; set; } = null;
    public ReactiveCommand<Unit, Unit>? AdvanceDay { get; set; } = null;
    public ReactiveCommand<Unit, Unit>? AdvanceWeek { get; set; } = null;


    public BaseRxViewModel(IScreen screen)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        var dashboard = ViewsStore.Instance.Get(NavigableRoute.Dashboard);
        if (dashboard is not null)
        {
            GoToDashboard = ReactiveCommand.CreateFromObservable(() => HostScreen.Router.NavigateAndReset.Execute(dashboard));
        }

    }


}