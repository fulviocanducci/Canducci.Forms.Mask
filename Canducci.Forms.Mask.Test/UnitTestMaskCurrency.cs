using System.Globalization;
using System.Windows.Forms;

namespace Canducci.Forms.Mask.Test
{
    [TestFixture]
    [Apartment(ApartmentState.STA)]
    public class MaskCurrencyTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void InitialText_IsFormatted_WhenHandleCreatedBeforeAttach()
        {
            using var tb = new TextBox();
            // force handle creation
            var _ = tb.Handle;

            using var mask = new MaskCurrency(tb, 0m, new CultureInfo("pt-BR"));

            Assert.AreEqual("0,00", tb.Text);
        }

        [Test]
        public void SettingValue_UpdatesText_AndRaisesEvent()
        {
            using var tb = new TextBox();
            var _ = tb.Handle;

            using var mask = new MaskCurrency(tb, 0m, new CultureInfo("pt-BR"));

            bool changed = false;
            mask.ValueChanged += (s, e) => changed = true;

            mask.Value = 12.34m;

            Assert.IsTrue(changed);
            Assert.AreEqual("12,34", tb.Text);
        }

        [Test]
        public void ChangingCulture_UpdatesFormattedText()
        {
            using var tb = new TextBox();
            var _ = tb.Handle;

            using var mask = new MaskCurrency(tb, 1234.56m, new CultureInfo("pt-BR"));

            // initial in pt-BR is "1.234,56"
            Assert.AreEqual("1.234,56", tb.Text);

            mask.Culture = new CultureInfo("en-US");

            Assert.AreEqual("1,234.56", tb.Text);
        }
    }
}
