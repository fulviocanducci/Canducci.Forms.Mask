namespace WinForm
{
    partial class FrmExample
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TxtCurrencyBr = new TextBox();
            TxtCurrencyUs = new TextBox();
            label1 = new Label();
            label2 = new Label();
            LblBr = new Label();
            SuspendLayout();
            // 
            // TxtCurrencyBr
            // 
            TxtCurrencyBr.Location = new Point(57, 22);
            TxtCurrencyBr.Name = "TxtCurrencyBr";
            TxtCurrencyBr.Size = new Size(139, 23);
            TxtCurrencyBr.TabIndex = 0;
            // 
            // TxtCurrencyUs
            // 
            TxtCurrencyUs.Location = new Point(57, 50);
            TxtCurrencyUs.Name = "TxtCurrencyUs";
            TxtCurrencyUs.Size = new Size(139, 23);
            TxtCurrencyUs.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(18, 30);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "Brasil:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(24, 58);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 3;
            label2.Text = "EUA:";
            // 
            // LblBr
            // 
            LblBr.AutoSize = true;
            LblBr.Location = new Point(202, 30);
            LblBr.Name = "LblBr";
            LblBr.Size = new Size(38, 15);
            LblBr.TabIndex = 4;
            LblBr.Text = "label3";
            // 
            // FrmExample
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(282, 94);
            Controls.Add(LblBr);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(TxtCurrencyUs);
            Controls.Add(TxtCurrencyBr);
            KeyPreview = true;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmExample";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Exemplos";
            Load += FrmExample_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox TxtCurrencyBr;
        private TextBox TxtCurrencyUs;
        private Label label1;
        private Label label2;
        private Label LblBr;
    }
}
