namespace FDSim.Game.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using FDSim.Models.Game.League;
public class MatchIdToResultMap : IMultiValueConverter
{
    public static readonly MatchIdToResultMap Instance = new MatchIdToResultMap();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values.Count == 2 &&
            values[0] is string id &&
            values[1] is Dictionary<string, MatchResult> map)
        {
            if (map.ContainsKey(id))
            {
                return $"{map[id]}";
            }
        }

        return string.Empty;
    }
}
