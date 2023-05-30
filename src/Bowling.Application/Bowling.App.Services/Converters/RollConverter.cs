using Bowling.App.Services.Constants;
using Bowling.Entities;

namespace Bowling.App.Services.Converters;
public class RollConverter
{
    public Dictionary<char, int> RollValues;

    public RollConverter()
    {
        this.RollValues = new Dictionary<char, int>
        {
            { '1', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'X', 10 },
            { '-', 0 }
        };
    }

    public int ConvertToInteger(char roll, Frame frame)
    {
        if (this.RollValues.TryGetValue(roll, out int value))
        {
            return value;
        }
        else
        {
            // Spare Case
            if (roll == '/')
            {
                return BowlingConstants.MAX_FRAME_ROLL_VALUE - frame.Rolls.First(); 
            }

            throw new ArgumentException($"Invalid roll value: {roll}");
        }
    }
}