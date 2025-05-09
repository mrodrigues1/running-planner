namespace RunningPlanner.Core.Extensions; // Or any other appropriate namespace

public static class DecimalExtensions
{
    /// <summary>
    /// Rounds a decimal value to a specified number of fractional digits.
    /// </summary>
    /// <param name="value">The decimal number to round.</param>
    /// <param name="decimalPlaces">The number of decimal places to round to. Must be between 0 and 28.</param>
    /// <returns>The decimal number rounded to the specified number of decimal places.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if decimalPlaces is less than 0 or greater than 28.</exception>
    public static decimal RoundTo(this decimal value, int decimalPlaces)
    {
        // Math.Round handles the range check (0-28) and throws ArgumentOutOfRangeException if needed.
        return Math.Round(value, decimalPlaces, MidpointRounding.AwayFromZero);
        // Or use MidpointRounding.ToEven if you prefer banker's rounding
    }
}
