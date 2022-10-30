using FDSim.Models.Common;

namespace FDSim.Models.Game.Team;
public class Finances
{
    public Money Transfer { get; set; } = new(0);
    public Money Wage { get; set; } = new(0);

    public List<Transaction> Transactions { get; init; } = new();
    public Money Expenses
    {
        get => new(Transactions.Select(t => t.Amount.Value).Where(v => v < 0).Sum());
    }
    public Money Income
    {
        get => new(Transactions.Select(t => t.Amount.Value).Where(v => v >= 0).Sum());
    }

    public Money Balance
    {
        get => Income - Expenses;
    }

    public Money ProjectedTransfer(Money value) => Transfer - value;
    public Money ProjectedWage(Money value) => Wage - value;

}

public enum TransactionType
{
    Wage,
    Bonus,
    Tickets,
    SeasonTickets,
    TransferOut,
    TransferIn,
    Merchandise,
    Facilities,
    Misc,
    Investor,
    Sponsor,

    Other,
}

public class Transaction
{
    public TransactionType Type { get; set; } = TransactionType.Other;
    public string Description { get; init; } = string.Empty;
    public string? PayloadId { get; init; } = null;
    public Money Amount { get; init; } = new(0);

    public static List<Transaction> MakeList() => new();

    public static Transaction MakeWages(Money wages) => new()
    {
        Amount = new(-1 * wages.Value),
        Type = TransactionType.Wage,
    };

    public static Transaction MakeMiscExpenses(Money sum) => new()
    {
        Amount = new(-1 * sum.Value),
        Type = TransactionType.Misc,
    };
    public static Transaction MakeMiscIncome(Money sum) => new()
    {
        Amount = new(sum.Value),
        Type = TransactionType.Misc,
    };

    public static Transaction MakeSeasonTickets(Money sum) => new()
    {
        Amount = new(sum.Value),
        Type = TransactionType.SeasonTickets,
    };

    public static Transaction MakeMerch(Money sum) => new()
    {
        Amount = new(sum.Value),
        Type = TransactionType.Merchandise,
    };

    public static Transaction MakeSponsor(Money sum) => new()
    {
        Amount = new(sum.Value),
        Type = TransactionType.Sponsor,
    };
}
