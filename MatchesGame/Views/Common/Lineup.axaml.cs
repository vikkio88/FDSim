using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using FDSim.Models.People;
using FDSim.Models.Enums;
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

    public Player? GK { get; set; } = null;
    public List<Player> DF { get; set; } = new();
    public List<Player> MF { get; set; } = new();
    public List<Player> ST { get; set; } = new();

    static Lineup()
    {
        // This needs both Reactive.Linq and System
        ValueProperty.Changed.Select(o => o.Sender).OfType<Lineup>().Subscribe(l => l.OnValueChanged());
    }

    public void OnValueChanged()
    {
        GK = Value.Starters.Find(p => p.Role == Role.Goalkeeper);
        DF = Value.Starters.FindAll(p => p.Role == Role.Defender);
        MF = Value.Starters.FindAll(p => p.Role == Role.Midfielder);
        ST = Value.Starters.FindAll(p => p.Role == Role.Striker);
    }
}