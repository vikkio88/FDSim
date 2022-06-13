namespace FDSim.Generators.DataProviders;

using Models.Enums;
static public class TeamNameTemplates
{
    static public readonly string[] en_GB = {
        "{0}",
        "City of {0}",
        "{0} City",
        "{0} Sport",
        "{0} FC",
        "{0} United",
        "{0} Football",
        };
    static public readonly string[] de = {
        "{0}",
        "{0} Fussball",
        "{0} Sport",
        "{0} FC",
        };
    static public readonly string[] it = {
        "{0}",
        "Città di {0}",
        "{0} Sport",
        "{0} FC",
        "AC {0}",
        "{0} AC",
        "Real {0}",
        "{0} Football",
        "{0} Calcio",
        };

    static public readonly string[] es = {
        "{0}",
        "Real {0}",
        "{0} Sport",
        "Atletico {0}",
        "Deportivo {0}",
        "{0} Football",
        "{0} FC",
        };
    static public readonly string[] fr = {
        "{0}",
        "{0} FC",
        "US {0}",
        "AC {0}",
        "CS {0}",
        };

    static public readonly string[] nl = {
        "{0}",
        "{0} FC",
        };
    static public readonly string[] pl = {
        "{0}",
        "{0} FC",
        "{0} Klub Sportowy",
        };
    static public readonly string[] tr = {
        "{0}",
        "{0} FC",
        "{0}spor",
        };

    static public String[] ByNationality(Nationality nationality)
    {
        return nationality switch
        {
            Nationality.English => en_GB,
            Nationality.German => de,
            Nationality.Italian => it,
            Nationality.Spanish => es,
            Nationality.French => fr,
            Nationality.Dutch => nl,
            Nationality.Polish => pl,
            Nationality.Turkish => tr,
            _ => en_GB
        };
    }

}
