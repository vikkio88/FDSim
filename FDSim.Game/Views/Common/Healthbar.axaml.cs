using System;
using System.Linq;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace FDSim.Game.Views.Common;

/**
Usage examples

                <common:Healthbar Value="80"
                    Width="100" Height="20"/>
                <common:Healthbar Value="50" Max="100"
                    Width="200" Height="20"/>
                <common:Healthbar Value="100" Max="100"
                    Width="200" Height="20"/>
                <common:Healthbar Value="10" Max="100"
                    Width="100" Height="20"/>
                <common:Healthbar Value="30" Max="100"
                    Width="100" Height="20"/>
**/
public class Healthbar : TemplatedControl
{
    public static readonly DirectProperty<Healthbar, int> ValueProperty =
               AvaloniaProperty.RegisterDirect<Healthbar, int>(nameof(Value),
                   l => l.Value, (l, value) => l.Value = value, 25);

    private int _value;
    public int Value
    {
        get => _value;
        set => SetAndRaise(ValueProperty, ref _value, value);
    }

    public static readonly DirectProperty<Healthbar, int> MaxProperty =
               AvaloniaProperty.RegisterDirect<Healthbar, int>(nameof(Value),
                   l => l.Max, (l, value) => l.Max = value, 100);

    private int _max;
    public int Max
    {
        get => _max;
        set => SetAndRaise(MaxProperty, ref _max, value);
    }

    public IBrush Colour { get; set; } = Avalonia.Media.Brush.Parse("Green");
    public string Info { get; set; } = string.Empty;
    static Healthbar()
    {
        WidthProperty.Changed.Select(o => o.Sender).OfType<Healthbar>().Subscribe(l => l.OnValueChanged());
    }

    public void OnValueChanged()
    {
        Max = (Max != 0 ? Max : 100);
        var perc = Value / (double)Max;
        Colour = perc switch
        {
            >= .7 => Brush.Parse("#008450"),
            >= .5 => Brush.Parse("#778400"),
            >= .3 => Brush.Parse("#B85A1C"),
            _ => Brush.Parse("#B81D13"),
        };

        Info = $"{Value}/{Max}";

        Value = Value * (int)Width / Max;
    }
}