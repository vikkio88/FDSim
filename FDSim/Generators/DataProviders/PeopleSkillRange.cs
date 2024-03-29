namespace FDSim.Generators.DataProviders;

using Models.Common;
using Generators;
public class PeopleSkillRange
{
    Dicer _dicer;
    public PeopleSkillRange(Dicer dicer)
    {
        _dicer = dicer;
    }

    public Perc GetCoachSkill(int? forcedPercentage = null)
    {
        int value = forcedPercentage ?? _dicer.Percentage();

        return new(value switch
        {
            >= 90 => _dicer.Faker.Random.Number(90, 100),
            >= 80 => _dicer.Faker.Random.Number(70, 89),
            >= 70 => _dicer.Faker.Random.Number(60, 69),
            >= 50 => _dicer.Faker.Random.Number(50, 70),
            _ => _dicer.Faker.Random.Number(50, 60)
        });
    }
    public Perc GetPlayerSkill(int age, int? forcedPercentage = null)
    {
        int value = forcedPercentage ?? _dicer.Percentage();

        var skillBase = value switch
        {
            >= 90 => _dicer.Faker.Random.Number(90, 100),
            >= 80 => _dicer.Faker.Random.Number(70, 89),
            >= 70 => _dicer.Faker.Random.Number(60, 69),
            >= 50 => _dicer.Faker.Random.Number(50, 70),
            _ => _dicer.Faker.Random.Number(50, 60)
        };

        int diff = _dicer.Faker.Random.Number(3, 8);

        return new(age switch
        {
            >= 34 => Math.Min(80 - diff, skillBase - diff),
            >= 23 => skillBase,
            _ => Math.Min(70 - diff, skillBase - diff),
        });


    }

}
