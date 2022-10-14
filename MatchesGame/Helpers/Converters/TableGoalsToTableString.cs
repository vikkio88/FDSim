namespace MatchesGame.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

public class TableGoalsToTableString : IMultiValueConverter
{
    public static readonly TableGoalsToTableString Instance = new TableGoalsToTableString();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values.Count == 3 &&
            values[0] is int goalsScored &&
            values[1] is int goalsConceded &&
            values[2] is int goalsDiff)
        {

            return $"{goalsScored}:{goalsConceded} ({goalsDiff})";

        }

        return "0:0 (0)";
    }
}
