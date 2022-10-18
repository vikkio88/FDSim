using FDSim.Models.Common;

namespace FDSim.Models.Game.Team;
public class Contract
{
    public const int MAX_YEARS = 5;
    public int Years { get; set; } = MAX_YEARS;
    public int Remaining { get; set; } = MAX_YEARS;
    public Money Pay { get; set; } = new(0);
    public string PersonId { get; set; } = string.Empty;
    public string TeamId { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"{Remaining}/{Years} years {Pay} /y";
    }
}
