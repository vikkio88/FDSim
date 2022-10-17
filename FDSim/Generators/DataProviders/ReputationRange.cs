namespace FDSim.Generators.DataProviders;

using Models.Common;
using Generators;
public class ReputationRange
{
    Dicer _dicer;
    public ReputationRange(Dicer dicer)
    {
        _dicer = dicer;
    }

    public Perc GetReputation(Perc skill)
    {
        var perc = _dicer.Faker.Random.Number(70, 100) / 100.0;
        return new Perc(skill.Value * perc);
    }

}
