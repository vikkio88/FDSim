using FDSim.Game.Services;

namespace FDSim.Game.ViewModels;

using FDSim.Models.Game.League;
using FDSim.Models.Game.Team;
using FDSim.Models.People;
using Game.Services;
using ReactiveUI;
using System.Reactive;


public class PlayerDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "PlayerDetails";
    public ReactiveCommand<Unit, Unit> Back { get; }

    public int BirthYear { get; }
    public Team? Team { get; init; }
    public Contract? Contract { get; init; }
    public Player? Player { get; init; }

    public StatRow? Stats { get; set; }
    public ReactiveCommand<string, IRoutableViewModel> ViewTeam { get; }

    public PlayerDetailsViewModel(IScreen screen, string combinedId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        var ids = combinedId.Split(".");
        var teamId = ids[0];
        var playerId = ids[1];

        ViewTeam = ReactiveCommand.CreateFromObservable(
            (string teamId) => HostScreen.Router.Navigate.Execute(new TeamViewModel(HostScreen, teamId))
        );

        var (team, player) = GameDb.Instance.GetPlayerAndTeamById(teamId, playerId);
        Team = team;
        Player = player;
        BirthYear = GameDb.Instance.CurrentYear - (Player?.Age ?? 0);
        Contract = Team?.Roster?.GetContract(playerId) ?? null;
        Stats = GameDb.Instance?.League?.Stats?.GetForPlayer(playerId) ?? null;
    }
}