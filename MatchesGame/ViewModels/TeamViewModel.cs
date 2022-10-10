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

    private Player? _selectedPlayer = null;
    public Player? SelectedPlayer
    {
        get => _selectedPlayer;
        set => this.RaiseAndSetIfChanged(ref _selectedPlayer, value);
    }
    public string Country { get; } = string.Empty;
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

            SelectedPlayer = GameDb.Instance.GetTeamById(TeamId)?.Roster?.GetById(playerId);
        });

        TeamId = teamId;
        Team = Services.GameDb.Instance.GetTeamById(TeamId);
        // Maybe use this instead, can be reused for roles
        // https://docs.avaloniaui.net/docs/data-binding/converting-binding-values#binding-converters
        Country = $"({NationalityHelper.GetName(Team.Nationality)})";
    }

}
