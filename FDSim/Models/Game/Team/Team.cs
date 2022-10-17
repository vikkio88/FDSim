namespace FDSim.Models.Game.Team;

using FDSim.Models.Common;
using FDSim.Models.Enums;

public class Team : IdEntity
{
    public String Name { get; init; }
    public String City { get; init; }
    public Nationality Nationality { get; init; }

    public Finances Finances { get; set; }
    public Perc Reputation { get; set; } = new(0);
    public Perc YouthAcademy { get; set; } = new(0);
    public Perc TrainingFacilities { get; set; } = new(0);

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
