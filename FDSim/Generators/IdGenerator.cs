namespace FDSim.Generators;

public interface IIdGenerator
{
    public String Generate();
}
class IdGenerator : IIdGenerator
{
    public String Generate()
    {
        return Ulid.NewUlid().ToString();
    }
}
