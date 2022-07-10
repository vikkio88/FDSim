namespace MatchesGame.ViewModels;

using System.Reactive;
using ReactiveUI;
using FDSim.Models.Game.Team;
using FDSim.Models.People;
using FDSim.Models.Enums.Helpers;
using MatchesGame.Services;
using MatchesGame.Views.Common;

public class TeamViewModel : ReactiveObject, IRoutableViewModel
{

    public string TeamId { get; } = string.Empty;
    public Team Team { get; }
    public StarsContext TeamAvg { get; set; }

    private Player? _selectedPlayer = null;
    public Player? SelectedPlayer
    {
        get => _selectedPlayer;
        set => this.RaiseAndSetIfChanged(ref _selectedPlayer, value);
    }
    private StarsContext _selectedPlayerSkills;
    public StarsContext SelectedPlayerSkills
    {
        get => _selectedPlayerSkills;
        set => this.RaiseAndSetIfChanged(ref _selectedPlayerSkills, value);
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

            SelectedPlayer = TeamsDb.Instance.GetById(TeamId)?.Roster?.GetById(playerId);
            SelectedPlayerSkills = new(SelectedPlayer.Skill.Value);
        });

        TeamId = teamId;
        Team = Services.TeamsDb.Instance.GetById(TeamId);
        TeamAvg = new(Team.Roster.Avg);
        Country = $"({NationalityHelper.GetName(Team.Nationality)})";
    }

}
