using System.Globalization;

namespace Canducci.Forms.Mask;

/// <summary>
/// Provides extension methods for attaching a currency mask to a WinForms <see cref="TextBox"/>.
/// </summary>
public static class MaskExtensions
{
    /// <summary>
    /// Attaches a currency mask to the specified <see cref="TextBox"/>, allowing formatted currency input.
    /// </summary>
    /// <param name="textBox">The target text box to attach the mask to.</param>
    /// <param name="initialValue">Optional initial value to display (default 0).</param>
    /// <param name="culture">Optional culture used for formatting. If null, "pt-BR" is used.</param>
    /// <returns>An instance of <see cref="MaskCurrency"/> attached to <paramref name="textBox"/>.</returns>
    public static MaskCurrency MaskCurrency(this TextBox textBox, decimal initialValue = 0m, CultureInfo? culture = null)
    {
        return new MaskCurrency(textBox, initialValue, culture);
    }

    /// <summary>
    /// Attaches a currency mask to the specified <see cref="TextBox"/>, allowing formatted currency input with a default initial value of 0.
    /// </summary>
    /// <param name="textBox">The target text box to attach the mask to.</param>
    /// <param name="culture">Optional culture used for formatting. If null, "pt-BR" is used.</param>
    /// <returns>An instance of <see cref="MaskCurrency"/> attached to <paramref name="textBox"/>.</returns>
    public static MaskCurrency MaskCurrency(this TextBox textBox, CultureInfo? culture = null)
    {
        return new MaskCurrency(textBox, 0m, culture);
    }

    /// <summary>
    /// Attaches a currency mask to the specified <see cref="TextBox"/>, allowing formatted currency input.
    /// </summary>
    /// <param name="textBox">The target text box to attach the mask to.</param>
    /// <param name="initialValue">Optional initial value to display (default 0).</param>
    /// <param name="culture">Optional culture used for formatting. If null, "pt-BR" is used.</param>
    /// <returns>An instance of <see cref="MaskCurrency"/> attached to <paramref name="textBox"/>.</returns>
    public static MaskCurrency MaskCurrency(this TextBox textBox, decimal initialValue = 0m, string culture = null)
    {
        return new MaskCurrency(textBox, initialValue, CultureInfo.GetCultureInfo(culture ?? "pt-BR"));
    }

    /// <summary>
    /// Attaches a currency mask to the specified <see cref="TextBox"/>, allowing formatted currency input with a default initial value of 0.
    /// </summary>
    /// <param name="textBox">The target text box to attach the mask to.</param>
    /// <param name="culture">Optional culture used for formatting. If null, "pt-BR" is used.</param>
    /// <returns>An instance of <see cref="MaskCurrency"/> attached to <paramref name="textBox"/>.</returns>
    public static MaskCurrency MaskCurrency(this TextBox textBox, string culture = null)
    {
        return new MaskCurrency(textBox, 0m, CultureInfo.GetCultureInfo(culture ?? "pt-BR"));
    }
}
