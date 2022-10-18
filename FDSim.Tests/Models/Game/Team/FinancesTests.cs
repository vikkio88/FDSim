namespace FDSim.Tests.Models.Game.Team;

using FDSim.Models.Common;
using FDSim.Models.Game.Team;
public class FinancesTests
{
    [Fact]
    public void FinancesCalculateBalanceCorrectly()
    {
        var f = new Finances();
        Assert.NotStrictEqual(new Money(0.0), f.Balance);

        var transactions = Transaction.MakeList();
        transactions.Add(Transaction.MakeMiscExpenses(new(1_000_000)));
        transactions.Add(Transaction.MakeMiscIncome(new(10_000_000)));

        f = new Finances() { Transactions = transactions };
        Assert.Equal(-1_000_000, f.Expenses.Value);
        Assert.Equal(10_000_000, f.Income.Value);
        Assert.NotStrictEqual(new Money(9_000_000), f.Balance);

        transactions = Transaction.MakeList();
        transactions.Add(Transaction.MakeMiscExpenses(new(100_000_000)));
        transactions.Add(Transaction.MakeMiscIncome(new(10_000_000)));
        f = new Finances() { Transactions = transactions };

        f = new Finances() { Transactions = transactions };
        Assert.Equal(-100_000_000, f.Expenses.Value);
        Assert.Equal(10_000_000, f.Income.Value);
        Assert.NotStrictEqual(new Money(-90_000_000), f.Balance);
    }

    [Fact]
    public void FinancesProjectionTest()
    {
        var f = new Finances() { Transfer = new(10_000_000), Wage = new(1_000_000) };

        Assert.NotStrictEqual(new Money(8_000_000), f.ProjectedTransfer(new(2_000_000)));
        Assert.NotStrictEqual(new Money(-2_000_000), f.ProjectedTransfer(new(12_000_000)));

        Assert.NotStrictEqual(new Money(500_000), f.ProjectedWage(new(500_000)));
        Assert.NotStrictEqual(new Money(-2_000_000), f.ProjectedWage(new(3_000_000)));
    }
}
