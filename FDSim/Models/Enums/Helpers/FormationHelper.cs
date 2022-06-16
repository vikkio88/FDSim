namespace FDSim.Models.Enums.Helpers;
public static class FormationHelper
{
    public static String GetName(Formation formation)
    {
        return formation switch
        {
            Formation._442 => "4-4-2",
            Formation._433 => "4-3-3",
            Formation._343 => "3-4-3",
            Formation._352 => "3-5-2",
            Formation._424 => "4-2-4",
            Formation._541 => "5-4-1",
            Formation._532 => "5-3-2",
            _ => "Invalid"
        };
    }

    public static Dictionary<Role, int> GetRolesNeeded(Formation formation)
    {
        return formation switch
        {
            Formation._442 => new(){
                {Role.Goalkeeper, 1},
                {Role.Defender, 4},
                {Role.Midfielder, 4},
                {Role.Striker, 2},
            },
            Formation._433 => new(){
                {Role.Goalkeeper, 1},
                {Role.Defender, 4},
                {Role.Midfielder, 3},
                {Role.Striker, 3},
            },
            Formation._343 => new(){
                {Role.Goalkeeper, 1},
                {Role.Defender, 3},
                {Role.Midfielder, 4},
                {Role.Striker, 3},
            },
            Formation._352 => new(){
                {Role.Goalkeeper, 1},
                {Role.Defender, 3},
                {Role.Midfielder, 5},
                {Role.Striker, 2},
            },
            Formation._424 => new(){
                {Role.Goalkeeper, 1},
                {Role.Defender, 4},
                {Role.Midfielder, 2},
                {Role.Striker, 4},
            },
            Formation._541 => new(){
                {Role.Goalkeeper, 1},
                {Role.Defender, 5},
                {Role.Midfielder, 4},
                {Role.Striker, 1},
            },
            Formation._532 => new(){
                {Role.Goalkeeper, 1},
                {Role.Defender, 5},
                {Role.Midfielder, 3},
                {Role.Striker, 2},
            },
            _ => new(){
                {Role.Goalkeeper, 0},
                {Role.Defender, 0},
                {Role.Midfielder, 0},
                {Role.Striker, 0},
            }
        };
    }

}
