using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Svg.Skia;

namespace MatchesGame.Views.Common;
public class IsoToFlagConverter : IValueConverter
{
    public static readonly IsoToFlagConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string nationality)
        {
            var src = new SvgSource();
            //        if (File.Exists(path)) otherwise maybe return empty
            using (var stream = System.IO.File.OpenRead($"Assets/Flags/{nationality}.svg"))
            {
                src.Load(stream);
            }
            return src;
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}