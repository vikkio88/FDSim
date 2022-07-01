namespace FDSim.Models.Game.Team;

using FDSim.Models.Enums;

public class Team : IdEntity
{
    public String Name { get; init; }
    public String City { get; init; }
    public Nationality Nationality { get; init; }

    public Roster? Roster { get; set; }

    public Team()
    {
        Name = "";
        City = "";
        Nationality = Nationality.English;
        Roster = null;
    }

    public override string ToString()
    {
        return $"{base.ToString()}_Team: {Name} ({City} {Nationality}) - Roster: {Roster?.Count ?? 0}";
    }

}
