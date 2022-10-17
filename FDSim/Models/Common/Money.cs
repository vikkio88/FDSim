using System;
using System.Globalization;

namespace FDSim.Models.Common;
public class Money
{
    private double _value = 0;
    public double Value { get => _value; }
    private string _currency;
    public Money(int value, string currency = "£")
    {
        _value = value;
        _currency = currency;
    }

    public Money(double value, string currency = "£")
    {
        _value = value;
        _currency = currency;
    }

    public static Money MakeH(double value, string currency = "£") => new(value * 100, currency);
    public static Money MakeM(double value, string currency = "£") => new(value * 1_000_000, currency);
    public static Money MakeK(double value, string currency = "£") => new(value * 1_000, currency);

    private string formatToKMB()
    {
        double effectiveValue = _value;
        if (effectiveValue > 999999999 || effectiveValue < -999999999)
        {
            return effectiveValue.ToString("0,,,.###b", CultureInfo.InvariantCulture);
        }

        if (effectiveValue > 999999 || effectiveValue < -999999)
        {
            return effectiveValue.ToString("0,,.##m", CultureInfo.InvariantCulture);
        }

        if (effectiveValue > 999 || effectiveValue < -999)
        {
            return effectiveValue.ToString("0,.#k", CultureInfo.InvariantCulture);
        }


        return effectiveValue.ToString(CultureInfo.InvariantCulture);
    }

    public override string ToString()
    {
        return $"{_currency} {formatToKMB()}";
    }

    public static Money operator +(Money a, Money b)
    {
        return new(a._value + b._value, a._currency);
    }

    public static Money operator -(Money a, Money b)
    {
        return new(a._value - b._value, a._currency);
    }
}
