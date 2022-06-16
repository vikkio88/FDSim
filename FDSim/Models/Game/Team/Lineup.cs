namespace FDSim.Models.Game.Team;

using Enums;
using Enums.Helpers;
using People;

using DRi = Dictionary<Enums.Role, int>;
using DRd = Dictionary<Enums.Role, double>;

public class Lineup
{
    const int NUMBER_OF_SUBS = 2;
    public List<Player> Starters { get; init; }
    public List<Player> Bench { get; init; }
    public DRi RolesAssigned { get; init; }
    public DRi RolesNeeded { get; init; }
    public DRi RolesMissing { get; init; }
    public DRd AvgSkillPerRole { get; init; }
    public double AvgMorale { get; init; } = 0;
    public double AvgCondition { get; init; } = 0;


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
        double moraleTotal = 0.0;
        double conditionTotal = 0.0;


        foreach (var (role, amount) in rolesNeeded)
        {
            var playersInRole = players[role];
            // maybe here we could also order by morale and condition
            var picks = playersInRole.OrderBy(p => p.SkillAvg).Take(amount).ToList();
            picks.ForEach(p =>
            {
                moraleTotal += p.Status.Morale.Value;
                conditionTotal += p.Status.Condition.Value;
                selectedPlayers.Add(p.Id);
            });

            avgSkillPerRole[role] = picks.Count > 0 ? picks.Average(p => p.SkillAvg) : 0.0;
            rolesAssigned[role] = picks.Count;
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
            RolesNeeded = rolesNeeded,
            AvgSkillPerRole = avgSkillPerRole,
            AvgMorale = moraleTotal / (double)starters.Count,
            AvgCondition = conditionTotal / (double)starters.Count,
        };
    }


    // Maybe can move this to avgSkill calculation
    public DRd gtAdjustedAvgSkillPerRole()
    {
        var adjusted = RoleHelper.GetEmptyRoleDouble();
        foreach (var (role, avg) in AvgSkillPerRole)
        {
            var newAvg = avg;
            if (RolesMissing[role] > 0)
                newAvg = (avg / (double)(RolesAssigned[role] + RolesMissing[role]));

            adjusted[role] = newAvg;
        }

        return adjusted;
    }
}
