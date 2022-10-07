namespace MatchesGame.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using FDSim.Models.People;
public class PlayerIdToPlayerFromRoster : IMultiValueConverter
{
    public static readonly PlayerIdToPlayerFromRoster Instance = new PlayerIdToPlayerFromRoster();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values.Count == 2 &&
            values[0] is string id &&
            values[1] is Dictionary<string, Player> map)
        {
            return $"{map[id].PrintName}";
        }

        return string.Empty;
    }
}
