namespace MatchesGame.Helpers;
public static class ConversionHelpers
{
    public static bool BoolFrom(object? obj = null, bool defaultValue = false)
    {
        bool condition = defaultValue;
        bool.TryParse(obj?.ToString() ?? "False", out condition);
        return condition;
    }
}
