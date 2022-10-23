namespace FDSim.Game.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using FDSim.Models.Game.Team;
public class PlayerIdTeamIdToNameMap : IMultiValueConverter
{
    public static readonly PlayerIdTeamIdToNameMap Instance = new PlayerIdTeamIdToNameMap();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        if (
            values.Count == 3 &&
            values[0] is string playerId &&
            values[1] is string teamId &&
            values[2] is Dictionary<string, Team> map)
        {
            var team = map[teamId];
            var player = team?.Roster?.GetById(playerId);
            return $"{player?.PrintName ?? ""}";
        }

        return string.Empty;
    }
}
