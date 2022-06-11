namespace FDSim.Models.Game;

using FDSim.Models.People;
using FDSim.Models.Enums;

public class Team : IdEntity
{
    public String Name { get; init; }
    public String City { get; init; }
    public Nationality Nationality { get; init; }

    public List<Player> Roster { get; init; }

    public Team()
    {
        Name = "";
        City = "";
        Nationality = Nationality.English;
        Roster = new List<Player>();
    }

    public override string ToString()
    {
        return String.Format("{0}_Team: {1} {2} ({3}) - Roster: {4}", base.ToString(), Name, City, Nationality, Roster.Count);
    }

}
