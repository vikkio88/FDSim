using System;
using Avalonia;
using Avalonia.Controls.Primitives;

namespace MatchesGame.Views.Common;
public class Flag : TemplatedControl
{
    public static readonly StyledProperty<string> IsoProperty = AvaloniaProperty.Register<Flag, string>(nameof(Iso));
    public string Iso
    {
        get => GetValue(IsoProperty);
        set => SetValue(IsoProperty, value);
    }

}