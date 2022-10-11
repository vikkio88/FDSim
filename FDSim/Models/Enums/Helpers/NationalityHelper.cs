namespace FDSim.Models.Enums.Helpers;

using Enums;


public static class NationalityHelper
{
    public static String GetLocale(Nationality nationality)
    {
        return nationality switch
        {
            Nationality.English => "en_GB",
            Nationality.German => "de",
            Nationality.Italian => "it",
            Nationality.Spanish => "es",
            Nationality.French => "fr",
            Nationality.Dutch => "nl",
            Nationality.Polish => "pl",
            Nationality.Turkish => "tr",
            _ => "en_GB"
        };
    }

    public static String GetName(Nationality nationality)
    {
        return nationality switch
        {
            Nationality.English => "England",
            Nationality.German => "Germany",
            Nationality.Italian => "Italy",
            Nationality.Spanish => "Spain",
            Nationality.French => "France",
            Nationality.Dutch => "Netherlands",
            Nationality.Polish => "Poland",
            Nationality.Turkish => "Turkey",
            _ => "Unknown"
        };
    }
    public static String GetIsoCode(string nationality)
    {
        Enum.TryParse(nationality, out Nationality nationalityEnum);
        return GetIsoCode(nationalityEnum);
    }
    public static String GetIsoCode(Nationality nationality)
    {
        return nationality switch
        {
            Nationality.English => "gb-eng",
            Nationality.German => "de",
            Nationality.Italian => "it",
            Nationality.Spanish => "es",
            Nationality.French => "fr",
            Nationality.Dutch => "nl",
            Nationality.Polish => "pl",
            Nationality.Turkish => "tr",
            _ => "xx",
        };
    }
}
