using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace MatchesGame.Views.Common;
public class Stars : TemplatedControl
{
    public bool[] FullStars { get; } = { true, true, true, true, true, };
    public bool HalfStar { get; } = false;
}