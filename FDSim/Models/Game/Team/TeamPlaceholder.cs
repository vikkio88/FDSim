namespace FDSim.Models.Game.Team;
public class TeamPlaceholder
{
    public string Id { get; }
    public string Name { get; }

    public TeamPlaceholder(Team team)
    {
        Id = team.Id;
        Name = team.Name;
    }
}
