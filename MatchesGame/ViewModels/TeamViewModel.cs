namespace MatchesGame.ViewModels;

using System.Reactive;
using ReactiveUI;
using FDSim.Models.Game.Team;
using FDSim.Models.People;
using FDSim.Models.Enums.Helpers;
using MatchesGame.Services;

public class TeamViewModel : ReactiveObject, IRoutableViewModel
{
    public string TeamId { get; } = string.Empty;
    public Team Team { get; }
    public Lineup Lineup { get; }
    public bool IsPlayersTeam { get; }
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "teamView";
    public ReactiveCommand<Unit, Unit> Back { get; }
    public ReactiveCommand<string, IRoutableViewModel> ViewPlayer { get; }

    public TeamViewModel(IScreen screen, string teamId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        TeamId = teamId;
        Team = Services.GameDb.Instance.GetTeamById(TeamId);

        IsPlayersTeam = GameDb.Instance.PlayerTeamId == teamId && GameDb.Instance.HasGameStarted;
        Lineup = Lineup.Make(Team.Roster.PlayersPerRole, Team.Coach.Module);

        ViewPlayer = ReactiveCommand.CreateFromObservable(
            (string playerId) => HostScreen.Router.Navigate.Execute(new PlayerDetailsViewModel(HostScreen, $"{TeamId}.{playerId}"))
        );
    }

}
