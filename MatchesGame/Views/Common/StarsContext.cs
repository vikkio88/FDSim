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
        Init((int)value);
    }

    public void Init(int value)
    {
        bool[] tempFullStars = { false, false, false, false, false };
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
            HalfStar = true;
        }
        FullStars = tempFullStars;
    }
}
