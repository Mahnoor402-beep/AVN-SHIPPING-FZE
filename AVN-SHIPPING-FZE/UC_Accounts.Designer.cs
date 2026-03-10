namespace AVN_SHIPPING_FZE
{
    partial class UC_Accounts
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tabControl = new TabControl();
            tabInvoices = new TabPage();
            tabExpenses = new TabPage();
            tabPL = new TabPage();
            tabControl.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tabInvoices);
            tabControl.Controls.Add(tabExpenses);
            tabControl.Controls.Add(tabPL);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Font = new Font("Arial", 10F);
            tabControl.Location = new Point(0, 0);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(1326, 472);
            tabControl.TabIndex = 0;
            // 
            // tabInvoices
            // 
            tabInvoices.BackColor = Color.FromArgb(245, 246, 250);
            tabInvoices.Location = new Point(4, 32);
            tabInvoices.Name = "tabInvoices";
            tabInvoices.Padding = new Padding(5);
            tabInvoices.Size = new Size(1318, 436);
            tabInvoices.TabIndex = 0;
            tabInvoices.Text = "📄  Invoices";
            // 
            // tabExpenses
            // 
            tabExpenses.BackColor = Color.FromArgb(245, 246, 250);
            tabExpenses.Location = new Point(4, 32);
            tabExpenses.Name = "tabExpenses";
            tabExpenses.Padding = new Padding(5);
            tabExpenses.Size = new Size(1318, 436);
            tabExpenses.TabIndex = 1;
            tabExpenses.Text = "💸  Expenses";
            tabExpenses.Click += tabExpenses_Click;
            // 
            // tabPL
            // 
            tabPL.BackColor = Color.FromArgb(245, 246, 250);
            tabPL.Location = new Point(4, 32);
            tabPL.Name = "tabPL";
            tabPL.Padding = new Padding(5);
            tabPL.Size = new Size(1318, 436);
            tabPL.TabIndex = 2;
            tabPL.Text = "📊  Profit & Loss";
            // 
            // UC_Accounts
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 246, 250);
            Controls.Add(tabControl);
            Name = "UC_Accounts";
            Size = new Size(1326, 472);
            tabControl.ResumeLayout(false);
            ResumeLayout(false);
        }

        private TabControl tabControl;
        private TabPage tabInvoices, tabExpenses, tabPL;
    }
}