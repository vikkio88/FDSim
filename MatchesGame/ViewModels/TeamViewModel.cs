namespace MatchesGame.ViewModels;

using System;
using System.Reactive;
using ReactiveUI;
using MatchesGame.Services;
using FDSim.Models.Game.Team;
using FDSim.Models.People;
using FDSim.Models.Enums.Helpers;

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
            SelectedPlayer = TeamsDb.Instance.GetById(TeamId).Roster?.GetById(playerId);
        });

        TeamId = teamId;
        Team = Services.TeamsDb.Instance.GetById(TeamId);
        Country = $"({NationalityHelper.GetName(Team.Nationality)})";
    }


}
