namespace FDSim.Models.Enums.Helpers;

using Enums;


public static class NationalityHelper
{
    public static String getLocale(Nationality nationality)
    {
        switch (nationality)
        {
            case Nationality.English: { return "en_GB"; }
            case Nationality.German: { return "de"; }
            case Nationality.Italian: { return "it"; }
            case Nationality.Spanish: { return "es"; }
            case Nationality.French: { return "fr"; }
            case Nationality.Dutch: { return "nl"; }
            case Nationality.Polish: { return "pl"; }
            case Nationality.Turkish: { return "tr"; }
            default: { return "en_GB"; }
        }
    }
}
