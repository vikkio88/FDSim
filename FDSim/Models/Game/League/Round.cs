namespace FDSim.Models.Game.League;
public class Round
{
    public int Index { get; set; }
    public List<MatchPlaceholder> Matches { get; set; } = new();
    public bool Played { get; set; } = false;

    //@TODO might want to add a date?
}
