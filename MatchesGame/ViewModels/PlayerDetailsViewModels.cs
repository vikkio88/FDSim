namespace MatchesGame.ViewModels;

using FDSim.Models.Game.League;
using FDSim.Models.Game.Team;
using FDSim.Models.People;
using MatchesGame.Services;
using ReactiveUI;
using System.Reactive;


public class PlayerDetailsViewModel : ReactiveObject, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "PlayerDetails";
    public ReactiveCommand<Unit, Unit> Back { get; }

    public Team? Team { get; init; }
    public Player? Player { get; init; }

    public StatRow? Stats { get; set; }

    public PlayerDetailsViewModel(IScreen screen, string combinedId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        var ids = combinedId.Split(".");
        var teamId = ids[0];
        var playerId = ids[1];

        var values = GameDb.Instance.GetPlayerAndTeamById(teamId, playerId);
        Team = values.Item1;
        Player = values.Item2;
        Stats = GameDb.Instance?.League?.Stats?.GetForPlayer(playerId) ?? null;

    }
}