using System;
using System.Globalization;
using System.IO;
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
            var filePath = $"Assets/Flags/{nationality}.svg";
            if (!File.Exists(filePath)) return false;
            using (var stream = File.OpenRead(filePath))
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