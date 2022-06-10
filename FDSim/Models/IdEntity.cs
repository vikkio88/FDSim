namespace FDSim.Models;
public abstract class IdEntity
{
    public String Id { get; set; }
    public IdEntity()
    {
        Id = "";
    }

    public override string ToString()
    {
        return String.Format("id:{0}", Id);
    }

}
