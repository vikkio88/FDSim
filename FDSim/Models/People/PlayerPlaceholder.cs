namespace FDSim.Models.People;
public class PlayerPlaceholder
{
    public string Id { get; }
    public string FullName { get; }
    public int Age { get; }

    public PlayerPlaceholder(Player player)
    {
        Id = player.Id;
        FullName = player.PrintName;
        Age = player.Age;
    }
}
