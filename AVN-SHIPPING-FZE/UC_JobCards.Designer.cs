namespace AVN_SHIPPING_FZE
{
    partial class UC_JobCards
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
            tabJobCard = new TabPage();        // ✅ must be created first
            tabReminders = new TabPage();
            // ✅ ADD THESE - they are missing!
            lblTitle = new Label();
            lblReminderTitle = new Label();
            lblSearch = new Label();

            // NOW set AutoScroll - after both are created
            tabJobCard.AutoScroll = true;
            tabJobCard.AutoScrollMinSize = new Size(1450, 800);
            tabReminders.AutoScroll = true;

            // Row 1
            lblJobCardNo = new Label(); txtJobCardNo = new TextBox();
            lblJobCardType = new Label(); cmbJobCardType = new ComboBox();
            lblFCLLCL = new Label(); cmbFCLLCL = new ComboBox();
            lblStatus = new Label(); cmbStatus = new ComboBox();

            // Row 2
            lblDateCreated = new Label(); dtpDateCreated = new DateTimePicker();
            lblShipper = new Label(); txtShipper = new TextBox();
            lblConsignee = new Label(); txtConsignee = new TextBox();
            lblCustomer = new Label(); cmbCustomer = new ComboBox();

            // Row 3
            lblInvoiceNo = new Label(); txtInvoiceNo = new TextBox();
            lblNoOfPkgs = new Label(); txtNoOfPkgs = new TextBox();
            lblShippingBillNo = new Label(); txtShippingBillNo = new TextBox();
            lblWeightKG = new Label(); txtWeightKG = new TextBox();

            // Row 4
            lblPortOfLoading = new Label(); txtPortOfLoading = new TextBox();
            lblPortOfDischarge = new Label(); txtPortOfDischarge = new TextBox();
            lblShippingLine = new Label(); txtShippingLine = new TextBox();
            lblBookingNo = new Label(); txtBookingNo = new TextBox();

            // Row 5
            lblBookingRelDate = new Label(); dtpBookingRelDate = new DateTimePicker();
            lblPlanVessel = new Label(); txtPlanVessel = new TextBox();
            lblSOB = new Label(); dtpSOB = new DateTimePicker();
            lblETD = new Label(); dtpETD = new DateTimePicker();

            // Row 6
            lblETA = new Label(); dtpETA = new DateTimePicker();
            lblDraftReceived = new Label(); dtpDraftReceived = new DateTimePicker();
            lblInvoiceReceived = new Label(); dtpInvoiceReceived = new DateTimePicker();
            lblCHAName = new Label(); txtCHAName = new TextBox();

            // Row 7
            lblCargoCartingDt = new Label(); dtpCargoCartingDt = new DateTimePicker();
            lblStuffingDt = new Label(); dtpStuffingDt = new DateTimePicker();
            lblVGMSubmitDate = new Label(); dtpVGMSubmitDate = new DateTimePicker();
            lblShipHandoverDt = new Label(); dtpShipHandoverDt = new DateTimePicker();
            // Row 8
            lblCustomClearDate = new Label(); dtpCustomClearDate = new DateTimePicker();
            lblHumidity = new Label(); txtHumidity = new TextBox();
            lblNoContainers = new Label(); txtNoContrs = new TextBox();
            lblLineSeal = new Label(); txtLineSeal = new TextBox();

            // Row 9
            lblCustSeal = new Label(); txtCustSeal = new TextBox();
            lblBLRelDate = new Label(); dtpBLRelDate = new DateTimePicker();
            lblGateInPOL = new Label(); dtpGateInPOL = new DateTimePicker();
            lblHBLNo = new Label(); txtHBLNo = new TextBox();

            // Row 10
            lblMBLNo = new Label(); txtMBLNo = new TextBox();
            lblSISubmitDt = new Label(); dtpSISubmitDt = new DateTimePicker();
            lblDocDeliveryDt = new Label(); dtpDocDeliveryDt = new DateTimePicker();
            lblNotes = new Label(); txtNotes = new TextBox();

            // Buttons & Search
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnAddReminder = new Button();
            lblSearch = new Label();
            txtSearch = new TextBox();
            dgvJobCards = new DataGridView();
            dgvReminders = new DataGridView();

            ((System.ComponentModel.ISupportInitialize)dgvJobCards).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvReminders).BeginInit();
            SuspendLayout();

            int lw = 155;  // label width — wider so text fits
            int iw = 185;  // input width — wider boxes
            int rh = 38;   // row height
            int sy = 55;   // start Y

            int lx1 = 5, ix1 = 163;   // Col 1
            int lx2 = 358, ix2 = 516;   // Col 2
            int lx3 = 711, ix3 = 869;   // Col 3
            int lx4 = 1064, ix4 = 1222;  // Col 4

            // ══ TAB CONTROL ═══════════════════════
            tabControl.Dock = DockStyle.Fill;
            tabControl.Name = "tabControl";
            tabControl.Font = new Font("Arial", 10F, FontStyle.Bold);
            tabControl.Controls.Add(tabJobCard);
            tabControl.Controls.Add(tabReminders);

            tabJobCard.Text = "🧾  Job Cards";
            tabJobCard.BackColor = Color.FromArgb(245, 247, 250);
            tabJobCard.AutoScroll = true;  // ✅ ADD THIS
            tabJobCard.AutoScrollMinSize = new Size(1450, 800);  // ✅ forces scrollbars to appear


            tabReminders.Text = "🔔  Reminders";
            tabReminders.BackColor = Color.FromArgb(245, 247, 250);
            tabReminders.AutoScroll = true;  // ✅ ADD THIS

            // ══ HELPER FUNCTIONS ══════════════════
            void MakeLabel(Label l, string text, int x, int y)
            {
                l.Text = text;
                l.Location = new Point(x, y + 4);
                l.Size = new Size(lw, 22);
                l.Font = new Font("Arial", 9F, FontStyle.Bold);
                l.ForeColor = Color.FromArgb(40, 40, 80);
                l.TextAlign = ContentAlignment.MiddleRight;
            }

            void MakeTextBox(TextBox t, string name, int x, int y)
            {
                t.Name = name;
                t.Location = new Point(x, y);
                t.Size = new Size(iw, 26);
                t.Font = new Font("Arial", 9F);
                t.BackColor = Color.White;
                t.BorderStyle = BorderStyle.FixedSingle;
            }

            void MakeCombo(ComboBox c, string name, int x, int y)
            {
                c.Name = name;
                c.Location = new Point(x, y);
                c.Size = new Size(iw, 26);
                c.Font = new Font("Arial", 9F);
                c.DropDownStyle = ComboBoxStyle.DropDownList;
                c.BackColor = Color.White;
                c.FlatStyle = FlatStyle.Flat;
            }

            void MakeDTP(DateTimePicker d, string name, int x, int y)
            {
                d.Name = name;
                d.Location = new Point(x, y);
                d.Size = new Size(iw, 26);
                d.Font = new Font("Arial", 9F);
                d.Format = DateTimePickerFormat.Short;
                d.ShowCheckBox = true;
                d.Checked = false;
                d.CalendarFont = new Font("Arial", 9F);
            }

            // ══ TITLE ═════════════════════════════
            lblTitle.Text = "🧾  JOB CARD MANAGEMENT";
            lblTitle.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblTitle.Location = new Point(10, 10);
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = Color.FromArgb(0, 51, 102);

            // ══ ROW 1 — Job Card No, Type, FCL/LCL, Status
            int r1 = sy;
            MakeLabel(lblJobCardNo, "Job Card No.", lx1, r1);
            MakeTextBox(txtJobCardNo, "txtJobCardNo", ix1, r1);

            MakeLabel(lblJobCardType, "Job Card Type", lx2, r1);
            MakeCombo(cmbJobCardType, "cmbJobCardType", ix2, r1);
            cmbJobCardType.Items.AddRange(new string[] { "Export", "Import" });
            cmbJobCardType.SelectedIndex = 0;

            MakeLabel(lblFCLLCL, "FCL / LCL", lx3, r1);
            MakeCombo(cmbFCLLCL, "cmbFCLLCL", ix3, r1);
            cmbFCLLCL.Items.AddRange(new string[] { "FCL", "LCL", "FCL+LCL" });
            cmbFCLLCL.SelectedIndex = 0;

            MakeLabel(lblStatus, "Status", lx4, r1);
            MakeCombo(cmbStatus, "cmbStatus", ix4, r1);
            cmbStatus.Items.AddRange(new string[]
            { "Open","In Progress","Completed","Cancelled","On Hold" });
            cmbStatus.SelectedIndex = 0;

            // ══ ROW 2 — Date Created, Shipper, Consignee, Customer
            int r2 = r1 + rh;
            MakeLabel(lblDateCreated, "Date Created", lx1, r2);
            MakeDTP(dtpDateCreated, "dtpDateCreated", ix1, r2);
            dtpDateCreated.Value = DateTime.Today;
            dtpDateCreated.Checked = true;

            MakeLabel(lblShipper, "Shipper", lx2, r2);
            MakeTextBox(txtShipper, "txtShipper", ix2, r2);

            MakeLabel(lblConsignee, "Consignee", lx3, r2);
            MakeTextBox(txtConsignee, "txtConsignee", ix3, r2);

            MakeLabel(lblCustomer, "Customer", lx4, r2);
            MakeCombo(cmbCustomer, "cmbCustomer", ix4, r2);

            // ══ ROW 3 — Invoice, Pkgs, Shipping Bill, Weight
            int r3 = r2 + rh;
            MakeLabel(lblInvoiceNo, "Invoice No.", lx1, r3);
            MakeTextBox(txtInvoiceNo, "txtInvoiceNo", ix1, r3);
            MakeLabel(lblNoOfPkgs, "No. of Pkgs", lx2, r3);
            MakeTextBox(txtNoOfPkgs, "txtNoOfPkgs", ix2, r3);
            MakeLabel(lblShippingBillNo, "Shipping Bill No.", lx3, r3);
            MakeTextBox(txtShippingBillNo, "txtShippingBillNo", ix3, r3);
            MakeLabel(lblWeightKG, "Weight (KG)", lx4, r3);
            MakeTextBox(txtWeightKG, "txtWeightKG", ix4, r3);

            // ══ ROW 4 — Ports, Shipping Line, Booking No
            int r4 = r3 + rh;
            MakeLabel(lblPortOfLoading, "Port of Loading", lx1, r4);
            MakeTextBox(txtPortOfLoading, "txtPortOfLoading", ix1, r4);
            MakeLabel(lblPortOfDischarge, "Port of Discharge", lx2, r4);
            MakeTextBox(txtPortOfDischarge, "txtPortOfDischarge", ix2, r4);
            MakeLabel(lblShippingLine, "Shipping Line", lx3, r4);
            MakeTextBox(txtShippingLine, "txtShippingLine", ix3, r4);
            MakeLabel(lblBookingNo, "Booking No.", lx4, r4);
            MakeTextBox(txtBookingNo, "txtBookingNo", ix4, r4);

            // ══ ROW 5 — Booking Rel Date, Plan Vessel, SOB, ETD
            int r5 = r4 + rh;
            MakeLabel(lblBookingRelDate, "Booking Rel. Date", lx1, r5);
            MakeDTP(dtpBookingRelDate, "dtpBookingRelDate", ix1, r5);
            MakeLabel(lblPlanVessel, "Plan Vessel", lx2, r5);
            MakeTextBox(txtPlanVessel, "txtPlanVessel", ix2, r5);
            MakeLabel(lblSOB, "SOB", lx3, r5);
            MakeDTP(dtpSOB, "dtpSOB", ix3, r5);
            MakeLabel(lblETD, "ETD", lx4, r5);
            MakeDTP(dtpETD, "dtpETD", ix4, r5);

            // ══ ROW 6 — ETA, Draft Received, Invoice Received, CHA Name
            int r6 = r5 + rh;
            MakeLabel(lblETA, "ETA", lx1, r6);
            MakeDTP(dtpETA, "dtpETA", ix1, r6);
            MakeLabel(lblDraftReceived, "Draft Received", lx2, r6);
            MakeDTP(dtpDraftReceived, "dtpDraftReceived", ix2, r6);
            MakeLabel(lblInvoiceReceived, "Invoice Received", lx3, r6);
            MakeDTP(dtpInvoiceReceived, "dtpInvoiceReceived", ix3, r6);
            MakeLabel(lblCHAName, "CHA Name", lx4, r6);
            MakeTextBox(txtCHAName, "txtCHAName", ix4, r6);

            // ══ ROW 7 — Cargo Carting, Stuffing, VGM, Shipment Handover
            int r7 = r6 + rh;
            MakeLabel(lblCargoCartingDt, "Cargo Carting Dt", lx1, r7);
            MakeDTP(dtpCargoCartingDt, "dtpCargoCartingDt", ix1, r7);
            MakeLabel(lblStuffingDt, "Stuffing Dt", lx2, r7);
            MakeDTP(dtpStuffingDt, "dtpStuffingDt", ix2, r7);
            MakeLabel(lblVGMSubmitDate, "VGM Submit Date", lx3, r7);
            MakeDTP(dtpVGMSubmitDate, "dtpVGMSubmitDate", ix3, r7);
            MakeLabel(lblShipHandoverDt, "Shipment Handover", lx4, r7);
            MakeDTP(dtpShipHandoverDt, "dtpShipHandoverDt", ix4, r7);

            // ══ ROW 8 — Custom Clear, Humidity, Containers, Line Seal
            int r8 = r7 + rh;
            MakeLabel(lblCustomClearDate, "Custom Clear Date", lx1, r8);
            MakeDTP(dtpCustomClearDate, "dtpCustomClearDate", ix1, r8);
            MakeLabel(lblHumidity, "Humidity", lx2, r8);
            MakeTextBox(txtHumidity, "txtHumidity", ix2, r8);
            MakeLabel(lblNoContainers, "No. of Containers", lx3, r8);
            MakeTextBox(txtNoContrs, "txtNoContainers", ix3, r8);
            MakeLabel(lblLineSeal, "Line Seal", lx4, r8);
            MakeTextBox(txtLineSeal, "txtLineSeal", ix4, r8);

            // ══ ROW 9 — Cust Seal, BL Release, Gate In POL, HBL No
            int r9 = r8 + rh;
            MakeLabel(lblCustSeal, "Cust. Seal", lx1, r9);
            MakeTextBox(txtCustSeal, "txtCustSeal", ix1, r9);
            MakeLabel(lblBLRelDate, "B/L Release Date", lx2, r9);
            MakeDTP(dtpBLRelDate, "dtpBLRelDate", ix2, r9);
            MakeLabel(lblGateInPOL, "Gate In at POL", lx3, r9);
            MakeDTP(dtpGateInPOL, "dtpGateInPOL", ix3, r9);
            MakeLabel(lblHBLNo, "HBL No.", lx4, r9);
            MakeTextBox(txtHBLNo, "txtHBLNo", ix4, r9);

            // ══ ROW 10 — MBL No, SI Submit, Doc Delivery, Notes
            int r10 = r9 + rh;
            MakeLabel(lblMBLNo, "MBL No.", lx1, r10);
            MakeTextBox(txtMBLNo, "txtMBLNo", ix1, r10);
            MakeLabel(lblSISubmitDt, "SI Submit Dt", lx2, r10);
            MakeDTP(dtpSISubmitDt, "dtpSISubmitDt", ix2, r10);
            MakeLabel(lblDocDeliveryDt, "Doc Delivery Dt", lx3, r10);
            MakeDTP(dtpDocDeliveryDt, "dtpDocDeliveryDt", ix3, r10);
            MakeLabel(lblNotes, "Notes", lx4, r10);
            MakeTextBox(txtNotes, "txtNotes", ix4, r10);

            // ══ BUTTONS ════════════════════════════
            int btnY = r10 + rh + 8;
            int bw = 120, bh = 36;

            btnAdd.Text = "➕  Add";
            btnAdd.Location = new Point(10, btnY);
            btnAdd.Size = new Size(bw, bh);
            btnAdd.BackColor = Color.FromArgb(0, 150, 0);
            btnAdd.ForeColor = Color.White;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.Click += btnAdd_Click;

            btnUpdate.Text = "✏️  Update";
            btnUpdate.Location = new Point(140, btnY);
            btnUpdate.Size = new Size(bw, bh);
            btnUpdate.BackColor = Color.FromArgb(0, 102, 204);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.Click += btnUpdate_Click;

            btnDelete.Text = "🗑️  Delete";
            btnDelete.Location = new Point(270, btnY);
            btnDelete.Size = new Size(bw, bh);
            btnDelete.BackColor = Color.FromArgb(200, 0, 0);
            btnDelete.ForeColor = Color.White;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.Click += btnDelete_Click;

            btnClear.Text = "🔄  Clear";
            btnClear.Location = new Point(400, btnY);
            btnClear.Size = new Size(bw, bh);
            btnClear.BackColor = Color.FromArgb(100, 100, 100);
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.Click += btnClear_Click;

            btnAddReminder.Text = "🔔  Add Reminder";
            btnAddReminder.Location = new Point(530, btnY);
            btnAddReminder.Size = new Size(150, bh);
            btnAddReminder.BackColor = Color.FromArgb(180, 100, 0);
            btnAddReminder.ForeColor = Color.White;
            btnAddReminder.FlatStyle = FlatStyle.Flat;
            btnAddReminder.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnAddReminder.FlatAppearance.BorderSize = 0;
            btnAddReminder.Click += btnAddReminder_Click;

            // ══ SEARCH ════════════════════════════
            int searchY = btnY + bh + 10;
            lblSearch.Text = "🔍  Search:";
            lblSearch.Location = new Point(10, searchY + 3);
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Arial", 9F, FontStyle.Bold);

            txtSearch.Name = "txtSearch";
            txtSearch.Location = new Point(100, searchY);
            txtSearch.Size = new Size(300, 26);
            txtSearch.Font = new Font("Arial", 9F);
            txtSearch.BorderStyle = BorderStyle.FixedSingle;

            // ══ DATAGRIDVIEW ══════════════════════
            int gridY = searchY + 36;
            dgvJobCards.Name = "dgvJobCards";
            dgvJobCards.Location = new Point(10, gridY);
            dgvJobCards.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                            | AnchorStyles.Left | AnchorStyles.Right;
            dgvJobCards.Size = new Size(1410, 280);
            dgvJobCards.ReadOnly = true;
            dgvJobCards.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvJobCards.MultiSelect = false;
            dgvJobCards.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvJobCards.BackgroundColor = Color.White;
            dgvJobCards.BorderStyle = BorderStyle.None;
            dgvJobCards.RowHeadersVisible = false;
            dgvJobCards.AllowUserToAddRows = false;
            dgvJobCards.Font = new Font("Arial", 9F);
            dgvJobCards.CellClick += dgvJobCards_CellClick;

            // ══ ADD ALL TO JOB CARD TAB ═══════════
            tabJobCard.Controls.AddRange(new Control[]
            {
                lblTitle,
                lblJobCardNo,   txtJobCardNo,
                lblJobCardType, cmbJobCardType,
                lblFCLLCL,      cmbFCLLCL,
                lblStatus,      cmbStatus,
                lblDateCreated, dtpDateCreated,
                lblShipper,     txtShipper,
                lblConsignee,   txtConsignee,
                lblCustomer,    cmbCustomer,
                lblInvoiceNo,   txtInvoiceNo,
                lblNoOfPkgs,    txtNoOfPkgs,
                lblShippingBillNo, txtShippingBillNo,
                lblWeightKG,    txtWeightKG,
                lblPortOfLoading,   txtPortOfLoading,
                lblPortOfDischarge, txtPortOfDischarge,
                lblShippingLine,    txtShippingLine,
                lblBookingNo,       txtBookingNo,
                lblBookingRelDate,  dtpBookingRelDate,
                lblPlanVessel,      txtPlanVessel,
                lblSOB,             dtpSOB,
                lblETD,             dtpETD,
                lblETA,             dtpETA,
                lblDraftReceived,   dtpDraftReceived,
                lblInvoiceReceived, dtpInvoiceReceived,
                lblCHAName,         txtCHAName,
                lblCargoCartingDt,  dtpCargoCartingDt,
                lblStuffingDt,      dtpStuffingDt,
                lblVGMSubmitDate,   dtpVGMSubmitDate,
                lblShipHandoverDt,  dtpShipHandoverDt,
                lblCustomClearDate, dtpCustomClearDate,
                lblHumidity,        txtHumidity,
                lblNoContainers,    txtNoContrs,
                lblLineSeal,        txtLineSeal,
                lblCustSeal,        txtCustSeal,
                lblBLRelDate,       dtpBLRelDate,
                lblGateInPOL,       dtpGateInPOL,
                lblHBLNo,           txtHBLNo,
                lblMBLNo,           txtMBLNo,
                lblSISubmitDt,      dtpSISubmitDt,
                lblDocDeliveryDt,   dtpDocDeliveryDt,
                lblNotes,           txtNotes,
                btnAdd, btnUpdate, btnDelete,
                btnClear, btnAddReminder,
                lblSearch, txtSearch,
                dgvJobCards
            });

            // ══ REMINDERS TAB ═════════════════════
            lblReminderTitle.Text = "🔔  REMINDERS";
            lblReminderTitle.Font = new Font("Arial", 14F, FontStyle.Bold);
            lblReminderTitle.Location = new Point(10, 10);
            lblReminderTitle.AutoSize = true;
            lblReminderTitle.ForeColor = Color.FromArgb(0, 51, 102);

            dgvReminders.Name = "dgvReminders";
            dgvReminders.Location = new Point(10, 45);
            dgvReminders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                                             | AnchorStyles.Left | AnchorStyles.Right;
            dgvReminders.Size = new Size(1140, 700);
            dgvReminders.ReadOnly = true;
            dgvReminders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvReminders.MultiSelect = false;
            dgvReminders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvReminders.BackgroundColor = Color.White;
            dgvReminders.BorderStyle = BorderStyle.None;
            dgvReminders.RowHeadersVisible = false;
            dgvReminders.AllowUserToAddRows = false;
            dgvReminders.Font = new Font("Arial", 9F);
    

            tabReminders.Controls.Add(lblReminderTitle);
            tabReminders.Controls.Add(dgvReminders);

            // ══ USER CONTROL ══════════════════════
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            Dock = DockStyle.Fill;
            Name = "UC_JobCards";
            Controls.Add(tabControl);

            ((System.ComponentModel.ISupportInitialize)dgvJobCards).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvReminders).EndInit();
            ResumeLayout(false);
        }

        // ── PRIVATE FIELDS ────────────────────────
        private TabControl tabControl;
        private TabPage tabJobCard, tabReminders;
        private Label lblTitle, lblReminderTitle;
        private Label lblJobCardNo, lblJobCardType, lblFCLLCL, lblStatus;
        private Label lblDateCreated, lblShipper, lblConsignee, lblCustomer;
        private Label lblInvoiceNo, lblNoOfPkgs, lblShippingBillNo, lblWeightKG;
        private Label lblPortOfLoading, lblPortOfDischarge, lblShippingLine, lblBookingNo;
        private Label lblBookingRelDate, lblPlanVessel, lblSOB, lblETD;
        private Label lblETA, lblDraftReceived, lblInvoiceReceived, lblCHAName;
        private Label lblCargoCartingDt, lblStuffingDt, lblVGMSubmitDate, lblShipHandoverDt;
        private Label lblCustomClearDate, lblHumidity, lblNoContainers, lblLineSeal;
        private Label lblCustSeal, lblBLRelDate, lblGateInPOL, lblHBLNo;
        private Label lblMBLNo, lblSISubmitDt, lblDocDeliveryDt, lblNotes;
        private Label lblSearch;
        private TextBox txtJobCardNo, txtShipper, txtConsignee;
        private TextBox txtInvoiceNo, txtNoOfPkgs, txtShippingBillNo, txtWeightKG;
        private TextBox txtPortOfLoading, txtPortOfDischarge, txtShippingLine, txtBookingNo;
        private TextBox txtPlanVessel, txtCHAName;
        private TextBox txtHumidity, txtNoContrs, txtLineSeal;
        private Label lblNoContrs;
        private TextBox txtCustSeal, txtHBLNo, txtMBLNo, txtNotes;
        private TextBox txtSearch;
        private ComboBox cmbJobCardType, cmbFCLLCL, cmbStatus, cmbCustomer;

        // ── DATE PICKERS ──────────────────────────
        private DateTimePicker dtpDateCreated;
        private DateTimePicker dtpBookingRelDate;
        private DateTimePicker dtpSOB;
        private DateTimePicker dtpETD;
        private DateTimePicker dtpETA;
        private DateTimePicker dtpDraftReceived;
        private DateTimePicker dtpInvoiceReceived;
        private DateTimePicker dtpCargoCartingDt;
        private DateTimePicker dtpStuffingDt;
        private DateTimePicker dtpVGMSubmitDate;
        private DateTimePicker dtpShipHandoverDt;
        private DateTimePicker dtpCustomClearDate;
        private DateTimePicker dtpBLRelDate;
        private DateTimePicker dtpGateInPOL;
        private DateTimePicker dtpSISubmitDt;
        private DateTimePicker dtpDocDeliveryDt;

        private Button btnAdd, btnUpdate, btnDelete, btnClear, btnAddReminder;
        private DataGridView dgvJobCards, dgvReminders;
        private Panel pnlForm, pnlButtons, pnlGrid;
    }
}