namespace FDSim.Models.Common;
public class Perc
{
    private int _value = 0;
    public int Value { get => _value; set { _value = Math.Max(0, Math.Min(value, 100)); } }
    public Perc(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Value}%";
    }
}
