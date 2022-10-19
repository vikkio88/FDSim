namespace MatchesGame.Helpers.Converters;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
public class IsPlayersTeam : IMultiValueConverter
{
    public static readonly IsPlayersTeam Instance = new IsPlayersTeam();
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {

        bool condition = ConversionHelpers.BoolFrom(parameter, true);

        if (values.Count == 2 &&
                values[1] is null
        )
        {
            return !condition;
        }

        if (
            values.Count == 2 &&
            values[0] is string teamId &&
            values[1] is string selectedTeamId)
        {
            return (teamId == selectedTeamId) == condition;
        }

        return condition;
    }
}
