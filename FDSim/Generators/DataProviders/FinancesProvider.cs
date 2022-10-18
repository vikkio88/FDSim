namespace FDSim.Generators.DataProviders;

using Models.Common;
using Generators;
using FDSim.Models.Game.Team;

public class FinancesProvider
{
    Dicer _dicer;
    public FinancesProvider(Dicer dicer)
    {
        _dicer = dicer;
    }

    public Finances GetFinances(Team team)
    {
        var roster = team.Roster;

        var wageCosts = roster?.Contracts?.Select(kv => kv.Value?.Pay.Value).Sum() ?? 0.0;
        var percWage = _dicer.Faker.Random.Number(95, 110) / 100.0;

        var topVal = roster?.Players.Select(p => p.Value.Value).Max() ?? 0.0;
        var avgVal = roster?.Players.Select(p => p.Value.Value).Average() ?? 0.0;
        var percVal = _dicer.Faker.Random.Number(50, 150) / 100.0;

        var percExp = _dicer.Faker.Random.Number(100, 150) / 100.0;

        var expenses = wageCosts * percExp;
        var percInc = _dicer.Faker.Random.Number(95, 200) / 100.0;
        var income = expenses * percInc;

        var transactions = new List<Transaction>();

        // adding expenses
        transactions.Add(Transaction.MakeWages(new(wageCosts)));
        transactions.Add(Transaction.MakeMiscExpenses(new(expenses - wageCosts)));

        // adding incomes
        var percMerc = _dicer.Faker.Random.Number(team.Reputation.Value / 2, 55) / 100.0;
        var percSponsor = _dicer.Faker.Random.Number(team.Reputation.Value / 2, 60) / 100.0;
        var ticketsAmount = income * percMerc;
        transactions.Add(Transaction.MakeSeasonTickets(new(ticketsAmount)));
        transactions.Add(Transaction.MakeSponsor(new(income * percSponsor)));
        transactions.Add(Transaction.MakeMerch(new(income - ticketsAmount)));


        return new Finances()
        {
            Transfer = new((topVal - avgVal) / 2 * percVal),
            Wage = new(wageCosts * percWage),
            Transactions = transactions
        };
    }

}
