namespace MatchesGame.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

public class TablePlayedWDLToString : IMultiValueConverter
{
    public static readonly TablePlayedWDLToString Instance = new TablePlayedWDLToString();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values.Count == 4 &&
            values[0] is int played &&
            values[1] is int won &&
            values[2] is int drawn &&
            values[3] is int lost
            )
        {

            return $"{played} ({won}/{drawn}/{lost})";

        }

        return string.Empty;
    }
}
