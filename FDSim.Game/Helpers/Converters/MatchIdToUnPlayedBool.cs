namespace FDSim.Game.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using FDSim.Models.Game.League;
public class MatchIdToUnPlayedBool : IMultiValueConverter
{
    public static readonly MatchIdToUnPlayedBool Instance = new MatchIdToUnPlayedBool();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values.Count == 2 &&
            values[0] is string id &&
            values[1] is Dictionary<string, MatchResult> map)
        {
            return !map.ContainsKey(id);
        }

        return false;
    }
}
