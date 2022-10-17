namespace FDSim.Models.People;

using Models.Common;
public class PlayerStatus : Status
{
    public Perc Attachment { get; set; } = new(0);
    public Perc Condition { get; set; } = new(0);

    public override string ToString()
    {
        return $"{base.ToString()} - c: {Condition}";
    }
}
