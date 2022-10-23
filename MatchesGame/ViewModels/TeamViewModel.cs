namespace MatchesGame.ViewModels;

using System.Reactive;
using ReactiveUI;
using FDSim.Models.Game.Team;
using FDSim.Models.People;
using FDSim.Models.Enums.Helpers;
using MatchesGame.Services;
using FDSim.Models.Game.League;

public class TeamViewModel : ReactiveObject, IRoutableViewModel
{
    public string TeamId { get; } = string.Empty;
    public Team Team { get; }
    public Lineup Lineup { get; }
    public TableRow? Stats { get; }
    public bool IsPlayersTeam { get; }
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "teamView";
    public ReactiveCommand<Unit, Unit> Back { get; }
    public ReactiveCommand<string, IRoutableViewModel> ViewPlayer { get; }
    public ReactiveCommand<string, IRoutableViewModel> ViewRosterPlayer { get; }

    public TeamViewModel(IScreen screen, string teamId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        TeamId = teamId;
        Team = Services.GameDb.Instance.GetTeamById(TeamId);

        IsPlayersTeam = GameDb.Instance.PlayerTeamId == teamId && GameDb.Instance.HasGameStarted;

        // move this to Team itself
        Lineup = Team?.Roster.GetLineup(Team?.Coach?.Module ?? FDSim.Models.Enums.Formation._442);

        Stats = GameDb.Instance.GetTableRowByTeamId(teamId);
        
        // got history of positions to show in graph
        //System.Console.WriteLine($"{string.Join(" ", GameDb.Instance.GetSeasonStatHistoryByTeamId(teamId)?.Positions)}");

        ViewPlayer = ReactiveCommand.CreateFromObservable(
            (string combinedId) => HostScreen.Router.Navigate.Execute(new PlayerDetailsViewModel(HostScreen, combinedId))
        );

        ViewRosterPlayer = ReactiveCommand.CreateFromObservable(
            (string playerId) => ViewPlayer.Execute($"{TeamId}.{playerId}")
        );
    }

}
