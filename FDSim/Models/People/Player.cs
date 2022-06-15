namespace FDSim.Models.People;

using FDSim.Models.Enums;
public class Player : Person
{
    public Role Role { get; init; }
    public Player() : base() { Role = Role.Goalkeeper; }
    public Player(String name, String surname, int age, Role role) : base(name, surname, age) { Role = role; }
    public Player(String name, String surname, int age, int skillAvg, Role role) : base(name, surname, age) { SkillAvg = skillAvg; Role = role; }

    public override string ToString()
    {
        return String.Format("{0} - Player ({1})", base.ToString(), Role);
    }
}
