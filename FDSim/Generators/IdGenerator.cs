namespace FDSim.Generators;

public interface IIdGenerator
{
    public String generate();
}
class IdGenerator : IIdGenerator
{
    public String generate()
    {
        return Ulid.NewUlid().ToString();
    }
}
