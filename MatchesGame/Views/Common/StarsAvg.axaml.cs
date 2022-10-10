using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace MatchesGame.Views.Common;
public class StarsAvg : TemplatedControl
{

    public static readonly StyledProperty<string> ValueProperty = AvaloniaProperty.Register<StarsAvg, string>(nameof(Value), "1");
    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);

    }

    public static readonly DirectProperty<StarsAvg, bool[]> FullStarsProperty = AvaloniaProperty.RegisterDirect<StarsAvg, bool[]>(nameof(FullStars), o => o._fullStars, (o, v) => o._fullStars = v);
    private bool[] _fullStars = { false, false, false, false, false };
    public bool[] FullStars { get => _fullStars; set => SetAndRaise(FullStarsProperty, ref _fullStars, value); }
    public static readonly DirectProperty<StarsAvg, bool> HalfStarProperty = AvaloniaProperty.RegisterDirect<StarsAvg, bool>(nameof(HalfStar), o => o._halfStar, (o, v) => o._halfStar = v);
    private bool _halfStar = false;
    public bool HalfStar { get => _halfStar; set => SetAndRaise(HalfStarProperty, ref _halfStar, value); }
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        var (f, h) = StarsContext.FromString(Value);
        FullStars = f;
        HalfStar = h;
    }
}