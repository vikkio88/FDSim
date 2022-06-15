namespace FDSim.Models.Game.Team;

using Enums;
using Enums.Helpers;
using People;

public class Lineup
{
    const int NUMBER_OF_SUBS = 2;
    public List<Player> Starters { get; init; }
    public List<Player> Bench { get; init; }
    public Dictionary<Role, int> RolesAssigned { get; init; }
    public Dictionary<Role, int> RolesNeeded { get; init; }
    public Dictionary<Role, int> RolesMissing { get; init; }


    public static Lineup Make(Dictionary<Role, List<Player>> players, Formation formation)
    {
        var rolesNeeded = FormationHelper.GetRolesNeeded(formation);
        var starters = new List<Player>();
        var bench = new List<Player>();

        //this is needed to filter players already chosen
        var selectedPlayers = new List<String>();

        var rolesAssigned = RoleHelper.GetEmptyRoleCounter();
        var rolesMissing = RoleHelper.GetEmptyRoleCounter();
        var avgSkillPerRole = RoleHelper.GetEmptyRoleDouble();


        foreach (var (role, amount) in rolesNeeded)
        {
            var playersInRole = players[role];
            var picks = playersInRole.OrderBy(p => p.SkillAvg).Take(amount).ToList();
            picks.ForEach(p => selectedPlayers.Add(p.Id));

            avgSkillPerRole[role] = picks.Average(p => p.SkillAvg);
            rolesMissing[role] = amount - picks.Count;

            starters = starters.Concat(picks).ToList();
            var bencherPerRole = playersInRole.FindAll(p => !selectedPlayers.Contains(p.Id)).Take(NUMBER_OF_SUBS).ToList();
            bencherPerRole.ForEach(p => selectedPlayers.Add(p.Id));
            bench = bench.Concat(bencherPerRole).ToList();
        }

        // maybe can pluck from bench?
        // patching team if has not enough players in a role

        return new Lineup
        {
            Starters = starters,
            Bench = bench,
            RolesAssigned = rolesAssigned,
            RolesMissing = rolesMissing,
            RolesNeeded = rolesNeeded
        };

    }
}
