namespace FDSim.Models.People;

using FDSim.Models.Enums;

public abstract class Person : IdEntity
{
    public String Name { get; set; }
    public String Surname { get; set; }
    public int Age { get; set; }
    public Nationality Nationality { get; set; }

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
