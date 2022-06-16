namespace FDSim.Tests.Models.Game.League;

using FDSim.Generators;
using FDSim.Models.Game.League;
using FDSim.Models.People;
public class MatchTests
{
    [Fact]
    public void MatchSimulationTests()
    {
        var dicer = new Dicer(0);
        var gG = new GameEntityGenerator(dicer.Seed, dicer: dicer);
        var t1 = gG.GetTeam();
        var t2 = gG.GetTeam();

        var m = Match.Make(t1, t2);
        m.Simulate(dicer);

        Assert.True(m.isFinished);
        Assert.NotNull(m.Result);
    }

    [Fact(Skip = "Debug Test")]
    // [Fact]
    public void MatchSimulationDebugTests()
    {
        var sw = new System.IO.StreamWriter("./results.txt");
        const int SIMULATIONS = 500;
        // const int SIMULATIONS = 40;
        var dicer = new Dicer(0);
        var gG = new GameEntityGenerator(dicer.Seed, dicer: dicer);
        var t1 = gG.GetTeam();
        var t2 = gG.GetTeam();
        int[] counter = { 0, 0, 0 };

        Console.WriteLine($"{t1.Name} - {t2.Name}");
        Console.WriteLine($"{t1.Roster?.Average()} - {t2.Roster?.Average()}");
        foreach (var index in Enumerable.Range(0, SIMULATIONS))
        {
            var m = Match.Make(t1, t2);
            m.Simulate(dicer);
            // Console.WriteLine($"{index + 1} - {m}");
            sw.WriteLine($"{index + 1} - {m}");
            if (m.Result?.isDraw ?? false)
                counter[1]++;
            else
                counter[m.Result?.GoalHome > m.Result?.GoalAway ? 0 : 2]++;

        }
        var amazingStatus = new PlayerStatus() { Morale = new(100), Condition = new(100) };
        t2.Roster?.AddPlayer(new() { SkillAvg = 100, Role = FDSim.Models.Enums.Role.Goalkeeper, Status = amazingStatus });
        t2.Roster?.AddPlayer(new() { SkillAvg = 100, Role = FDSim.Models.Enums.Role.Defender, Status = amazingStatus });
        t2.Roster?.AddPlayer(new() { SkillAvg = 100, Role = FDSim.Models.Enums.Role.Defender, Status = amazingStatus });
        t2.Roster?.AddPlayer(new() { SkillAvg = 100, Role = FDSim.Models.Enums.Role.Midfielder, Status = amazingStatus });
        t2.Roster?.AddPlayer(new() { SkillAvg = 100, Role = FDSim.Models.Enums.Role.Midfielder, Status = amazingStatus });
        t2.Roster?.AddPlayer(new() { SkillAvg = 100, Role = FDSim.Models.Enums.Role.Striker, Status = amazingStatus });
        t2.Roster?.AddPlayer(new() { SkillAvg = 100, Role = FDSim.Models.Enums.Role.Striker, Status = amazingStatus });
        Console.WriteLine($"{counter[0]} - {counter[1]} - {counter[2]}");
        sw.WriteLine($"{counter[0]} - {counter[1]} - {counter[2]}");
        counter[0] = 0;
        counter[1] = 0;
        counter[2] = 0;
        Console.WriteLine($"{t1.Name} - {t2.Name}");
        Console.WriteLine($"{t1.Roster?.Average()} - {t2.Roster?.Average()}");

        foreach (var index in Enumerable.Range(0, SIMULATIONS))
        {
            var m = Match.Make(t1, t2);
            m.Simulate(dicer);
            // Console.WriteLine($"{index + 1} - {m}");
            sw.WriteLine($"{index + 1} - {m}");
            if (m.Result?.isDraw ?? false)
                counter[1]++;
            else
                counter[m.Result?.GoalHome > m.Result?.GoalAway ? 0 : 2]++;
        }
        Console.WriteLine($"{counter[0]} - {counter[1]} - {counter[2]}");
        sw.WriteLine($"{counter[0]} - {counter[1]} - {counter[2]}");
        sw.Dispose();
    }

}
