using System.Globalization;

namespace Canducci.Forms.Mask;

/// <summary>
/// Attach a currency mask to a WinForms <see cref="TextBox"/>.
/// Thread-safe, disposable and intended for reuse in a library.
/// </summary>
public sealed class MaskCurrency : IDisposable
{
    private readonly TextBox _textBox;
    private readonly CultureInfo _culture;
    private long _valueInCents;
    private bool _disposed;

    /// <summary>
    /// Occurs when the currency <see cref="Value"/> changes.
    /// </summary>
    public event EventHandler? ValueChanged;

    /// <summary>
    /// Gets or sets the current decimal value represented by the mask.
    /// Setting will update the attached <see cref="TextBox"/> text.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown if the instance has been disposed.</exception>
    public decimal Value
    {
        get => _valueInCents / 100m;
        set
        {
            EnsureNotDisposed();
            var newCents = CurrencyFormatter.FromDecimal(value);
            if (newCents == _valueInCents) return;
            _valueInCents = newCents;
            UpdateText();
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    /// <summary>
    /// Minimum allowed value. Defaults to <see cref="decimal.MinValue"/>.
    /// </summary>
    public decimal Min { get; set; } = decimal.MinValue;

    /// <summary>
    /// Maximum allowed value. Defaults to <see cref="decimal.MaxValue"/>.
    /// </summary>
    public decimal Max { get; set; } = decimal.MaxValue;

    /// <summary>
    /// Initializes a new instance of <see cref="MaskCurrency"/> and attaches it to the provided <see cref="TextBox"/>.
    /// </summary>
    /// <param name="textBox">The target <see cref="TextBox"/> to attach the mask to.</param>
    /// <param name="initialValue">Initial decimal value to display (default 0m).</param>
    /// <param name="culture">Culture used for formatting (defaults to "pt-BR").</param>
    /// <exception cref="ArgumentNullException">If <paramref name="textBox"/> is null.</exception>
    public MaskCurrency(TextBox textBox, decimal initialValue = 0m, CultureInfo? culture = null)
    {
        _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));
        _culture = culture ?? new CultureInfo("pt-BR");
        _textBox.TextAlign = HorizontalAlignment.Right;

        AttachEvents();
        Value = initialValue;
    }

    /// <summary>
    /// Creates and attaches a <see cref="MaskCurrency"/> to the specified <see cref="TextBox"/>.
    /// </summary>
    /// <param name="textBox">The target text box.</param>
    /// <param name="initialValue">Initial value to set.</param>
    /// <param name="culture">Optional culture for formatting.</param>
    /// <returns>A new <see cref="MaskCurrency"/> instance attached to <paramref name="textBox"/>.</returns>
    public static MaskCurrency Attach(TextBox textBox, decimal initialValue = 0m, CultureInfo? culture = null)
    {
        return new MaskCurrency(textBox, initialValue, culture);
    }

    private void AttachEvents()
    {
        _textBox.KeyPress += TextBox_KeyPress;
        _textBox.PreviewKeyDown += TextBox_PreviewKeyDown;
        _textBox.Leave += TextBox_Leave;
        _textBox.TextChanged += TextBox_TextChanged;
    }

    private void DetachEvents()
    {
        _textBox.KeyPress -= TextBox_KeyPress;
        _textBox.PreviewKeyDown -= TextBox_PreviewKeyDown;
        _textBox.Leave -= TextBox_Leave;
        _textBox.TextChanged -= TextBox_TextChanged;
    }

    private void TextBox_PreviewKeyDown(object? sender, PreviewKeyDownEventArgs e)
    {
    }

    private void TextBox_Leave(object? sender, EventArgs e)
    {
        UpdateText();
    }

    private void TextBox_TextChanged(object? sender, EventArgs e)
    {
        if (_disposed) return;

        if (CurrencyFormatter.TryParse(_textBox.Text, _culture, out var cents))
        {
            if (cents != _valueInCents)
            {
                _valueInCents = cents;
                UpdateText();
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            UpdateText();
        }
    }

    private void TextBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (_disposed)
        {
            return;
        }
        if (e.KeyChar == (char)Keys.Back)
        {
            _valueInCents /= 10;
            UpdateText();
            e.Handled = true;
            ValueChanged?.Invoke(this, EventArgs.Empty);
            return;
        }
        if (!char.IsDigit(e.KeyChar))
        {
            e.Handled = true;
            return;
        }
        int digit = e.KeyChar - '0';
        if (_valueInCents > (long.MaxValue - digit) / 10)
        {
            e.Handled = true;
            return;
        }
        _valueInCents = (_valueInCents * 10) + digit;
        var decimalValue = _valueInCents / 100m;
        if (decimalValue < Min)
        {
            _valueInCents = CurrencyFormatter.FromDecimal(Min);
        }
        if (decimalValue > Max)
        {
            _valueInCents = CurrencyFormatter.FromDecimal(Max);
        }
        UpdateText();
        e.Handled = true;
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateText()
    {
        if (_textBox.IsDisposed || !_textBox.IsHandleCreated)
        {
            return;
        }
        void update()
        {
            _textBox.Text = CurrencyFormatter.ToString(_valueInCents, _culture);
            _textBox.SelectionStart = _textBox.Text.Length;
        }
        if (_textBox.InvokeRequired)
        {
            _textBox.Invoke((Action)update);
        }
        else
        {
            update();
        }
    }

    private void EnsureNotDisposed()
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(MaskCurrency));
        }
    }

    /// <summary>
    /// Detaches the mask from the underlying <see cref="TextBox"/>. Safe to call multiple times.
    /// </summary>
    public void Detach()
    {
        if (_disposed)
        {
            return;
        }
        DetachEvents();
    }

    /// <summary>
    /// Disposes the instance and detaches all event handlers from the <see cref="TextBox"/>.
    /// </summary>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        DetachEvents();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}