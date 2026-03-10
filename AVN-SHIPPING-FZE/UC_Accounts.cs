using ClosedXML.Excel;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Drawing.Printing;

namespace AVN_SHIPPING_FZE
{
    public partial class UC_Accounts : UserControl
    {
        // ── INVOICE TAB CONTROLS ──────────────────
        private Panel pnlInvoiceForm, pnlItemsArea;
        private Label lblInvTitle;
        private Label lblCust, lblJobNo, lblJobDate, lblJobType;
        private Label lblShipType, lblExRate, lblAttention, lblTRN;
        private Label lblInvDate, lblDueDate, lblStatus, lblSearch;
        private ComboBox cmbCustomer, cmbStatus;
        private TextBox txtJobNo;
        private ComboBox cmbJobType, cmbShipType;
        private TextBox txtExRate, txtAttention, txtTRN;
        private TextBox txtSearch, txtDueDate;
        private DateTimePicker dtpInvDate, dtpJobDate;
        private Button btnNewInvoice, btnSaveInvoice, btnDeleteInvoice;
        private Button btnPDF, btnExcel, btnPrint, btnClearInv;
        private DataGridView dgvInvoices, dgvItems;
        private Button btnAddItem, btnRemoveItem;
        private Label lblSubTotal, lblVAT, lblRoundOff, lblTotal;
        private TextBox txtSubTotal, txtVAT, txtRoundOff, txtTotal, txtAmtWords;

        // ── EXPENSE TAB CONTROLS ──────────────────
        private Label lblExpTitle;
        private Label lblExpDate, lblExpCat, lblExpDesc;
        private Label lblExpAmt, lblExpPay, lblExpRef, lblExpSearch;
        private DateTimePicker dtpExpDate;
        private ComboBox cmbExpCategory, cmbExpPayment;
        private TextBox txtExpDesc, txtExpAmt, txtExpRef;
        private TextBox txtExpNotes, txtExpSearch;
        private Button btnAddExp, btnUpdateExp, btnDeleteExp, btnClearExp;
        private DataGridView dgvExpenses;

        // ── P&L TAB CONTROLS ─────────────────────
        private Label lblPLTitle;
        private DateTimePicker dtpPLFrom, dtpPLTo;
        private Button btnGeneratePL;
        private DataGridView dgvPL;
        private Label lblTotalRevenue, lblTotalExpense, lblNetProfit;

        private int selectedInvoiceID = -1;
        private int selectedExpenseID = -1;

        public UC_Accounts()
        {
            InitializeComponent();
            BuildInvoiceTab();
            BuildExpenseTab();
            BuildPLTab();
            LoadCustomers();
            LoadInvoices();
            LoadExpenses();
        }

        // ══════════════════════════════════════════
        // BUILD INVOICE TAB
        // ══════════════════════════════════════════
        // ══════════════════════════════════════════
        // BUILD INVOICE TAB
        // ══════════════════════════════════════════
        private void BuildInvoiceTab()
        {
            var p = tabInvoices;
            p.AutoScroll = true; // ✅ ENABLES SCROLLING

            lblInvTitle = new Label
            {
                Text = "🧾  INVOICE MANAGEMENT",
                Font = new Font("Arial", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                Location = new Point(10, 10),
                AutoSize = true
            };
            p.Controls.Add(lblInvTitle);

            // ── Form Panel ──
            pnlInvoiceForm = new Panel
            {
                Location = new Point(10, 45),
                Size = new Size(1280, 310),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(245, 248, 255)
            };
            p.Controls.Add(pnlInvoiceForm);

            // ── Helper lambdas ──
            void PL(Label l, string t, int x, int y)
            {
                l.Text = t; l.Location = new Point(x, y);
                l.AutoSize = true;
                l.Font = new Font("Arial", 9F, FontStyle.Bold);
                l.ForeColor = Color.FromArgb(40, 40, 80);
                pnlInvoiceForm.Controls.Add(l);
            }
            void PT(TextBox tb, int x, int y, int w = 160)
            {
                tb.Location = new Point(x, y);
                tb.Size = new Size(w, 26);
                tb.Font = new Font("Arial", 9F);
                tb.BorderStyle = BorderStyle.FixedSingle;
                pnlInvoiceForm.Controls.Add(tb);
            }
            void PC(ComboBox cb, int x, int y, int w = 160)
            {
                cb.Location = new Point(x, y);
                cb.Size = new Size(w, 26);
                cb.Font = new Font("Arial", 9F);
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                pnlInvoiceForm.Controls.Add(cb);
            }

            // ── ROW 1: Customer | Status | Search ──
            lblCust = new Label();
            PL(lblCust, "Customer *", 10, 18);
            cmbCustomer = new ComboBox();
            PC(cmbCustomer, 100, 15, 220);

            lblStatus = new Label();
            PL(lblStatus, "Status", 345, 18);
            cmbStatus = new ComboBox();
            cmbStatus.Items.AddRange(new string[] { "Unpaid", "Paid", "Partial", "Overdue" });
            cmbStatus.SelectedIndex = 0;
            PC(cmbStatus, 400, 15, 140);

            lblSearch = new Label();
            PL(lblSearch, "🔍 Search:", 565, 18);
            txtSearch = new TextBox();
            PT(txtSearch, 650, 15, 300);
            txtSearch.TextChanged += (s, e) => LoadInvoices(txtSearch.Text);

            // ── ROW 2: Job No | Job Date | Job Type | Ship Type ──
            lblJobNo = new Label();
            PL(lblJobNo, "Job No", 10, 60);
            txtJobNo = new TextBox();
            PT(txtJobNo, 75, 57, 130);

            lblJobDate = new Label();
            PL(lblJobDate, "Job Date", 225, 60);
            dtpJobDate = new DateTimePicker
            {
                Location = new Point(300, 57),
                Size = new Size(150, 26),
                Font = new Font("Arial", 9F),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };
            pnlInvoiceForm.Controls.Add(dtpJobDate);

            lblJobType = new Label();
            PL(lblJobType, "Job Type", 470, 60);
            cmbJobType = new ComboBox();
            cmbJobType.Items.AddRange(new string[] { "Import", "Export", "Local", "Transit", "Courier" });
            cmbJobType.SelectedIndex = 0;
            PC(cmbJobType, 545, 57, 160);

            lblShipType = new Label();
            PL(lblShipType, "Ship Type", 725, 60);
            cmbShipType = new ComboBox();
            cmbShipType.Items.AddRange(new string[] { "Sea FCL", "Sea LCL", "Air", "Road", "Courier" });
            cmbShipType.SelectedIndex = 0;
            PC(cmbShipType, 805, 57, 160);

            // ── ROW 3: Invoice Date | Due Date | Ex Rate | Attention ──
            lblInvDate = new Label();
            PL(lblInvDate, "Invoice Date", 10, 105);
            dtpInvDate = new DateTimePicker
            {
                Location = new Point(105, 102),
                Size = new Size(150, 26),
                Font = new Font("Arial", 9F),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };
            pnlInvoiceForm.Controls.Add(dtpInvDate);

            lblDueDate = new Label();
            PL(lblDueDate, "Due Date", 275, 105);
            txtDueDate = new TextBox();
            PT(txtDueDate, 345, 102, 120);

            lblExRate = new Label();
            PL(lblExRate, "Ex. Rate", 485, 105);
            txtExRate = new TextBox();
            PT(txtExRate, 550, 102, 100);
            txtExRate.Text = "3.685";

            lblAttention = new Label();
            PL(lblAttention, "Attention", 670, 105);
            txtAttention = new TextBox();
            PT(txtAttention, 745, 102, 220);

            // ── ROW 4: TRN ──
            lblTRN = new Label();
            PL(lblTRN, "TRN No.", 10, 150);
            txtTRN = new TextBox();
            PT(txtTRN, 80, 147, 180);

            // ── Action Buttons ──
            Button MakeBtn(string text, Color color, int x, EventHandler click)
            {
                var btn = new Button
                {
                    Text = text,
                    Location = new Point(x, 235),
                    Size = new Size(120, 34),
                    BackColor = color,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Arial", 9F, FontStyle.Bold)
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += click;
                pnlInvoiceForm.Controls.Add(btn);
                return btn;
            }

            btnNewInvoice = MakeBtn("➕ New", Color.FromArgb(0, 150, 0), 10, btnNew_Click);
            btnSaveInvoice = MakeBtn("💾 Save", Color.FromArgb(0, 102, 204), 140, btnSave_Click);
            btnDeleteInvoice = MakeBtn("🗑️ Delete", Color.FromArgb(200, 0, 0), 270, btnDeleteInv_Click);
            btnClearInv = MakeBtn("🔄 Clear", Color.FromArgb(120, 120, 120), 400, btnClearInv_Click);
            btnPDF = MakeBtn("📄 PDF", Color.FromArgb(180, 0, 0), 530, btnPDF_Click);
            btnExcel = MakeBtn("📊 Excel", Color.FromArgb(0, 130, 0), 660, btnExcel_Click);
            btnPrint = MakeBtn("🖨️ Print", Color.FromArgb(60, 60, 60), 790, btnPrint_Click);

            // ── Items Section ──
            var lblItems = new Label
            {
                Text = "INVOICE ITEMS",
                Font = new Font("Arial", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                Location = new Point(10, 370),
                AutoSize = true
            };
            p.Controls.Add(lblItems);

            btnAddItem = new Button
            {
                Text = "➕ Add Row",
                Location = new Point(190, 366),
                Size = new Size(110, 28),
                BackColor = Color.FromArgb(0, 150, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 8.5F, FontStyle.Bold)
            };
            btnAddItem.FlatAppearance.BorderSize = 0;
            btnAddItem.Click += btnAddItem_Click;
            p.Controls.Add(btnAddItem);

            btnRemoveItem = new Button
            {
                Text = "➖ Remove Row",
                Location = new Point(310, 366),
                Size = new Size(120, 28),
                BackColor = Color.FromArgb(200, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 8.5F, FontStyle.Bold)
            };
            btnRemoveItem.FlatAppearance.BorderSize = 0;
            btnRemoveItem.Click += btnRemoveItem_Click;
            p.Controls.Add(btnRemoveItem);

            dgvItems = new DataGridView
            {
                Location = new Point(10, 400),
                Size = new Size(1280, 220),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                Font = new Font("Arial", 9F),
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "SrNo", HeaderText = "Sr.", Width = 40, FillWeight = 4 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Description", HeaderText = "Description", Width = 400, FillWeight = 50 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Currency", HeaderText = "Currency", Width = 80, FillWeight = 8 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "Qty", HeaderText = "Qty", Width = 60, FillWeight = 6 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "UnitPrice", HeaderText = "Unit Price", Width = 120, FillWeight = 12 });
            dgvItems.Columns.Add(new DataGridViewTextBoxColumn { Name = "TotalAmt", HeaderText = "Total (AED)", Width = 120, FillWeight = 12 });
            dgvItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
            dgvItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvItems.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold);
            dgvItems.EnableHeadersVisualStyles = false;
            dgvItems.CellEndEdit += (s, e) => RecalcTotals();
            p.Controls.Add(dgvItems);

            // ── Totals Panel ──
            int ty = 635;

            // Totals on right
            lblSubTotal = new Label { Text = "Sub Total:", Location = new Point(920, ty), AutoSize = true, Font = new Font("Arial", 9F, FontStyle.Bold) };
            txtSubTotal = new TextBox { Location = new Point(1020, ty - 3), Size = new Size(130, 26), ReadOnly = true, Font = new Font("Arial", 9F), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.FromArgb(240, 240, 240) };
            lblVAT = new Label { Text = "VAT (5%):", Location = new Point(920, ty + 35), AutoSize = true, Font = new Font("Arial", 9F, FontStyle.Bold) };
            txtVAT = new TextBox { Location = new Point(1020, ty + 32), Size = new Size(130, 26), ReadOnly = true, Font = new Font("Arial", 9F), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.FromArgb(240, 240, 240) };
            lblRoundOff = new Label { Text = "Round Off:", Location = new Point(920, ty + 70), AutoSize = true, Font = new Font("Arial", 9F, FontStyle.Bold) };
            txtRoundOff = new TextBox { Location = new Point(1020, ty + 67), Size = new Size(130, 26), Font = new Font("Arial", 9F), BorderStyle = BorderStyle.FixedSingle, Text = "0" };
            txtRoundOff.TextChanged += (s, e) => RecalcTotals();
            lblTotal = new Label { Text = "TOTAL (AED):", Location = new Point(905, ty + 108), AutoSize = true, Font = new Font("Arial", 11F, FontStyle.Bold), ForeColor = Color.FromArgb(0, 51, 102) };
            txtTotal = new TextBox { Location = new Point(1020, ty + 105), Size = new Size(130, 30), ReadOnly = true, Font = new Font("Arial", 10F, FontStyle.Bold), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.FromArgb(230, 245, 255) };

            // Amount in words on left
            var lblWords = new Label { Text = "Amount in Words:", Location = new Point(10, ty + 110), AutoSize = true, Font = new Font("Arial", 9F, FontStyle.Bold) };
            txtAmtWords = new TextBox { Location = new Point(165, ty + 107), Size = new Size(720, 30), ReadOnly = true, Font = new Font("Arial", 9F), BorderStyle = BorderStyle.FixedSingle, BackColor = Color.FromArgb(240, 240, 240) };

            p.Controls.AddRange(new Control[]
            {
                lblSubTotal, txtSubTotal,
                lblVAT,      txtVAT,
                lblRoundOff, txtRoundOff,
                lblTotal,    txtTotal,
                lblWords,    txtAmtWords
            });

            // ── Invoices List Grid ──
            var lblList = new Label
            {
                Text = "SAVED INVOICES",
                Font = new Font("Arial", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                Location = new Point(10, ty + 150),
                AutoSize = true
            };
            p.Controls.Add(lblList);

            dgvInvoices = new DataGridView
            {
                Location = new Point(10, ty + 175),
                Size = new Size(1280, 280),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                Font = new Font("Arial", 9F)
            };
            dgvInvoices.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
            dgvInvoices.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInvoices.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold);
            dgvInvoices.EnableHeadersVisualStyles = false;
            dgvInvoices.CellClick += dgvInvoices_CellClick;
            p.Controls.Add(dgvInvoices);
        }

        // ══════════════════════════════════════════
        // BUILD EXPENSE TAB
        // ══════════════════════════════════════════
        private void BuildExpenseTab()
        {
            var p = tabExpenses;

            lblExpTitle = new Label
            {
                Text = "💸  EXPENSE TRACKING",
                Font = new Font("Arial", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                Location = new Point(10, 10),
                AutoSize = true
            };
            p.Controls.Add(lblExpTitle);

            void ML(Label l, string t, int x, int y)
            {
                l.Text = t; l.Location = new Point(x, y);
                l.AutoSize = true;
                l.Font = new Font("Arial", 9F, FontStyle.Bold);
                l.ForeColor = Color.FromArgb(40, 40, 80);
                p.Controls.Add(l);
            }
            void MT(TextBox tb, int x, int y, int w = 180)
            {
                tb.Location = new Point(x, y);
                tb.Size = new Size(w, 26);
                tb.Font = new Font("Arial", 9F);
                tb.BorderStyle = BorderStyle.FixedSingle;
                p.Controls.Add(tb);
            }

            lblExpDate = new Label(); dtpExpDate = new DateTimePicker();
            lblExpCat = new Label(); cmbExpCategory = new ComboBox();
            lblExpDesc = new Label(); txtExpDesc = new TextBox();
            lblExpAmt = new Label(); txtExpAmt = new TextBox();
            lblExpPay = new Label(); cmbExpPayment = new ComboBox();
            lblExpRef = new Label(); txtExpRef = new TextBox();
            txtExpNotes = new TextBox();
            lblExpSearch = new Label(); txtExpSearch = new TextBox();

            ML(lblExpDate, "Date *", 10, 60);
            dtpExpDate.Location = new Point(90, 57);
            dtpExpDate.Size = new Size(160, 26);
            dtpExpDate.Font = new Font("Arial", 9F);
            dtpExpDate.Format = DateTimePickerFormat.Short;
            dtpExpDate.Value = DateTime.Today;
            p.Controls.Add(dtpExpDate);

            ML(lblExpCat, "Category *", 270, 60);
            cmbExpCategory.Location = new Point(360, 57);
            cmbExpCategory.Size = new Size(180, 26);
            cmbExpCategory.Font = new Font("Arial", 9F);
            cmbExpCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExpCategory.Items.AddRange(new string[]
            { "Office", "Transport", "Port Charges", "Customs",
              "Salaries", "Utilities", "Marketing", "Other" });
            cmbExpCategory.SelectedIndex = 0;
            p.Controls.Add(cmbExpCategory);

            ML(lblExpDesc, "Description *", 10, 100);
            txtExpDesc = new TextBox(); MT(txtExpDesc, 120, 97, 400);

            ML(lblExpAmt, "Amount (AED) *", 10, 140);
            txtExpAmt = new TextBox(); MT(txtExpAmt, 130, 137, 150);

            ML(lblExpPay, "Payment Method", 300, 140);
            cmbExpPayment.Location = new Point(430, 137);
            cmbExpPayment.Size = new Size(160, 26);
            cmbExpPayment.Font = new Font("Arial", 9F);
            cmbExpPayment.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbExpPayment.Items.AddRange(new string[] { "Cash", "Bank Transfer", "Cheque", "Card" });
            cmbExpPayment.SelectedIndex = 0;
            p.Controls.Add(cmbExpPayment);

            ML(lblExpRef, "Reference", 10, 180);
            txtExpRef = new TextBox(); MT(txtExpRef, 100, 177, 180);

            var lblNotes = new Label
            {
                Text = "Notes",
                Location = new Point(300, 180),
                AutoSize = true,
                Font = new Font("Arial", 9F, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 40, 80)
            };
            p.Controls.Add(lblNotes);
            txtExpNotes = new TextBox(); MT(txtExpNotes, 360, 177, 300);

            Button MakeBtn(string text, Color color, int x, EventHandler click)
            {
                var btn = new Button
                {
                    Text = text,
                    Location = new Point(x, 220),
                    Size = new Size(110, 32),
                    BackColor = color,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Arial", 8.5F, FontStyle.Bold)
                };
                btn.FlatAppearance.BorderSize = 0;
                btn.Click += click;
                p.Controls.Add(btn);
                return btn;
            }

            btnAddExp = MakeBtn("➕ Add", Color.FromArgb(0, 150, 0), 10, btnAddExp_Click);
            btnUpdateExp = MakeBtn("✏️ Update", Color.FromArgb(0, 102, 204), 130, btnUpdateExp_Click);
            btnDeleteExp = MakeBtn("🗑️ Delete", Color.FromArgb(200, 0, 0), 250, btnDeleteExp_Click);
            btnClearExp = MakeBtn("🔄 Clear", Color.FromArgb(100, 100, 100), 370, btnClearExp_Click);

            ML(lblExpSearch, "🔍 Search:", 10, 268);
            txtExpSearch = new TextBox
            {
                Location = new Point(100, 265),
                Size = new Size(250, 26),
                Font = new Font("Arial", 9F)
            };
            txtExpSearch.TextChanged += (s, e) => LoadExpenses(txtExpSearch.Text);
            p.Controls.Add(txtExpSearch);

            dgvExpenses = new DataGridView
            {
                Location = new Point(10, 300),
                Size = new Size(1330, 380),
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                Font = new Font("Arial", 9F)
            };
            dgvExpenses.CellClick += dgvExpenses_CellClick;
            p.Controls.Add(dgvExpenses);
        }

        // ══════════════════════════════════════════
        // BUILD P&L TAB
        // ══════════════════════════════════════════
        private void BuildPLTab()
        {
            var p = tabPL;

            lblPLTitle = new Label
            {
                Text = "📊  PROFIT & LOSS REPORT",
                Font = new Font("Arial", 15F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 51, 102),
                Location = new Point(10, 10),
                AutoSize = true
            };
            p.Controls.Add(lblPLTitle);

            var lblFrom = new Label
            {
                Text = "From:",
                Location = new Point(10, 60),
                AutoSize = true,
                Font = new Font("Arial", 9F, FontStyle.Bold)
            };
            p.Controls.Add(lblFrom);
            dtpPLFrom = new DateTimePicker
            {
                Location = new Point(60, 57),
                Size = new Size(160, 26),
                Font = new Font("Arial", 9F),
                Format = DateTimePickerFormat.Short,
                Value = new DateTime(DateTime.Now.Year, 1, 1)
            };
            p.Controls.Add(dtpPLFrom);

            var lblTo = new Label
            {
                Text = "To:",
                Location = new Point(240, 60),
                AutoSize = true,
                Font = new Font("Arial", 9F, FontStyle.Bold)
            };
            p.Controls.Add(lblTo);
            dtpPLTo = new DateTimePicker
            {
                Location = new Point(270, 57),
                Size = new Size(160, 26),
                Font = new Font("Arial", 9F),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };
            p.Controls.Add(dtpPLTo);

            btnGeneratePL = new Button
            {
                Text = "📊 Generate Report",
                Location = new Point(450, 55),
                Size = new Size(160, 32),
                BackColor = Color.FromArgb(0, 51, 102),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 9F, FontStyle.Bold)
            };
            btnGeneratePL.FlatAppearance.BorderSize = 0;
            btnGeneratePL.Click += btnGeneratePL_Click;
            p.Controls.Add(btnGeneratePL);

            dgvPL = new DataGridView
            {
                Location = new Point(10, 105),
                Size = new Size(800, 400),
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                Font = new Font("Arial", 9F)
            };
            p.Controls.Add(dgvPL);

            lblTotalRevenue = new Label
            {
                Location = new Point(10, 520),
                AutoSize = true,
                Font = new Font("Arial", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 120, 0)
            };
            lblTotalExpense = new Label
            {
                Location = new Point(300, 520),
                AutoSize = true,
                Font = new Font("Arial", 11F, FontStyle.Bold),
                ForeColor = Color.FromArgb(180, 0, 0)
            };
            lblNetProfit = new Label
            {
                Location = new Point(600, 520),
                AutoSize = true,
                Font = new Font("Arial", 12F, FontStyle.Bold)
            };
            p.Controls.Add(lblTotalRevenue);
            p.Controls.Add(lblTotalExpense);
            p.Controls.Add(lblNetProfit);
        }

        // ══════════════════════════════════════════
        // INVOICE LOGIC
        // ══════════════════════════════════════════
        private void LoadCustomers()
        {
            try
            {
                cmbCustomer.Items.Clear();
                cmbCustomer.Items.Add(new CustomerItem(0, "-- Select Customer --"));
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "SELECT CustomerID, FullName FROM Customers ORDER BY FullName", con);
                using var r = cmd.ExecuteReader();
                while (r.Read())
                    cmbCustomer.Items.Add(new CustomerItem(
                        Convert.ToInt32(r["CustomerID"]),
                        r["FullName"].ToString()!));
                cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadInvoices(string search = "")
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(@"
                    SELECT i.InvoiceID, i.InvoiceNo, c.FullName as Customer,
                           i.InvoiceDate, i.JobNo, i.JobType,
                           i.TotalAmount, i.PaymentStatus
                    FROM Invoices i
                    LEFT JOIN Customers c ON i.CustomerID = c.CustomerID
                    WHERE i.InvoiceNo LIKE @s OR c.FullName LIKE @s
                    ORDER BY i.InvoiceID DESC", con);
                cmd.Parameters.AddWithValue("@s", $"%{search}%");

                var table = new DataTable();
                table.Columns.Add("InvoiceID");
                table.Columns.Add("Invoice No");
                table.Columns.Add("Customer");
                table.Columns.Add("Date");
                table.Columns.Add("Job No");
                table.Columns.Add("Job Type");
                table.Columns.Add("Total (AED)");
                table.Columns.Add("Status");

                using var r = cmd.ExecuteReader();
                while (r.Read())
                    table.Rows.Add(
                        r["InvoiceID"].ToString(),
                        r["InvoiceNo"].ToString(),
                        r["Customer"].ToString(),
                        r["InvoiceDate"].ToString(),
                        r["JobNo"].ToString(),
                        r["JobType"].ToString(),
                        Convert.ToDouble(r["TotalAmount"]).ToString("N2"),
                        r["PaymentStatus"].ToString());

                dgvInvoices.DataSource = null;
                dgvInvoices.DataSource = table;

                foreach (DataGridViewRow row in dgvInvoices.Rows)
                {
                    string st = row.Cells["Status"].Value?.ToString() ?? "";
                    row.DefaultCellStyle.BackColor = st switch
                    {
                        "Paid" => Color.FromArgb(220, 255, 220),
                        "Unpaid" => Color.FromArgb(255, 230, 230),
                        "Overdue" => Color.FromArgb(255, 200, 200),
                        "Partial" => Color.FromArgb(255, 255, 210),
                        _ => Color.White
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvInvoices_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectedInvoiceID = Convert.ToInt32(
                dgvInvoices.Rows[e.RowIndex].Cells["InvoiceID"].Value);
            LoadInvoiceToForm(selectedInvoiceID);
        }

        private void LoadInvoiceToForm(int id)
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "SELECT * FROM Invoices WHERE InvoiceID=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                using var r = cmd.ExecuteReader();
                if (!r.Read()) return;

                txtJobNo.Text = r["JobNo"]?.ToString() ?? "";
                int jIdx = cmbJobType.Items.IndexOf(r["JobType"]?.ToString() ?? "");
                if (jIdx >= 0) cmbJobType.SelectedIndex = jIdx;
                int sIdx = cmbShipType.Items.IndexOf(r["ShipmentType"]?.ToString() ?? "");
                if (sIdx >= 0) cmbShipType.SelectedIndex = sIdx;
                txtExRate.Text = r["ExRate"]?.ToString() ?? "3.685";
                txtAttention.Text = r["Attention"]?.ToString() ?? "";
                txtTRN.Text = r["TRN"]?.ToString() ?? "";
                txtDueDate.Text = r["DueDate"]?.ToString() ?? "N/A";

                if (DateTime.TryParse(r["InvoiceDate"]?.ToString(), out DateTime d))
                    dtpInvDate.Value = d;
                if (DateTime.TryParse(r["JobDate"]?.ToString(), out DateTime jd))
                    dtpJobDate.Value = jd;

                int custID = Convert.ToInt32(r["CustomerID"]);
                foreach (CustomerItem item in cmbCustomer.Items)
                    if (item.ID == custID) { cmbCustomer.SelectedItem = item; break; }

                int idx = cmbStatus.Items.IndexOf(r["PaymentStatus"]?.ToString() ?? "Unpaid");
                if (idx >= 0) cmbStatus.SelectedIndex = idx;

                dgvItems.Rows.Clear();
                using var cmd2 = new SqliteCommand(
                    "SELECT * FROM InvoiceItems WHERE InvoiceID=@id ORDER BY SrNo", con);
                cmd2.Parameters.AddWithValue("@id", id);
                using var r2 = cmd2.ExecuteReader();
                while (r2.Read())
                    dgvItems.Rows.Add(
                        r2["SrNo"].ToString(),
                        r2["Description"].ToString(),
                        r2["Currency"].ToString(),
                        r2["Qty"].ToString(),
                        r2["UnitPrice"].ToString(),
                        r2["TotalAmount"].ToString());

                RecalcTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice: " + ex.Message);
            }
        }

        private void RecalcTotals()
        {
            try
            {
                double subTotal = 0;
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (double.TryParse(row.Cells["Qty"].Value?.ToString(), out double qty) &&
                        double.TryParse(row.Cells["UnitPrice"].Value?.ToString(), out double up))
                    {
                        double rowTotal = qty * up;
                        row.Cells["TotalAmt"].Value = rowTotal.ToString("N2");
                        subTotal += rowTotal;
                    }
                    else if (double.TryParse(row.Cells["TotalAmt"].Value?.ToString(), out double ta))
                    {
                        subTotal += ta;
                    }
                }

                double vat = Math.Round(subTotal * 0.05, 2);
                double roundOff = double.TryParse(txtRoundOff.Text, out double ro) ? ro : 0;
                double total = subTotal + vat + roundOff;

                txtSubTotal.Text = subTotal.ToString("N2");
                txtVAT.Text = vat.ToString("N2");
                txtTotal.Text = total.ToString("N2");
                txtAmtWords.Text = NumberToWords.Convert(total);
            }
            catch { }
        }

        private string GenerateInvoiceNo()
        {
            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand("SELECT COUNT(*) FROM Invoices", con);
            int count = Convert.ToInt32(cmd.ExecuteScalar()) + 1;
            return $"AVN-{count:D2}/{DateTime.Now.Year}";
        }

        private void btnNew_Click(object sender, EventArgs e)
            => ClearInvoiceForm();

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a customer!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cust = (CustomerItem)cmbCustomer.SelectedItem!;
            RecalcTotals();

            double.TryParse(txtSubTotal.Text.Replace(",", ""), out double sub);
            double.TryParse(txtVAT.Text.Replace(",", ""), out double vat);
            double.TryParse(txtRoundOff.Text, out double ro);
            double.TryParse(txtTotal.Text.Replace(",", ""), out double total);

            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();

                if (selectedInvoiceID == -1)
                {
                    using var cmd = new SqliteCommand(@"
                        INSERT INTO Invoices
                        (InvoiceNo, CustomerID, JobNo, JobDate, JobType,
                         ShipmentType, ExRate, Attention, TRN,
                         InvoiceDate, DueDate, SubTotal, VAT, RoundOff,
                         TotalAmount, AmountInWords, PaymentStatus)
                        VALUES
                        (@no, @cid, @jno, @jdate, @jtype,
                         @stype, @exrate, @att, @trn,
                         @idate, @ddate, @sub, @vat, @ro,
                         @total, @words, @status)", con);

                    cmd.Parameters.AddWithValue("@no", GenerateInvoiceNo());
                    cmd.Parameters.AddWithValue("@cid", cust.ID);
                    cmd.Parameters.AddWithValue("@jno", txtJobNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@jdate", dtpJobDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@jtype", cmbJobType?.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@stype", cmbShipType?.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@exrate", txtExRate.Text.Trim());
                    cmd.Parameters.AddWithValue("@att", txtAttention.Text.Trim());
                    cmd.Parameters.AddWithValue("@trn", txtTRN.Text.Trim());
                    cmd.Parameters.AddWithValue("@idate", dtpInvDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ddate", txtDueDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@sub", sub);
                    cmd.Parameters.AddWithValue("@vat", vat);
                    cmd.Parameters.AddWithValue("@ro", ro);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@words", txtAmtWords.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem!.ToString());
                    cmd.ExecuteNonQuery();

                    using var getID = new SqliteCommand("SELECT last_insert_rowid()", con);
                    selectedInvoiceID = Convert.ToInt32(getID.ExecuteScalar());
                }
                else
                {
                    using var cmd = new SqliteCommand(@"
                        UPDATE Invoices SET
                        CustomerID=@cid, JobNo=@jno, JobDate=@jdate,
                        JobType=@jtype, ShipmentType=@stype, ExRate=@exrate,
                        Attention=@att, TRN=@trn, InvoiceDate=@idate,
                        DueDate=@ddate, SubTotal=@sub, VAT=@vat,
                        RoundOff=@ro, TotalAmount=@total,
                        AmountInWords=@words, PaymentStatus=@status
                        WHERE InvoiceID=@id", con);

                    cmd.Parameters.AddWithValue("@cid", cust.ID);
                    cmd.Parameters.AddWithValue("@jno", txtJobNo.Text.Trim());
                    cmd.Parameters.AddWithValue("@jdate", dtpJobDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@jtype", cmbJobType?.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@stype", cmbShipType?.SelectedItem?.ToString() ?? "");
                    cmd.Parameters.AddWithValue("@exrate", txtExRate.Text.Trim());
                    cmd.Parameters.AddWithValue("@att", txtAttention.Text.Trim());
                    cmd.Parameters.AddWithValue("@trn", txtTRN.Text.Trim());
                    cmd.Parameters.AddWithValue("@idate", dtpInvDate.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@ddate", txtDueDate.Text.Trim());
                    cmd.Parameters.AddWithValue("@sub", sub);
                    cmd.Parameters.AddWithValue("@vat", vat);
                    cmd.Parameters.AddWithValue("@ro", ro);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@words", txtAmtWords.Text);
                    cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem!.ToString());
                    cmd.Parameters.AddWithValue("@id", selectedInvoiceID);
                    cmd.ExecuteNonQuery();

                    using var del = new SqliteCommand(
                        "DELETE FROM InvoiceItems WHERE InvoiceID=@id", con);
                    del.Parameters.AddWithValue("@id", selectedInvoiceID);
                    del.ExecuteNonQuery();
                }

                int sr = 1;
                foreach (DataGridViewRow row in dgvItems.Rows)
                {
                    if (row.IsNewRow) continue;
                    string desc = row.Cells["Description"].Value?.ToString() ?? "";
                    if (string.IsNullOrWhiteSpace(desc)) continue;

                    using var icmd = new SqliteCommand(@"
                        INSERT INTO InvoiceItems
                        (InvoiceID, SrNo, Description, Currency, Qty, UnitPrice, TotalAmount)
                        VALUES (@iid, @sr, @desc, @cur, @qty, @up, @ta)", con);
                    icmd.Parameters.AddWithValue("@iid", selectedInvoiceID);
                    icmd.Parameters.AddWithValue("@sr", sr++);
                    icmd.Parameters.AddWithValue("@desc", desc);
                    icmd.Parameters.AddWithValue("@cur", row.Cells["Currency"].Value?.ToString() ?? "AED");
                    icmd.Parameters.AddWithValue("@qty", row.Cells["Qty"].Value?.ToString() ?? "1");
                    icmd.Parameters.AddWithValue("@up", row.Cells["UnitPrice"].Value?.ToString() ?? "0");
                    icmd.Parameters.AddWithValue("@ta", row.Cells["TotalAmt"].Value?.ToString() ?? "0");
                    icmd.ExecuteNonQuery();
                }

                MessageBox.Show("Invoice saved! ✅", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadInvoices();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving: " + ex.Message);
            }
        }

        private void btnDeleteInv_Click(object sender, EventArgs e)
        {
            if (selectedInvoiceID == -1)
            {
                MessageBox.Show("Select an invoice first!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Delete this invoice?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "DELETE FROM Invoices WHERE InvoiceID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedInvoiceID);
                cmd.ExecuteNonQuery();
                using var cmd2 = new SqliteCommand(
                    "DELETE FROM InvoiceItems WHERE InvoiceID=@id", con);
                cmd2.Parameters.AddWithValue("@id", selectedInvoiceID);
                cmd2.ExecuteNonQuery();
                ClearInvoiceForm();
                LoadInvoices();
            }
        }

        private void btnClearInv_Click(object sender, EventArgs e)
            => ClearInvoiceForm();

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            int sr = dgvItems.Rows.Count;
            dgvItems.Rows.Add(sr, "", "AED", "1", "0", "0");
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvItems.CurrentRow != null && !dgvItems.CurrentRow.IsNewRow)
            {
                dgvItems.Rows.Remove(dgvItems.CurrentRow);
                RecalcTotals();
            }
        }

        private void btnPDF_Click(object sender, EventArgs e)
        {
            if (selectedInvoiceID == -1)
            {
                MessageBox.Show("Please save the invoice first, then click PDF!",
                    "Save First", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using var dlg = new SaveFileDialog
                {
                    Filter = "PDF Files|*.pdf",
                    FileName = $"Invoice_{selectedInvoiceID}.pdf"
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    InvoicePDF.Generate(selectedInvoiceID, dlg.FileName);
                    MessageBox.Show("PDF saved! ✅", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo
                        { FileName = dlg.FileName, UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PDF Error: " + ex.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            if (selectedInvoiceID == -1)
            {
                MessageBox.Show("Please save the invoice first!",
                    "Save First", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using var dlg = new SaveFileDialog
                {
                    Filter = "Excel Files|*.xlsx",
                    FileName = $"Invoice_{selectedInvoiceID}.xlsx"
                };
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ExportInvoiceExcel(selectedInvoiceID, dlg.FileName);
                    MessageBox.Show("Excel saved! ✅", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo
                        { FileName = dlg.FileName, UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Excel Error: " + ex.Message);
            }
        }

        private void ExportInvoiceExcel(int invoiceID, string path)
        {
            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Invoice");

            ws.Cell(1, 1).Value = "TAX INVOICE - AVN SHIPPING F.Z.E";
            ws.Cell(1, 1).Style.Font.Bold = true;
            ws.Cell(1, 1).Style.Font.FontSize = 16;

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(
                "SELECT i.*, c.FullName, c.Address, c.Phone, c.Email " +
                "FROM Invoices i LEFT JOIN Customers c " +
                "ON i.CustomerID=c.CustomerID WHERE i.InvoiceID=@id", con);
            cmd.Parameters.AddWithValue("@id", invoiceID);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return;

            ws.Cell(3, 1).Value = "Customer:"; ws.Cell(3, 1).Style.Font.Bold = true;
            ws.Cell(3, 2).Value = r["FullName"]?.ToString();
            ws.Cell(3, 4).Value = "Invoice No:"; ws.Cell(3, 4).Style.Font.Bold = true;
            ws.Cell(3, 5).Value = r["InvoiceNo"]?.ToString();
            ws.Cell(4, 1).Value = "Address:"; ws.Cell(4, 1).Style.Font.Bold = true;
            ws.Cell(4, 2).Value = r["Address"]?.ToString();
            ws.Cell(4, 4).Value = "Date:"; ws.Cell(4, 4).Style.Font.Bold = true;
            ws.Cell(4, 5).Value = r["InvoiceDate"]?.ToString();
            ws.Cell(5, 4).Value = "Job No:"; ws.Cell(5, 4).Style.Font.Bold = true;
            ws.Cell(5, 5).Value = r["JobNo"]?.ToString();
            ws.Cell(6, 4).Value = "Job Type:"; ws.Cell(6, 4).Style.Font.Bold = true;
            ws.Cell(6, 5).Value = r["JobType"]?.ToString();

            int row = 9;
            ws.Cell(row, 1).Value = "Sr.";
            ws.Cell(row, 2).Value = "Description";
            ws.Cell(row, 3).Value = "Currency";
            ws.Cell(row, 4).Value = "Qty";
            ws.Cell(row, 5).Value = "Unit Price";
            ws.Cell(row, 6).Value = "Total Amount";
            for (int c = 1; c <= 6; c++)
            {
                ws.Cell(row, c).Style.Font.Bold = true;
                ws.Cell(row, c).Style.Fill.BackgroundColor = XLColor.FromArgb(0, 51, 102);
                ws.Cell(row, c).Style.Font.FontColor = XLColor.White;
            }

            using var cmd2 = new SqliteCommand(
                "SELECT * FROM InvoiceItems WHERE InvoiceID=@id ORDER BY SrNo", con);
            cmd2.Parameters.AddWithValue("@id", invoiceID);
            using var r2 = cmd2.ExecuteReader();
            row++;
            while (r2.Read())
            {
                ws.Cell(row, 1).Value = r2["SrNo"]?.ToString();
                ws.Cell(row, 2).Value = r2["Description"]?.ToString();
                ws.Cell(row, 3).Value = r2["Currency"]?.ToString();
                ws.Cell(row, 4).Value = r2["Qty"]?.ToString();
                ws.Cell(row, 5).Value = Convert.ToDouble(r2["UnitPrice"]);
                ws.Cell(row, 6).Value = Convert.ToDouble(r2["TotalAmount"]);
                row++;
            }

            row++;
            ws.Cell(row, 5).Value = "Sub Total:"; ws.Cell(row, 5).Style.Font.Bold = true;
            ws.Cell(row, 6).Value = Convert.ToDouble(r["SubTotal"]); row++;
            ws.Cell(row, 5).Value = "VAT (5%):"; ws.Cell(row, 5).Style.Font.Bold = true;
            ws.Cell(row, 6).Value = Convert.ToDouble(r["VAT"]); row++;
            ws.Cell(row, 5).Value = "TOTAL (AED):"; ws.Cell(row, 5).Style.Font.Bold = true;
            ws.Cell(row, 6).Value = Convert.ToDouble(r["TotalAmount"]);
            ws.Cell(row, 6).Style.Font.Bold = true;

            row += 2;
            ws.Cell(row, 1).Value = "Bank Details:"; ws.Cell(row, 1).Style.Font.Bold = true;
            ws.Cell(row + 1, 1).Value = "A/c Holder: AVN SHIPPING-F.Z.E";
            ws.Cell(row + 2, 1).Value = "Bank: Wio Bank P.J.S.C";
            ws.Cell(row + 3, 1).Value = "IBAN: AE150860000009952484786";
            ws.Cell(row + 4, 1).Value = "Swift: WIOBAEADXXX";

            ws.Columns().AdjustToContents();
            wb.SaveAs(path);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (selectedInvoiceID == -1)
            {
                MessageBox.Show("Please save the invoice first!",
                    "Save First", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                string tmpPath = System.IO.Path.Combine(
                    System.IO.Path.GetTempPath(),
                    $"AVN_Invoice_{selectedInvoiceID}.pdf");
                InvoicePDF.Generate(selectedInvoiceID, tmpPath);
                System.Diagnostics.Process.Start(
                    new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = tmpPath,
                        Verb = "print",
                        UseShellExecute = true
                    });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Print Error: " + ex.Message);
            }
        }

        private void ClearInvoiceForm()
        {
            selectedInvoiceID = -1;
            cmbCustomer.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            txtJobNo.Text = "";
            cmbJobType.SelectedIndex = 0;
            cmbShipType.SelectedIndex = 0;
            txtExRate.Text = "3.685";
            txtAttention.Text = "";
            txtTRN.Text = "";
            txtDueDate.Text = "N/A";
            txtRoundOff.Text = "0";
            dtpInvDate.Value = DateTime.Today;
            dtpJobDate.Value = DateTime.Today;
            dgvItems.Rows.Clear();
            txtSubTotal.Text = "0.00";
            txtVAT.Text = "0.00";
            txtTotal.Text = "0.00";
            txtAmtWords.Text = "";
        }

        // ══════════════════════════════════════════
        // EXPENSE LOGIC
        // ══════════════════════════════════════════
        private void LoadExpenses(string search = "")
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(@"
                    SELECT ExpenseID, ExpenseDate, Category, Description,
                           Amount, PaymentMethod, Reference
                    FROM Expenses
                    WHERE Description LIKE @s OR Category LIKE @s
                    ORDER BY ExpenseDate DESC", con);
                cmd.Parameters.AddWithValue("@s", $"%{search}%");

                var table = new DataTable();
                table.Columns.Add("ExpenseID");
                table.Columns.Add("Date");
                table.Columns.Add("Category");
                table.Columns.Add("Description");
                table.Columns.Add("Amount (AED)");
                table.Columns.Add("Payment Method");
                table.Columns.Add("Reference");

                using var r = cmd.ExecuteReader();
                while (r.Read())
                    table.Rows.Add(
                        r["ExpenseID"].ToString(),
                        r["ExpenseDate"].ToString(),
                        r["Category"].ToString(),
                        r["Description"].ToString(),
                        Convert.ToDouble(r["Amount"]).ToString("N2"),
                        r["PaymentMethod"].ToString(),
                        r["Reference"].ToString());

                dgvExpenses.DataSource = null;
                dgvExpenses.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvExpenses_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvExpenses.Rows[e.RowIndex];
            selectedExpenseID = Convert.ToInt32(row.Cells["ExpenseID"].Value);

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(
                "SELECT * FROM Expenses WHERE ExpenseID=@id", con);
            cmd.Parameters.AddWithValue("@id", selectedExpenseID);
            using var r = cmd.ExecuteReader();
            if (!r.Read()) return;

            if (DateTime.TryParse(r["ExpenseDate"]?.ToString(), out DateTime d))
                dtpExpDate.Value = d;

            int idx = cmbExpCategory.Items.IndexOf(r["Category"]?.ToString() ?? "");
            if (idx >= 0) cmbExpCategory.SelectedIndex = idx;

            txtExpDesc.Text = r["Description"]?.ToString() ?? "";
            txtExpAmt.Text = r["Amount"]?.ToString() ?? "";
            txtExpRef.Text = r["Reference"]?.ToString() ?? "";
            txtExpNotes.Text = r["Notes"]?.ToString() ?? "";

            int pidx = cmbExpPayment.Items.IndexOf(r["PaymentMethod"]?.ToString() ?? "");
            if (pidx >= 0) cmbExpPayment.SelectedIndex = pidx;
        }

        private void btnAddExp_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtExpDesc.Text) ||
                string.IsNullOrWhiteSpace(txtExpAmt.Text))
            {
                MessageBox.Show("Description and Amount are required!",
                    "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(@"
                INSERT INTO Expenses
                (ExpenseDate, Category, Description, Amount, PaymentMethod, Reference, Notes)
                VALUES (@date, @cat, @desc, @amt, @pay, @ref, @notes)", con);
            cmd.Parameters.AddWithValue("@date", dtpExpDate.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@cat", cmbExpCategory.SelectedItem!.ToString());
            cmd.Parameters.AddWithValue("@desc", txtExpDesc.Text.Trim());
            cmd.Parameters.AddWithValue("@amt", txtExpAmt.Text.Trim());
            cmd.Parameters.AddWithValue("@pay", cmbExpPayment.SelectedItem!.ToString());
            cmd.Parameters.AddWithValue("@ref", txtExpRef.Text.Trim());
            cmd.Parameters.AddWithValue("@notes", txtExpNotes.Text.Trim());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Expense added! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearExpenseForm();
            LoadExpenses();
        }

        private void btnUpdateExp_Click(object sender, EventArgs e)
        {
            if (selectedExpenseID == -1)
            {
                MessageBox.Show("Select an expense first!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(@"
                UPDATE Expenses SET
                ExpenseDate=@date, Category=@cat, Description=@desc,
                Amount=@amt, PaymentMethod=@pay, Reference=@ref, Notes=@notes
                WHERE ExpenseID=@id", con);
            cmd.Parameters.AddWithValue("@date", dtpExpDate.Value.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@cat", cmbExpCategory.SelectedItem!.ToString());
            cmd.Parameters.AddWithValue("@desc", txtExpDesc.Text.Trim());
            cmd.Parameters.AddWithValue("@amt", txtExpAmt.Text.Trim());
            cmd.Parameters.AddWithValue("@pay", cmbExpPayment.SelectedItem!.ToString());
            cmd.Parameters.AddWithValue("@ref", txtExpRef.Text.Trim());
            cmd.Parameters.AddWithValue("@notes", txtExpNotes.Text.Trim());
            cmd.Parameters.AddWithValue("@id", selectedExpenseID);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Expense updated! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearExpenseForm();
            LoadExpenses();
        }

        private void btnDeleteExp_Click(object sender, EventArgs e)
        {
            if (selectedExpenseID == -1)
            {
                MessageBox.Show("Select an expense to delete!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Delete this expense?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "DELETE FROM Expenses WHERE ExpenseID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedExpenseID);
                cmd.ExecuteNonQuery();
                ClearExpenseForm();
                LoadExpenses();
            }
        }

        private void btnClearExp_Click(object sender, EventArgs e)
            => ClearExpenseForm();

        private void ClearExpenseForm()
        {
            selectedExpenseID = -1;
            dtpExpDate.Value = DateTime.Today;
            cmbExpCategory.SelectedIndex = 0;
            cmbExpPayment.SelectedIndex = 0;
            txtExpDesc.Text = "";
            txtExpAmt.Text = "";
            txtExpRef.Text = "";
            txtExpNotes.Text = "";
            txtExpSearch.Text = "";
        }

        // ══════════════════════════════════════════
        // P&L LOGIC
        // ══════════════════════════════════════════
        private void btnGeneratePL_Click(object sender, EventArgs e)
        {
            try
            {
                string from = dtpPLFrom.Value.ToString("yyyy-MM-dd");
                string to = dtpPLTo.Value.ToString("yyyy-MM-dd");

                using var con = DatabaseHelper.GetConnection();
                con.Open();

                var table = new DataTable();
                table.Columns.Add("Month");
                table.Columns.Add("Revenue (AED)");
                table.Columns.Add("Expenses (AED)");
                table.Columns.Add("Net Profit (AED)");

                using var cmd = new SqliteCommand(@"
                    SELECT strftime('%Y-%m', InvoiceDate) as Month,
                           SUM(CAST(TotalAmount as REAL)) as Revenue
                    FROM Invoices
                    WHERE InvoiceDate BETWEEN @from AND @to
                    AND PaymentStatus != 'Unpaid'
                    GROUP BY Month ORDER BY Month", con);
                cmd.Parameters.AddWithValue("@from", from);
                cmd.Parameters.AddWithValue("@to", to);

                var revenueByMonth = new Dictionary<string, double>();
                using var r = cmd.ExecuteReader();
                while (r.Read())
                    revenueByMonth[r["Month"].ToString()!] = Convert.ToDouble(r["Revenue"]);

                using var cmd2 = new SqliteCommand(@"
                    SELECT strftime('%Y-%m', ExpenseDate) as Month,
                           SUM(CAST(Amount as REAL)) as Expenses
                    FROM Expenses
                    WHERE ExpenseDate BETWEEN @from AND @to
                    GROUP BY Month ORDER BY Month", con);
                cmd2.Parameters.AddWithValue("@from", from);
                cmd2.Parameters.AddWithValue("@to", to);

                var expenseByMonth = new Dictionary<string, double>();
                using var r2 = cmd2.ExecuteReader();
                while (r2.Read())
                    expenseByMonth[r2["Month"].ToString()!] = Convert.ToDouble(r2["Expenses"]);

                var allMonths = revenueByMonth.Keys.Union(expenseByMonth.Keys).OrderBy(m => m);

                double totalRev = 0, totalExp = 0;
                foreach (var month in allMonths)
                {
                    double rev = revenueByMonth.ContainsKey(month) ? revenueByMonth[month] : 0;
                    double exp = expenseByMonth.ContainsKey(month) ? expenseByMonth[month] : 0;
                    double net = rev - exp;
                    totalRev += rev;
                    totalExp += exp;

                    var row = table.NewRow();
                    row["Month"] = month;
                    row["Revenue (AED)"] = rev.ToString("N2");
                    row["Expenses (AED)"] = exp.ToString("N2");
                    row["Net Profit (AED)"] = net.ToString("N2");
                    table.Rows.Add(row);
                }

                dgvPL.DataSource = null;
                dgvPL.DataSource = table;

                foreach (DataGridViewRow row in dgvPL.Rows)
                {
                    if (double.TryParse(
                        row.Cells["Net Profit (AED)"].Value?.ToString().Replace(",", ""),
                        out double net))
                    {
                        row.Cells["Net Profit (AED)"].Style.ForeColor =
                            net >= 0 ? Color.FromArgb(0, 120, 0) : Color.FromArgb(180, 0, 0);
                        row.Cells["Net Profit (AED)"].Style.Font =
                            new Font("Arial", 9F, FontStyle.Bold);
                    }
                }

                double netProfit = totalRev - totalExp;
                lblTotalRevenue.Text = $"Total Revenue: AED {totalRev:N2}";
                lblTotalExpense.Text = $"Total Expenses: AED {totalExp:N2}";
                lblNetProfit.Text = $"Net Profit: AED {netProfit:N2}";
                lblNetProfit.ForeColor = netProfit >= 0
                    ? Color.FromArgb(0, 120, 0)
                    : Color.FromArgb(180, 0, 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("P&L Error: " + ex.Message);
            }
        }

        private void tabExpenses_Click(object sender, EventArgs e) { }
    }
}