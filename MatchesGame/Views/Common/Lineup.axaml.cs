using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

using FLineup = FDSim.Models.Game.Team.Lineup;

namespace MatchesGame.Views.Common;
public class Lineup : TemplatedControl
{
    public static readonly DirectProperty<Lineup, FLineup> ValueProperty =
            AvaloniaProperty.RegisterDirect<Lineup, FLineup>(nameof(Value),
                l => l.Value, (l, command) => l.Value = command);

    private FLineup _value;
    public FLineup Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, value);
    }
}