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
}
