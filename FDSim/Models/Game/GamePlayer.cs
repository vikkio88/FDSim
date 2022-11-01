using FDSim.Models.Common;
using FDSim.Models.Enums;

namespace FDSim.Models.Game;
public class GamePlayer
{
    public string Name { get; init; }
    public string Surname { get; init; }
    public Nationality Nationality { get; init; }
    public int Age { get; init; }
    public string PrintName { get => $"{Name} {Surname}"; }
    public string ShortName { get => $"{Name.Substring(0, 1)}. {Surname}"; }
    public Perc Reputation { get; init; } = new(20);
    public GamePlayer(string FullName, int Age)
    {
        var names = FullName.Split(' ');
        Surname = names[names.Length - 1];
        Name = string.Join(" ", new ArraySegment<string>(names, 0, names.Length - 1));
    }
}
