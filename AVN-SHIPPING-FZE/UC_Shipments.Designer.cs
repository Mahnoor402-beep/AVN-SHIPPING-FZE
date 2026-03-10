namespace AVN_SHIPPING_FZE
{
    partial class UC_Shipments
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
            lblTitle = new Label();
            lblCustomer = new Label();
            lblOrigin = new Label();
            lblDestination = new Label();
            lblShipDate = new Label();
            lblWeight = new Label();
            lblStatus = new Label();
            lblDescription = new Label();
            lblSearch = new Label();
            cmbCustomer = new ComboBox();
            cmbStatus = new ComboBox();
            txtOrigin = new TextBox();
            txtDestination = new TextBox();
            txtShipDate = new TextBox();
            txtWeight = new TextBox();
            txtDescription = new TextBox();
            txtSearch = new TextBox();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dgvShipments = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvShipments).BeginInit();
            SuspendLayout();

            // ── TITLE ─────────────────────────────
            lblTitle.Text = "📦 SHIPMENTS";
            lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(20, 15);
            lblTitle.AutoSize = true;

            // ── LABELS ────────────────────────────
            lblCustomer.Text = "Customer *";
            lblCustomer.Location = new Point(20, 70);
            lblCustomer.AutoSize = true;

            lblOrigin.Text = "Origin *";
            lblOrigin.Location = new Point(20, 110);
            lblOrigin.AutoSize = true;

            lblDestination.Text = "Destination *";
            lblDestination.Location = new Point(20, 150);
            lblDestination.AutoSize = true;

            lblShipDate.Text = "Ship Date";
            lblShipDate.Location = new Point(20, 190);
            lblShipDate.AutoSize = true;

            lblWeight.Text = "Weight (kg)";
            lblWeight.Location = new Point(20, 230);
            lblWeight.AutoSize = true;

            lblStatus.Text = "Status";
            lblStatus.Location = new Point(20, 270);
            lblStatus.AutoSize = true;

            lblDescription.Text = "Description";
            lblDescription.Location = new Point(20, 310);
            lblDescription.AutoSize = true;

            lblSearch.Text = "🔍 Search:";
            lblSearch.Location = new Point(20, 430);
            lblSearch.AutoSize = true;

            // ── CUSTOMER DROPDOWN ─────────────────
            cmbCustomer.Location = new Point(160, 67);
            cmbCustomer.Size = new Size(250, 25);
            cmbCustomer.Name = "cmbCustomer";
            cmbCustomer.DropDownStyle = ComboBoxStyle.DropDownList;

            // ── STATUS DROPDOWN ───────────────────
            cmbStatus.Location = new Point(160, 267);
            cmbStatus.Size = new Size(250, 25);
            cmbStatus.Name = "cmbStatus";
            cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStatus.Items.AddRange(new string[]
            {
                "Pending", "In Transit", "Delivered", "Cancelled"
            });
            cmbStatus.SelectedIndex = 0;

            // ── TEXTBOXES ─────────────────────────
            txtOrigin.Location = new Point(160, 107);
            txtOrigin.Size = new Size(250, 25);
            txtOrigin.Name = "txtOrigin";

            txtDestination.Location = new Point(160, 147);
            txtDestination.Size = new Size(250, 25);
            txtDestination.Name = "txtDestination";

            txtShipDate.Location = new Point(160, 187);
            txtShipDate.Size = new Size(250, 25);
            txtShipDate.Name = "txtShipDate";
            txtShipDate.Text = DateTime.Today.ToString("yyyy-MM-dd");

            txtWeight.Location = new Point(160, 227);
            txtWeight.Size = new Size(250, 25);
            txtWeight.Name = "txtWeight";

            txtDescription.Location = new Point(160, 307);
            txtDescription.Size = new Size(250, 25);
            txtDescription.Name = "txtDescription";

            txtSearch.Location = new Point(160, 427);
            txtSearch.Size = new Size(250, 25);
            txtSearch.Name = "txtSearch";

            // ── BUTTONS ───────────────────────────
            btnAdd.Text = "➕ Add";
            btnAdd.Location = new Point(20, 370);
            btnAdd.Size = new Size(100, 35);
            btnAdd.BackColor = Color.FromArgb(0, 150, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Name = "btnAdd";
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Text = "✏️ Update";
            btnUpdate.Location = new Point(130, 370);
            btnUpdate.Size = new Size(100, 35);
            btnUpdate.BackColor = Color.FromArgb(0, 102, 204);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Text = "🗑️ Delete";
            btnDelete.Location = new Point(240, 370);
            btnDelete.Size = new Size(100, 35);
            btnDelete.BackColor = Color.FromArgb(200, 0, 0);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Name = "btnDelete";
            btnDelete.Click += btnDelete_Click;

            btnClear.Text = "🔄 Clear";
            btnClear.Location = new Point(350, 370);
            btnClear.Size = new Size(100, 35);
            btnClear.BackColor = Color.FromArgb(100, 100, 100);
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Name = "btnClear";
            btnClear.Click += btnClear_Click;

            // ── DATAGRIDVIEW ──────────────────────
            dgvShipments.Location = new Point(20, 470);
            dgvShipments.Size = new Size(950, 280);
            dgvShipments.Name = "dgvShipments";
            dgvShipments.ReadOnly = true;
            dgvShipments.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvShipments.MultiSelect = false;
            dgvShipments.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvShipments.BackgroundColor = Color.White;
            dgvShipments.BorderStyle = BorderStyle.None;
            dgvShipments.RowHeadersVisible = false;
            dgvShipments.AllowUserToAddRows = false;
            dgvShipments.CellClick += dgvShipments_CellClick;

            // ── USER CONTROL ──────────────────────
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 240, 240);
            Size = new Size(1000, 780);
            Name = "UC_Shipments";

            Controls.Add(lblTitle);
            Controls.Add(lblCustomer);
            Controls.Add(lblOrigin);
            Controls.Add(lblDestination);
            Controls.Add(lblShipDate);
            Controls.Add(lblWeight);
            Controls.Add(lblStatus);
            Controls.Add(lblDescription);
            Controls.Add(lblSearch);
            Controls.Add(cmbCustomer);
            Controls.Add(cmbStatus);
            Controls.Add(txtOrigin);
            Controls.Add(txtDestination);
            Controls.Add(txtShipDate);
            Controls.Add(txtWeight);
            Controls.Add(txtDescription);
            Controls.Add(txtSearch);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(dgvShipments);

            ((System.ComponentModel.ISupportInitialize)dgvShipments).EndInit();
            ResumeLayout(false);
        }

        private Label lblTitle;
        private Label lblCustomer;
        private Label lblOrigin;
        private Label lblDestination;
        private Label lblShipDate;
        private Label lblWeight;
        private Label lblStatus;
        private Label lblDescription;
        private Label lblSearch;
        private ComboBox cmbCustomer;
        private ComboBox cmbStatus;
        private TextBox txtOrigin;
        private TextBox txtDestination;
        private TextBox txtShipDate;
        private TextBox txtWeight;
        private TextBox txtDescription;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dgvShipments;
    }
}