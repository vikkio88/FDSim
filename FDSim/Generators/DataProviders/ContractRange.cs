namespace FDSim.Generators.DataProviders;

using Models.Common;
using Generators;
using FDSim.Models.Game.Team;
using FDSim.Models.People;

public class ContractRange
{
    Dicer _dicer;
    public ContractRange(Dicer dicer)
    {
        _dicer = dicer;
    }

    public Contract GetContract(Player player)
    {
        var years = _dicer.Faker.Random.Number(1, Contract.MAX_YEARS);
        return new()
        {
            Years = years,
            Remaining = _dicer.Faker.Random.Number(1, years),
            Pay = GetPay(player.IdealWage)
        };
    }

    public Money GetPay(Money perfectWage)
    {
        var perc = _dicer.Faker.Random.Number(40, 110) / 100.0;
        return new Money(perfectWage.Value * perc);
    }

}
