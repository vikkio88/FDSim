namespace FDSim.Tests.Generators.Mocks;

using FDSim.Generators;

class MockedIdGen : IIdGenerator
{
    public String Generate() { return "fakeTestId"; }
}
