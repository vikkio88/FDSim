namespace FDSim.Models.Enums.Helpers;

public static class RoleHelper
{
    public static Dictionary<Role, int> GetEmptyRoleCounter()
    {
        return new(){
                {Role.Goalkeeper, 0},
                {Role.Defender, 0},
                {Role.Midfielder, 0},
                {Role.Striker, 0},
            };
    }
    public static Dictionary<Role, double> GetEmptyRoleDouble()
    {
        return new(){
                {Role.Goalkeeper, 0.0},
                {Role.Defender, 0.0},
                {Role.Midfielder, 0.0},
                {Role.Striker, 0.0},
            };
    }
}
