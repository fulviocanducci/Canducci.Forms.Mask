using Canducci.Forms.Mask;
using System.Globalization;
namespace WinForm
{
    public partial class FrmExample : Form
    {
        public FrmExample()
        {
            InitializeComponent();
        }

        private void FrmExample_Load(object sender, EventArgs e)
        {
            var maskBr = TxtCurrencyBr.MaskCurrency(100, new CultureInfo("pt-BR"));
            maskBr.LeaveCalled += MaskBr_LeaveCalled;

            TxtCurrencyUs.MaskCurrency(new CultureInfo("en-US"));
        }

        private void MaskBr_LeaveCalled(object? sender, EventArgs e)
        {
            LblBr.Text = TxtCurrencyBr.Text;
        }
    }
}
