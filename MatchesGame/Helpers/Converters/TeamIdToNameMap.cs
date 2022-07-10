namespace MatchesGame.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using FDSim.Models.Game.Team;
public class TeamIdToNameMap : IMultiValueConverter
{
    public static readonly TeamIdToNameMap Instance = new TeamIdToNameMap();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values.Count == 2 &&
            values[0] is string id &&
            values[1] is Dictionary<string, Team> map)
        {
            return $"{map[id].Name}";
        }

        return string.Empty;
    }
}
