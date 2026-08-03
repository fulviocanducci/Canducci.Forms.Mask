using System.Globalization;

namespace Canducci.Forms.Mask.Test
{
    public class CurrencyFormatterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToString_Formats_PtBR()
        {
            var result = CurrencyFormatter.ToString(12345L, new CultureInfo("pt-BR"), "#,##0.00");
            Assert.AreEqual("123,45", result);
        }

        [Test]
        public void FromDecimal_Rounds_AwayFromZero()
        {
            var result = CurrencyFormatter.FromDecimal(12.345m);
            // 12.345 * 100 = 1234.5 -> rounded away from zero => 1235
            Assert.AreEqual(1235L, result);
        }

        [Test]
        public void TryParse_PtBR_Works()
        {
            var ok = CurrencyFormatter.TryParse("1.234,56", new CultureInfo("pt-BR"), out var cents);
            Assert.IsTrue(ok);
            Assert.AreEqual(123456L, cents);
        }
    }
}
