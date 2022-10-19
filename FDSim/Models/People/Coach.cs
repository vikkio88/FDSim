namespace FDSim.Models.People;

using FDSim.Models.Enums;
using FDSim.Models.Game.Team;

public class Coach : Person
{
    public Contract? Contract { get; set; } = null;
    public Formation Module { get; set; }
    public Status Status { get; set; }
    public Coach() : base() { Module = Formation._442; Status = new Status(); }
    public Coach(String name, String surname, int age, Formation module, Status? status = null)
    : base(name, surname, age)
    {
        Module = module;
        Status = status ?? new Status();
    }

    public override string ToString()
    {
        return String.Format("{0} - Coach ({1})", base.ToString(), Module);
    }
}
