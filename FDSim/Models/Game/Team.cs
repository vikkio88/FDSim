namespace FDSim.Models.Game;

using FDSim.Models.People;
using FDSim.Models.Enums;

public class Team : IdEntity
{
    public String Name { get; set; }
    public String City { get; set; }
    public Nationality Nationality { get; set; }

    public List<Player> Roster { get; set; }

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
