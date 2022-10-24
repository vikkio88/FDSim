namespace FDSim.Generators.DataProviders;

using Models.Common;
using Generators;
public class PeopleMoneyRange
{
    Dicer _dicer;
    public PeopleMoneyRange(Dicer dicer)
    {
        _dicer = dicer;
    }

    public Money GetIdealWage(Perc skill, bool isCoach = false, int? age = null)
    {
        //@TODO: might want to handle also old players
        if (age < 19)
        {
            return new(_dicer.Faker.Random.Number(5_000, 150_000));
        }

        int baseVal = skill.Value switch
        {
            >= 95 => _dicer.Faker.Random.Number(5_000, 15_000),
            >= 90 => _dicer.Faker.Random.Number(3_000, 7_000),
            >= 80 => _dicer.Faker.Random.Number(600, 5_000),
            >= 70 => _dicer.Faker.Random.Number(500, 1_000),
            >= 60 => _dicer.Faker.Random.Number(200, 450),
            >= 50 => _dicer.Faker.Random.Number(90, 200),
            _ => _dicer.Faker.Random.Number(1, 200)
        };

        return new(baseVal * (isCoach ? 250.0 : 1000.0));
    }

    // @TODO: Might want to add a way to change slighlty if is old player
    public Money GetValue(Perc skill)
    {
        int baseVal = skill.Value switch
        {
            >= 95 => _dicer.Faker.Random.Number(90_000, 200_000),
            >= 90 => _dicer.Faker.Random.Number(80_000, 120_000),
            >= 80 => _dicer.Faker.Random.Number(50_000, 90_000),
            >= 60 => _dicer.Faker.Random.Number(200, 800),
            >= 50 => _dicer.Faker.Random.Number(100, 600),
            _ => _dicer.Faker.Random.Number(10, 100)
        };

        return new(baseVal * 1000.0);
    }

}
