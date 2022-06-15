namespace FDSim.Models;
public abstract class IdEntity
{
    public String Id { get; set; }
    public IdEntity()
    {
        //@TODO: Maybe not doing this by default?
        Id = this.GetHashCode().ToString();
    }

    public override string ToString()
    {
        return String.Format("id:{0}", Id);
    }

}
