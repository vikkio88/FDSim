namespace FDSim.Test.Generators.DataProviders;

using FDSim.Generators;
using FDSim.Generators.DataProviders;
public class PeopleSkillRangeTest
{
    [Fact(Skip="Just to see what the distrivution looks like")]
    public void PeopleSkillRangeTestDistribution()
    {
        var dicer = new Dicer(0);

        var p = new PeopleSkillRange(dicer);

        foreach (int _ in Enumerable.Range(0, 100))
        {
            Console.WriteLine(p.GetSkill());
        }

    }
}
