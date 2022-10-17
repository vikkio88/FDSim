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

    private Contract? _contract = null;
    public Contract? Contract
    {
        get => _contract;
        set => this.RaiseAndSetIfChanged(ref _contract, value);
    }

    private Player? _selectedPlayer = null;
    public Player? SelectedPlayer
    {
        get => _selectedPlayer;
        set => this.RaiseAndSetIfChanged(ref _selectedPlayer, value);
    }
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = "teamView";
    public ReactiveCommand<Unit, Unit> Back { get; }
    public ReactiveCommand<string, Unit> SelectPlayer { get; }

    public TeamViewModel(IScreen screen, string teamId)
    {
        HostScreen = screen;
        Back = HostScreen.Router.NavigateBack;
        SelectPlayer = ReactiveCommand.Create((string playerId) =>
        {
            var team = GameDb.Instance.GetTeamById(TeamId);
            SelectedPlayer = team?.Roster?.GetById(playerId);
            Contract = team?.Roster?.GetContract(playerId);
        });

        TeamId = teamId;
        Team = Services.GameDb.Instance.GetTeamById(TeamId);
    }

}
