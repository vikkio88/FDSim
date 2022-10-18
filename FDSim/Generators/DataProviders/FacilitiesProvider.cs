namespace FDSim.Generators.DataProviders;

using Models.Common;
using Generators;
using FDSim.Models.Game.Team;

public class FacilitiesProvider
{
    Dicer _dicer;
    public FacilitiesProvider(Dicer dicer)
    {
        _dicer = dicer;
    }

    /***
     (Training,Youth)
    */
    public (Perc, Perc) GetFacilities(Team team)
    {
        if (team.Finances.Balance.Value < 0)
        {
            return (
                new(_dicer.Faker.Random.Number(30, 60)),
                new(_dicer.Faker.Random.Number(30, 60))
            );
        }

        return (
            new(_dicer.Faker.Random.Number((int)team.Reputation.Value, 100)),
            new(_dicer.Faker.Random.Number((int)team.Reputation.Value, 100))
        );
    }

}
