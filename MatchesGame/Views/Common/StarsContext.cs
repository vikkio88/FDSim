namespace MatchesGame.Views.Common;

using System.Linq;
public class StarsContext
{
    public bool[] FullStars { get; set; } = { false, false, false, false, false };
    public bool HalfStar { get; set; } = false;

    public StarsContext(int value)
    {
        Init(value);
    }
    public StarsContext(double value)
    {
        Init(value);
    }

    public void Init(double value)
    {
        var (tempFullStars, tempHalfStar) = calculateStars(value);
        FullStars = tempFullStars;
        HalfStar = tempHalfStar;
    }

    private static (bool[], bool) calculateStars(double value)
    {
        bool[] tempFullStars = { false, false, false, false, false };
        bool tempHalfStar = false;
        double starValue = value * 0.05;
        int fullStars = (int)starValue;
        foreach (var (_, i) in tempFullStars.Select((value, i) => (value, i)))
        {
            if (i < fullStars)
            {
                tempFullStars[i] = true;
            }
        }

        if (starValue - fullStars >= .5)
        {
            tempHalfStar = true;
        }

        return (tempFullStars, tempHalfStar);
    }

    public static (bool[], bool) FromString(string value)
    {
        return calculateStars(double.Parse(value));
    }
}
