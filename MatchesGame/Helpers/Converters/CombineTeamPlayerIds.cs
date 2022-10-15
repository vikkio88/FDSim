namespace MatchesGame.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

public class CombineTeamPlayerIds : IMultiValueConverter
{
    public static readonly CombineTeamPlayerIds Instance = new CombineTeamPlayerIds();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values.Count == 2 &&
            values[0] is string teamId &&
            values[1] is string playerId)
        {
            return $"{teamId}.{playerId}";
        }

        return null;
    }
}
