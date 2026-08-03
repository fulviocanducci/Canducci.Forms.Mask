using System.Globalization;

namespace Canducci.Forms.Mask;

public class Currency
{
    private readonly TextBox _textBox;
    private long _valueInCents;

    public Currency(TextBox textBox, string value = "0,00")
    {
        _textBox = textBox;
        _textBox.Text = value;
        _textBox.TextAlign = HorizontalAlignment.Right;
        _textBox.KeyPress += TextBox_KeyPress;
    }

    private void TextBox_KeyPress(object? sender, KeyPressEventArgs e)
    {
        if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
        {
            e.Handled = true;
            return;
        }
        if (e.KeyChar == (char)Keys.Back)
        {
            _valueInCents /= 10;
            UpdateText();
            e.Handled = true;
            return;
        }
        int digit = e.KeyChar - '0';
        _valueInCents = (_valueInCents * 10) + digit;
        UpdateText();
        e.Handled = true;
    }

    private void UpdateText()
    {
        decimal value = _valueInCents / 100m;

        _textBox.Text = value.ToString("#,##0.00", new CultureInfo("pt-BR"));
        _textBox.SelectionStart = _textBox.Text.Length;
    }

    public decimal Value
    {
        get
        {
            return _valueInCents / 100m;
        }
        set
        {
            _valueInCents = (long)(value * 100);
            UpdateText();
        }
    }

    public static Currency Add(TextBox textBox, string value = "0,00")
    {
        return new Currency(textBox, value);
    }
}