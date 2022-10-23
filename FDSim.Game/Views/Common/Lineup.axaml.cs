using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using FDSim.Models.People;
using FDSim.Models.Enums;
using FLineup = FDSim.Models.Game.Team.Lineup;
using System.Reactive;
using ReactiveUI;
using System.Windows.Input;

namespace FDSim.Game.Views.Common;
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
    public List<LineupSpot> DF { get; set; } = new();
    public List<LineupSpot> MF { get; set; } = new();
    public List<LineupSpot> ST { get; set; } = new();

    public static readonly DirectProperty<Lineup, ICommand?> OnPlayerClickProperty =
           AvaloniaProperty.RegisterDirect<Lineup, ICommand?>(nameof(OnPlayerClick),
               l => l.OnPlayerClick, (l, command) => l.OnPlayerClick = command);

    private ICommand? _onPlayerClick;
    public ICommand? OnPlayerClick
    {
        get => _onPlayerClick;
        set => SetAndRaise(OnPlayerClickProperty, ref _onPlayerClick, value);
    }

    public ReactiveUI.ReactiveCommand<string, Unit> OnPlayerClickInternal { get; set; }

    static Lineup()
    {
        // This needs both Reactive.Linq and System
        ValueProperty.Changed.Select(o => o.Sender).OfType<Lineup>().Subscribe(l => l.OnValueChanged());
    }

    public void OnValueChanged()
    {


        var teamId = Value.TeamId;
        OnPlayerClickInternal = ReactiveCommand.Create((string playerId) =>
        {
            OnPlayerClick?.Execute($"{teamId}.{playerId}");
        });

        GK = Value.Starters.Find(p => p.Role == Role.Goalkeeper);
        DF = Value.Starters.FindAll(p => p.Role == Role.Defender).Select(p => new LineupSpot(p, OnPlayerClickInternal)).ToList();
        MF = Value.Starters.FindAll(p => p.Role == Role.Midfielder).Select(p => new LineupSpot(p, OnPlayerClickInternal)).ToList();
        ST = Value.Starters.FindAll(p => p.Role == Role.Striker).Select(p => new LineupSpot(p, OnPlayerClickInternal)).ToList();
    }
}

public class LineupSpot
{
    public Player Player { get; }
    public ICommand OnClick { get; }

    public LineupSpot(Player player, ICommand onClick)
    {
        Player = player;
        OnClick = onClick;
    }

}