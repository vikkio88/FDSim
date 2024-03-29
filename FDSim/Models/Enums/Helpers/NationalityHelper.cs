namespace FDSim.Models.Enums.Helpers;

using Enums;


public static class NationalityHelper
{
    public static String GetLocale(Nationality nationality)
    {
        return nationality switch
        {
            Nationality.English => "en",
            Nationality.German => "de",
            Nationality.Italian => "it",
            Nationality.Spanish => "es",
            Nationality.French => "fr",
            Nationality.Dutch => "nl",
            Nationality.Polish => "pl",
            Nationality.Turkish => "tr",
            _ => "en"
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

    public static Nationality? FromNationString(string nation)
    {
        return nation switch
        {
            "England" => Nationality.English,
            "Germany" => Nationality.German,
            "Italy" => Nationality.Italian,
            "Spain" => Nationality.Spanish,
            "France" => Nationality.French,
            "Netherlands" => Nationality.Dutch,
            "Poland" => Nationality.Polish,
            "Turkey" => Nationality.Turkish,
            _ => null,
        };
    }
    public static Nationality? FromNationalityString(string nation)
    {
        return nation switch
        {
            "Italian" => Nationality.Italian,
            "English" => Nationality.English,
            "German" => Nationality.German,
            "Spanish" => Nationality.Spanish,
            "French" => Nationality.French,
            "Dutch" => Nationality.Dutch,
            "Polish" => Nationality.Polish,
            "Turkish" => Nationality.Turkish,
            _ => null,
        };
    }

    public static List<string> GetNationalities()
    {
        return new()
        {
            "Italian",
            "English",
            "German",
            "Spanish",
            "French",
            "Dutch",
            "Polish",
            "Turkish",
        };
    }
    public static List<string> GetNations()
    {
        return new()
        {
            "England",
            "Germany",
            "Italy",
            "Spain",
            "France",
            "Netherlands",
            "Poland",
            "Turkey",
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
