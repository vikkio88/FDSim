namespace FDSim.Models.People;

using FDSim.Models.Enums;

public abstract class Person : IdEntity
{
    public String Name { get; init; }
    public String Surname { get; init; }
    public int Age { get; set; }

    // Here I might CAP it to 100
    public int SkillAvg { get; set; }

    public Nationality Nationality { get; init; }

    public Person()
    {
        Name = "";
        Surname = "";
        Age = 0;
        Nationality = Nationality.English;
    }

    public Person(String name, String surname, int age)
    {
        Name = name;
        Surname = surname;
        Age = age;
    }

    public override string ToString()
    {
        return String.Format("{0}_Person: {1} {2} ({3}) - {4}", base.ToString(), Name, Surname, Age, Nationality);
    }

}
