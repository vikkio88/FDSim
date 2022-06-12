namespace FDSim.Models.Game.Team;



using FDSim.Models.People;
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
        return String.Format(
            "{0}_Team: {1} {2} ({3}) - Roster: {4}",
            base.ToString(),
            Name, City, Nationality,
            Roster?.Count() ?? 0
        );
    }

}
