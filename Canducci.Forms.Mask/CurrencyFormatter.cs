using System.Globalization;

namespace Canducci.Forms.Mask;

/// <summary>
/// Provides formatting and parsing helpers for currency values (separated from WinForms).
/// </summary>
internal static class CurrencyFormatter
{
    public static string ToString(long cents, CultureInfo culture, string format = "#,##0.00")
    {
        decimal value = cents / 100m;
        return value.ToString(format, culture);
    }

    public static long FromDecimal(decimal value)
    {
        // Round to cents and clamp to long range
        decimal cents = Math.Round(value * 100m, MidpointRounding.AwayFromZero);
        if (cents > long.MaxValue) return long.MaxValue;
        if (cents < long.MinValue) return long.MinValue;
        return (long)cents;
    }

    public static bool TryParse(string text, CultureInfo culture, out long cents)
    {
        cents = 0;
        if (decimal.TryParse(text, NumberStyles.Number | NumberStyles.AllowDecimalPoint, culture, out var d))
        {
            cents = FromDecimal(d);
            return true;
        }
        return false;
    }
}

