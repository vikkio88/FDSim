namespace FDSim.Models.Common;
public class Perc : IComparable<Perc>
{
    private int _value = 0;
    public int Value { get => _value; set { _value = Math.Max(0, Math.Min(value, 100)); } }
    public Perc(int value)
    {
        Value = value;
    }
    public int CompareTo(Perc? other)
    {
        if (other is null) return 0;
        return other.Value > Value ? 1 : -1;
    }

    public override string ToString()
    {
        return $"{Value}%";
    }
}
