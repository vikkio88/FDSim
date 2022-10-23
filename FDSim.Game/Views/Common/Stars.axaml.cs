using System;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace FDSim.Game.Views.Common;
public class Stars : TemplatedControl
{
    public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<Stars, string>(nameof(Value), "1");
    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }
    public static readonly DirectProperty<Stars, bool[]> FullStarsProperty = AvaloniaProperty.RegisterDirect<Stars, bool[]>(nameof(FullStars), o => o._fullStars, (o, v) => o._fullStars = v);
    private bool[] _fullStars = { false, false, false, false, false };
    public bool[] FullStars { get => _fullStars; set => SetAndRaise(FullStarsProperty, ref _fullStars, value); }
    public static readonly DirectProperty<Stars, bool> HalfStarProperty = AvaloniaProperty.RegisterDirect<Stars, bool>(nameof(HalfStar), o => o._halfStar, (o, v) => o._halfStar = v);
    private bool _halfStar = false;
    public bool HalfStar { get => _halfStar; set => SetAndRaise(HalfStarProperty, ref _halfStar, value); }

    static Stars()
    {
        // This needs both Reactive.Linq and System
        ValueProperty.Changed.Select(o => o.Sender).OfType<Stars>().Subscribe(s => s.OnValueChanged());
    }

    public void OnValueChanged()
    {
        var (f, h) = StarsContext.FromString(Value);
        FullStars = f;
        HalfStar = h;
    }
}