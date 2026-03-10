namespace AVN_SHIPPING_FZE
{
    partial class UC_Billing
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
            this.lblTitle = new Label();
            this.lblCustomer = new Label();
            this.lblShipment = new Label();
            this.lblInvoiceDate = new Label();
            this.lblAmount = new Label();
            this.lblTax = new Label();
            this.lblTotal = new Label();
            this.lblStatus = new Label();
            this.lblSearch = new Label();
            this.cmbCustomer = new ComboBox();
            this.cmbShipment = new ComboBox();
            this.cmbStatus = new ComboBox();
            this.dtpInvoiceDate = new DateTimePicker();
            this.txtAmount = new TextBox();
            this.txtTax = new TextBox();
            this.txtTotal = new TextBox();
            this.txtSearch = new TextBox();
            this.btnAdd = new Button();
            this.btnUpdate = new Button();
            this.btnDelete = new Button();
            this.btnClear = new Button();
            this.dgvBilling = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)this.dgvBilling).BeginInit();
            this.SuspendLayout();

            // ── TITLE ─────────────────────────────
            this.lblTitle.Text = "💰  BILLING & INVOICING";
            this.lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.FromArgb(0, 51, 102);
            this.lblTitle.Location = new Point(20, 15);
            this.lblTitle.AutoSize = true;

            // ── LABELS ────────────────────────────
            this.lblCustomer.Text = "Customer *";
            this.lblCustomer.Location = new Point(20, 70);
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Font = new Font("Arial", 9F, FontStyle.Bold);

            this.lblShipment.Text = "Shipment";
            this.lblShipment.Location = new Point(400, 70);
            this.lblShipment.AutoSize = true;
            this.lblShipment.Font = new Font("Arial", 9F, FontStyle.Bold);

            this.lblInvoiceDate.Text = "Invoice Date";
            this.lblInvoiceDate.Location = new Point(20, 115);
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Font = new Font("Arial", 9F, FontStyle.Bold);

            this.lblStatus.Text = "Payment Status";
            this.lblStatus.Location = new Point(400, 115);
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new Font("Arial", 9F, FontStyle.Bold);

            this.lblAmount.Text = "Amount (AED)";
            this.lblAmount.Location = new Point(20, 160);
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new Font("Arial", 9F, FontStyle.Bold);

            this.lblTax.Text = "Tax %";
            this.lblTax.Location = new Point(400, 160);
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new Font("Arial", 9F, FontStyle.Bold);

            this.lblTotal.Text = "Total (AED)";
            this.lblTotal.Location = new Point(600, 160);
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new Font("Arial", 9F, FontStyle.Bold);

            this.lblSearch.Text = "🔍 Search:";
            this.lblSearch.Location = new Point(20, 260);
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new Font("Arial", 9F, FontStyle.Bold);

            // ── DROPDOWNS ─────────────────────────
            this.cmbCustomer.Location = new Point(130, 67);
            this.cmbCustomer.Size = new Size(230, 26);
            this.cmbCustomer.Font = new Font("Arial", 9F);
            this.cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbCustomer.FlatStyle = FlatStyle.Flat;

            this.cmbShipment.Location = new Point(500, 67);
            this.cmbShipment.Size = new Size(350, 26);
            this.cmbShipment.Font = new Font("Arial", 9F);
            this.cmbShipment.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbShipment.FlatStyle = FlatStyle.Flat;

            this.cmbStatus.Location = new Point(520, 112);
            this.cmbStatus.Size = new Size(200, 26);
            this.cmbStatus.Font = new Font("Arial", 9F);
            this.cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbStatus.FlatStyle = FlatStyle.Flat;
            this.cmbStatus.Items.AddRange(new string[]
            { "Unpaid", "Paid", "Partial", "Overdue" });
            this.cmbStatus.SelectedIndex = 0;

            // ── DATE PICKER ───────────────────────
            this.dtpInvoiceDate.Location = new Point(130, 112);
            this.dtpInvoiceDate.Size = new Size(220, 26);
            this.dtpInvoiceDate.Font = new Font("Arial", 9F);
            this.dtpInvoiceDate.Format = DateTimePickerFormat.Short;
            this.dtpInvoiceDate.Value = DateTime.Today;

            // ── TEXTBOXES ─────────────────────────
            this.txtAmount.Location = new Point(130, 157);
            this.txtAmount.Size = new Size(220, 26);
            this.txtAmount.Font = new Font("Arial", 9F);

            this.txtTax.Location = new Point(450, 157);
            this.txtTax.Size = new Size(100, 26);
            this.txtTax.Font = new Font("Arial", 9F);
            this.txtTax.Text = "5";

            this.txtTotal.Location = new Point(700, 157);
            this.txtTotal.Size = new Size(220, 26);
            this.txtTotal.Font = new Font("Arial", 9F);
            this.txtTotal.BackColor = Color.FromArgb(230, 255, 230);
            this.txtTotal.ReadOnly = true;

            this.txtSearch.Location = new Point(130, 257);
            this.txtSearch.Size = new Size(250, 26);
            this.txtSearch.Font = new Font("Arial", 9F);

            // ── BUTTONS ───────────────────────────
            this.btnAdd.Text = "➕ Add Invoice";
            this.btnAdd.Location = new Point(20, 205);
            this.btnAdd.Size = new Size(120, 35);
            this.btnAdd.BackColor = Color.FromArgb(0, 150, 0);
            this.btnAdd.ForeColor = Color.White;
            this.btnAdd.FlatStyle = FlatStyle.Flat;
            this.btnAdd.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.Click += btnAdd_Click;

            this.btnUpdate.Text = "✏️ Update";
            this.btnUpdate.Location = new Point(150, 205);
            this.btnUpdate.Size = new Size(110, 35);
            this.btnUpdate.BackColor = Color.FromArgb(0, 102, 204);
            this.btnUpdate.ForeColor = Color.White;
            this.btnUpdate.FlatStyle = FlatStyle.Flat;
            this.btnUpdate.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.Click += btnUpdate_Click;

            this.btnDelete.Text = "🗑️ Delete";
            this.btnDelete.Location = new Point(270, 205);
            this.btnDelete.Size = new Size(110, 35);
            this.btnDelete.BackColor = Color.FromArgb(200, 0, 0);
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Click += btnDelete_Click;

            this.btnClear.Text = "🔄 Clear";
            this.btnClear.Location = new Point(390, 205);
            this.btnClear.Size = new Size(110, 35);
            this.btnClear.BackColor = Color.FromArgb(100, 100, 100);
            this.btnClear.ForeColor = Color.White;
            this.btnClear.FlatStyle = FlatStyle.Flat;
            this.btnClear.Font = new Font("Arial", 9F, FontStyle.Bold);
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.Click += btnClear_Click;

            // ── DATAGRIDVIEW ──────────────────────
            this.dgvBilling.Location = new Point(20, 295);
            this.dgvBilling.Size = new Size(1100, 400);
            this.dgvBilling.Anchor = AnchorStyles.Top
                                                | AnchorStyles.Bottom
                                                | AnchorStyles.Left
                                                | AnchorStyles.Right;
            this.dgvBilling.ReadOnly = true;
            this.dgvBilling.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvBilling.MultiSelect = false;
            this.dgvBilling.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBilling.BackgroundColor = Color.White;
            this.dgvBilling.BorderStyle = BorderStyle.None;
            this.dgvBilling.RowHeadersVisible = false;
            this.dgvBilling.AllowUserToAddRows = false;
            this.dgvBilling.Font = new Font("Arial", 9F);
            this.dgvBilling.CellClick += dgvBilling_CellClick;

            // ── USER CONTROL ──────────────────────
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
            this.Name = "UC_Billing";
            this.Size = new Size(1200, 800);

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblShipment);
            this.Controls.Add(this.lblInvoiceDate);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblTax);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.cmbCustomer);
            this.Controls.Add(this.cmbShipment);
            this.Controls.Add(this.cmbStatus);
            this.Controls.Add(this.dtpInvoiceDate);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.txtTax);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.dgvBilling);

            ((System.ComponentModel.ISupportInitialize)this.dgvBilling).EndInit();
            this.ResumeLayout(false);
        }

        private Label lblTitle, lblCustomer, lblShipment;
        private Label lblInvoiceDate, lblAmount, lblTax;
        private Label lblTotal, lblStatus, lblSearch;
        private ComboBox cmbCustomer, cmbShipment, cmbStatus;
        private DateTimePicker dtpInvoiceDate;
        private TextBox txtAmount, txtTax, txtTotal, txtSearch;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;
        private DataGridView dgvBilling;
    }
}