using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace FDSim.Game.Views.Common;
public class Link : TemplatedControl
{
    public static readonly StyledProperty<string> LabelProperty = AvaloniaProperty.Register<Link, string>(nameof(Label));
    public string Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
    public static readonly StyledProperty<string> TextAlignmentProperty = AvaloniaProperty.Register<Link, string>(nameof(TextAlignment));
    public string TextAlignment
    {
        get => GetValue(TextAlignmentProperty);
        set => SetValue(TextAlignmentProperty, value);
    }

    public static readonly DirectProperty<Link, ICommand?> CommandProperty =
            AvaloniaProperty.RegisterDirect<Link, ICommand?>(nameof(Command),
                l => l.Command, (l, command) => l.Command = command);

    private ICommand? _command;
    public ICommand? Command
    {
        get => _command;
        set => SetAndRaise(CommandProperty, ref _command, value);
    }

    public static readonly StyledProperty<object?> CommandParameterProperty =
             AvaloniaProperty.Register<Link, object?>(nameof(CommandParameter));

    public object? CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    protected override void OnPointerReleased(PointerReleasedEventArgs e)
    {
        Command?.Execute(CommandParameter);
    }
}