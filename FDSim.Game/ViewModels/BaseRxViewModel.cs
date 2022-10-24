using System.Reactive;
using ReactiveUI;

namespace FDSim.Game.ViewModels;


abstract public class BaseRxViewModel : ReactiveObject, IRoutableViewModel
{
    virtual public string? UrlPathSegment { get; }

    public IScreen HostScreen { get; }
    public ReactiveCommand<Unit, Unit> Back { get; }


    public BaseRxViewModel(IScreen screen)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
    }


}