using FDSim.Models.Common;

namespace FDSim.Models.Game.Team;
public class Finances
{
    public Money Transfer { get; set; } = new(0);
    public Money Wage { get; set; } = new(0);
    public Money Expenses { get; set; } = new(0);
    public Money Income { get; set; } = new(0);

    public Money Balance
    {
        get => Income - Expenses;
    }

    public Money ProjectedTransfer(Money value) => Transfer - value;
    public Money ProjectedWage(Money value) => Wage - value;

}
