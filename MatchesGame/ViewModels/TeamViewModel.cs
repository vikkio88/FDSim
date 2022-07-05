namespace MatchesGame.ViewModels;

using ReactiveUI;
using System.Reactive;
using FDSim.Models.Game.Team;
using FDSim.Models.Enums.Helpers;

public class TeamViewModel : ReactiveObject, IRoutableViewModel
{

    public string TeamId { get; } = string.Empty;
    public Team Team { get; }
    public string Country { get; } = string.Empty;
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "teamView";
    public ReactiveCommand<Unit, Unit> Back { get; }

    public TeamViewModel(IScreen screen, string teamId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;

        TeamId = teamId;
        Team = Services.TeamsDb.Instance.GetById(TeamId);
        Country = $"({NationalityHelper.GetName(Team.Nationality)})";
    }


}
