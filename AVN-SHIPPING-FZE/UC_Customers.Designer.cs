namespace AVN_SHIPPING_FZE
{
    partial class UC_Customers
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
            // ── CONTROLS ──────────────────────────────
            var lblTitle = new Label();
            var lblFullName = new Label();
            var lblCompany = new Label();
            var lblPhone = new Label();
            var lblEmail = new Label();
            var lblAddress = new Label();
            var lblTRN = new Label();
            var lblSearch = new Label();

            txtFullName = new TextBox();
            txtCompany = new TextBox();
            txtPhone = new TextBox();
            txtEmail = new TextBox();
            txtAddress = new TextBox();
            txtTRN = new TextBox();
            txtSearch = new TextBox();

            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();

            dgvCustomers = new DataGridView();

            this.SuspendLayout();

            // ── TITLE ─────────────────────────────────
            lblTitle.Text = "👤  CUSTOMERS";
            lblTitle.Font = new Font("Arial", 15F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 51, 102);
            lblTitle.Location = new Point(15, 12);
            lblTitle.AutoSize = true;

            // ── COLUMN 1 LABELS (x=15) ────────────────
            int lx1 = 15, ix1 = 130, lx2 = 520, ix2 = 650;
            int iw = 320, row1 = 55, rowH = 42;

            void ML(Label l, string t, int x, int y)
            {
                l.Text = t; l.Location = new Point(x, y + 4);
                l.AutoSize = true;
                l.Font = new Font("Arial", 9.5F, FontStyle.Bold);
                l.ForeColor = Color.FromArgb(40, 40, 80);
            }

            void MT(TextBox tb, int x, int y, int w = 320)
            {
                tb.Location = new Point(x, y);
                tb.Size = new Size(w, 28);
                tb.Font = new Font("Arial", 9.5F);
                tb.BorderStyle = BorderStyle.FixedSingle;
            }

            // ROW 1
            ML(lblFullName, "Full Name *", lx1, row1);
            MT(txtFullName, ix1, row1, iw);

            ML(lblCompany, "Company Name", lx2, row1);
            MT(txtCompany, ix2, row1, iw);

            // ROW 2
            ML(lblPhone, "Phone", lx1, row1 + rowH);
            MT(txtPhone, ix1, row1 + rowH, iw);

            ML(lblEmail, "Email", lx2, row1 + rowH);
            MT(txtEmail, ix2, row1 + rowH, iw);

            // ROW 3
            ML(lblAddress, "Address", lx1, row1 + rowH * 2);
            MT(txtAddress, ix1, row1 + rowH * 2, iw);

            ML(lblTRN, "TRN", lx2, row1 + rowH * 2);
            MT(txtTRN, ix2, row1 + rowH * 2, iw);

            // ── BUTTONS ───────────────────────────────
            int by = row1 + rowH * 3 + 8;

            void StyleBtn(Button b, string text, Color color, int x)
            {
                b.Text = text;
                b.Location = new Point(x, by);
                b.Size = new Size(110, 34);
                b.BackColor = color;
                b.ForeColor = Color.White;
                b.FlatStyle = FlatStyle.Flat;
                b.Font = new Font("Arial", 9F, FontStyle.Bold);
                b.FlatAppearance.BorderSize = 0;
            }

            StyleBtn(btnAdd, "➕ Add", Color.FromArgb(0, 150, 0), lx1);
            StyleBtn(btnUpdate, "✏️ Update", Color.FromArgb(0, 102, 204), lx1 + 120);
            StyleBtn(btnDelete, "🗑️ Delete", Color.FromArgb(200, 0, 0), lx1 + 240);
            StyleBtn(btnClear, "🔄 Clear", Color.FromArgb(100, 100, 100), lx1 + 360);

            // ── SEARCH ────────────────────────────────
            int sy = by + 50;
            lblSearch.Text = "🔍 Search:";
            lblSearch.Location = new Point(lx1, sy + 4);
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Arial", 9.5F, FontStyle.Bold);

            txtSearch.Location = new Point(lx1 + 90, sy);
            txtSearch.Size = new Size(300, 28);
            txtSearch.Font = new Font("Arial", 9.5F);

            // ── GRID ──────────────────────────────────
            int gy = sy + 40;
            dgvCustomers.Location = new Point(lx1, gy);
            dgvCustomers.Anchor = AnchorStyles.Top | AnchorStyles.Left
                                             | AnchorStyles.Right | AnchorStyles.Bottom;
            dgvCustomers.Size = new Size(1540, 420);
            dgvCustomers.ReadOnly = true;
            dgvCustomers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCustomers.MultiSelect = false;
            dgvCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCustomers.BackgroundColor = Color.White;
            dgvCustomers.BorderStyle = BorderStyle.None;
            dgvCustomers.RowHeadersVisible = false;
            dgvCustomers.AllowUserToAddRows = false;
            dgvCustomers.Font = new Font("Arial", 9.5F);
            dgvCustomers.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
            dgvCustomers.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCustomers.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9.5F, FontStyle.Bold);
            dgvCustomers.EnableHeadersVisualStyles = false;
            dgvCustomers.RowTemplate.Height = 28;
            dgvCustomers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);

            // ── WIRE EVENTS ───────────────────────────
            btnAdd.Click += btnAdd_Click;
            btnUpdate.Click += btnUpdate_Click;
            btnDelete.Click += btnDelete_Click;
            btnClear.Click += btnClear_Click;
            txtSearch.TextChanged += (s, e) => LoadCustomers(txtSearch.Text);
            dgvCustomers.CellClick += dgvCustomers_CellClick;

            // ── FORM PANEL ────────────────────────────
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.Dock = DockStyle.Fill;
            this.Name = "UC_Customers";

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblFullName);
            this.Controls.Add(lblCompany);
            this.Controls.Add(lblPhone);
            this.Controls.Add(lblEmail);
            this.Controls.Add(lblAddress);
            this.Controls.Add(lblTRN);
            this.Controls.Add(txtFullName);
            this.Controls.Add(txtCompany);
            this.Controls.Add(txtPhone);
            this.Controls.Add(txtEmail);
            this.Controls.Add(txtAddress);
            this.Controls.Add(txtTRN);
            this.Controls.Add(btnAdd);
            this.Controls.Add(btnUpdate);
            this.Controls.Add(btnDelete);
            this.Controls.Add(btnClear);
            this.Controls.Add(lblSearch);
            this.Controls.Add(txtSearch);
            this.Controls.Add(dgvCustomers);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private Label lblFullName;
        private Label lblCompany;
        private Label lblPhone;
        private Label lblEmail;
        private Label lblAddress;
        private Label lblTRN;
        private Label lblSearch;
        private TextBox txtFullName;
        private TextBox txtCompany;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private TextBox txtAddress;
        private TextBox txtTRN;
        private TextBox txtSearch;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private DataGridView dgvCustomers;
        private Label lblTitle;
    }
}