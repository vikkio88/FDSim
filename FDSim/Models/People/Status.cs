namespace FDSim.Models.People;

using Models.Common;
public class Status
{
    public Perc Morale { get; set; } = new(0);
    public Perc Attachment { get; set; } = new(0);

    public override string ToString()
    {
        return $"m: {Morale} - a: {Attachment}";
    }
}
