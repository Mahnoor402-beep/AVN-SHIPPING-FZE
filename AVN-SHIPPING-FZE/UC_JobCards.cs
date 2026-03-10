using Microsoft.Data.Sqlite;
using System.Data;

namespace AVN_SHIPPING_FZE
{
    public partial class UC_JobCards : UserControl
    {
        private int selectedJobCardID = -1;

        public UC_JobCards()
        {
            InitializeComponent();
            try
            {
                // Style grid headers
                dgvJobCards.EnableHeadersVisualStyles = false;
                dgvJobCards.ColumnHeadersDefaultCellStyle.Font =
                    new Font("Arial", 9F, FontStyle.Bold);
                dgvJobCards.ColumnHeadersDefaultCellStyle.BackColor =
                    Color.FromArgb(0, 51, 102);
                dgvJobCards.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

                dgvReminders.EnableHeadersVisualStyles = false;
                dgvReminders.ColumnHeadersDefaultCellStyle.Font =
                    new Font("Arial", 9F, FontStyle.Bold);
                dgvReminders.ColumnHeadersDefaultCellStyle.BackColor =
                    Color.FromArgb(0, 51, 102);
                dgvReminders.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            }
            catch { }

            LoadCustomersDropdown();
            LoadJobCards();
            LoadReminders();
            txtSearch.TextChanged += (s, e) => LoadJobCards(txtSearch.Text);
        }

        // ── HELPER: Get Date from DTP ──────────────
        private string DTPVal(DateTimePicker d) =>
            d.Checked ? d.Value.ToString("yyyy-MM-dd") : "";

        // ── HELPER: Set Date to DTP ────────────────
        private void SetDTP(DateTimePicker dtp, object val)
        {
            if (DateTime.TryParse(val?.ToString(), out DateTime dt))
            { dtp.Checked = true; dtp.Value = dt; }
            else dtp.Checked = false;
        }

        // ── HELPER: Reset DTP ─────────────────────
        private void ResetDTP(DateTimePicker d) => d.Checked = false;

        // ── AUTO GENERATE JOB CARD NUMBER ─────────
        private string GenerateJobCardNo()
        {
            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(
                "SELECT COUNT(*) FROM JobCards", con);
            int count = Convert.ToInt32(cmd.ExecuteScalar());
            return $"AVN-JC-{DateTime.Now.Year}-{(count + 1):D4}";
        }

        // ── LOAD CUSTOMERS DROPDOWN ───────────────
        private void LoadCustomersDropdown()
        {
            try
            {
                cmbCustomer.Items.Clear();
                cmbCustomer.Items.Add(new CustomerItem(0, "-- Select Customer --"));

                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "SELECT CustomerID, FullName FROM Customers ORDER BY FullName", con);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cmbCustomer.Items.Add(new CustomerItem(
                        Convert.ToInt32(reader["CustomerID"]),
                        reader["FullName"].ToString()!));
                }
                cmbCustomer.SelectedIndex = 0;
                txtJobCardNo.Text = GenerateJobCardNo();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dropdown error: " + ex.Message);
            }
        }

        // ── LOAD JOB CARDS INTO GRID ──────────────
        private void LoadJobCards(string search = "")
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();

                string query = @"
                    SELECT j.JobCardID, j.JobCardNo, j.JobCardType,
                           j.DateCreated, j.Status, j.Shipper,
                           j.Consignee, j.PortOfLoading, j.PortOfDischarge,
                           j.ETD, j.ETA, j.FCLOrLCL,
                           j.BookingNo, j.HBLNo, j.MBLNo,
                           c.FullName as Customer
                    FROM JobCards j
                    LEFT JOIN Customers c ON j.CustomerID = c.CustomerID
                    WHERE j.JobCardNo LIKE @s
                    OR j.Shipper LIKE @s
                    OR j.Consignee LIKE @s
                    OR j.Status LIKE @s
                    OR c.FullName LIKE @s
                    ORDER BY j.JobCardID DESC";

                using var cmd = new SqliteCommand(query, con);
                cmd.Parameters.AddWithValue("@s", $"%{search}%");

                var table = new DataTable();
                table.Columns.Add("JobCardID");
                table.Columns.Add("Job Card No");
                table.Columns.Add("Type");
                table.Columns.Add("Date");
                table.Columns.Add("Status");
                table.Columns.Add("Shipper");
                table.Columns.Add("Consignee");
                table.Columns.Add("POL");
                table.Columns.Add("POD");
                table.Columns.Add("ETD");
                table.Columns.Add("ETA");
                table.Columns.Add("FCL/LCL");
                table.Columns.Add("Booking No");
                table.Columns.Add("HBL No");
                table.Columns.Add("MBL No");
                table.Columns.Add("Customer");

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add(
                        reader["JobCardID"].ToString(),
                        reader["JobCardNo"].ToString(),
                        reader["JobCardType"].ToString(),
                        reader["DateCreated"].ToString(),
                        reader["Status"].ToString(),
                        reader["Shipper"].ToString(),
                        reader["Consignee"].ToString(),
                        reader["PortOfLoading"].ToString(),
                        reader["PortOfDischarge"].ToString(),
                        reader["ETD"].ToString(),
                        reader["ETA"].ToString(),
                        reader["FCLOrLCL"].ToString(),
                        reader["BookingNo"].ToString(),
                        reader["HBLNo"].ToString(),
                        reader["MBLNo"].ToString(),
                        reader["Customer"].ToString()
                    );
                }

                dgvJobCards.DataSource = null;
                dgvJobCards.DataSource = table;

                foreach (DataGridViewRow row in dgvJobCards.Rows)
                {
                    string status = row.Cells["Status"].Value?.ToString() ?? "";
                    row.DefaultCellStyle.BackColor = status switch
                    {
                        "Completed" => Color.FromArgb(200, 255, 200),
                        "Cancelled" => Color.FromArgb(255, 200, 200),
                        "In Progress" => Color.FromArgb(255, 255, 200),
                        "On Hold" => Color.FromArgb(255, 220, 150),
                        _ => Color.White
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // ── LOAD REMINDERS ────────────────────────
        private void LoadReminders()
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();

                using var cmd = new SqliteCommand(@"
                    SELECT r.ReminderID, r.JobCardNo, r.EventType,
                           r.EventDate, r.ReminderDate, r.Status,
                           r.EmailSent, r.RecipientEmail,
                           r.Priority, r.Notes
                    FROM Reminders r
                    ORDER BY r.ReminderDate ASC", con);

                var table = new DataTable();
                table.Columns.Add("ReminderID");
                table.Columns.Add("Job Card No");
                table.Columns.Add("Event Type");
                table.Columns.Add("Event Date");
                table.Columns.Add("Reminder Date");
                table.Columns.Add("Status");
                table.Columns.Add("Email Sent");
                table.Columns.Add("Recipient Email");
                table.Columns.Add("Priority");
                table.Columns.Add("Notes");

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add(
                        reader["ReminderID"].ToString(),
                        reader["JobCardNo"].ToString(),
                        reader["EventType"].ToString(),
                        reader["EventDate"].ToString(),
                        reader["ReminderDate"].ToString(),
                        reader["Status"].ToString(),
                        reader["EmailSent"].ToString() == "1" ? "✅ Yes" : "❌ No",
                        reader["RecipientEmail"].ToString(),
                        reader["Priority"].ToString(),
                        reader["Notes"].ToString()
                    );
                }

                dgvReminders.DataSource = null;
                dgvReminders.DataSource = table;

                foreach (DataGridViewRow row in dgvReminders.Rows)
                {
                    string rd = row.Cells["Reminder Date"].Value?.ToString() ?? "";
                    if (DateTime.TryParse(rd, out DateTime d))
                    {
                        if (d.Date < DateTime.Today &&
                            row.Cells["Status"].Value?.ToString() == "Pending")
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 180, 180);
                            row.DefaultCellStyle.Font = new Font("Arial", 9F, FontStyle.Bold);
                        }
                        else if (d.Date == DateTime.Today)
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 150);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Reminder load error: " + ex.Message);
            }
        }

        // ── CLICK ROW → FILL FORM ─────────────────
        private void dgvJobCards_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvJobCards.Rows[e.RowIndex];
            selectedJobCardID = Convert.ToInt32(row.Cells["JobCardID"].Value);
            LoadJobCardDetails(selectedJobCardID);
        }

        private void LoadJobCardDetails(int id)
        {
            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(
                "SELECT * FROM JobCards WHERE JobCardID=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return;

            // ── Text fields ───────────────────────
            txtJobCardNo.Text = reader["JobCardNo"].ToString();
            txtShipper.Text = reader["Shipper"].ToString();
            txtConsignee.Text = reader["Consignee"].ToString();
            txtInvoiceNo.Text = reader["InvoiceNumber"].ToString();
            txtNoOfPkgs.Text = reader["NoOfPkgs"].ToString();
            txtShippingBillNo.Text = reader["ShippingBillNo"].ToString();
            txtWeightKG.Text = reader["WeightKG"].ToString();
            txtPortOfLoading.Text = reader["PortOfLoading"].ToString();
            txtPortOfDischarge.Text = reader["PortOfDischarge"].ToString();
            txtShippingLine.Text = reader["ShippingLine"].ToString();
            txtBookingNo.Text = reader["BookingNo"].ToString();
            txtPlanVessel.Text = reader["PlanVessel"].ToString();
            txtCHAName.Text = reader["CHAName"].ToString();
            txtHumidity.Text = reader["HumidityReq"].ToString();
            txtNoContrs.Text = reader["NoOfContrs"].ToString();
            txtLineSeal.Text = reader["LineSeal"].ToString();
            txtCustSeal.Text = reader["CustSeal"].ToString();
            txtHBLNo.Text = reader["HBLNo"].ToString();
            txtMBLNo.Text = reader["MBLNo"].ToString();
            txtNotes.Text = reader["Notes"].ToString();

            // ── Date Pickers ──────────────────────
            SetDTP(dtpDateCreated, reader["DateCreated"]);
            SetDTP(dtpBookingRelDate, reader["BookingReleaseDate"]);
            SetDTP(dtpSOB, reader["SOB"]);
            SetDTP(dtpETD, reader["ETD"]);
            SetDTP(dtpETA, reader["ETA"]);
            SetDTP(dtpDraftReceived, reader["DraftReceived"]);
            SetDTP(dtpInvoiceReceived, reader["InvoiceReceived"]);
            SetDTP(dtpCargoCartingDt, reader["CargoCartingDt"]);
            SetDTP(dtpStuffingDt, reader["StuffingDt"]);
            SetDTP(dtpVGMSubmitDate, reader["VGMSubmitDate"]);
            SetDTP(dtpShipHandoverDt, reader["ShipmentHandoverDt"]);
            SetDTP(dtpCustomClearDate, reader["CustomClearDate"]);
            SetDTP(dtpBLRelDate, reader["BLReleasedDate"]);
            SetDTP(dtpGateInPOL, reader["GateInPOLDate"]);
            SetDTP(dtpSISubmitDt, reader["SISubmitDt"]);
            SetDTP(dtpDocDeliveryDt, reader["DocumentDeliveryDt"]);

            // ── Dropdowns ─────────────────────────
            SetCombo(cmbJobCardType, reader["JobCardType"].ToString());
            SetCombo(cmbFCLLCL, reader["FCLOrLCL"].ToString());
            SetCombo(cmbStatus, reader["Status"].ToString());

            int custID = Convert.ToInt32(reader["CustomerID"]);
            foreach (CustomerItem item in cmbCustomer.Items)
            {
                if (item.ID == custID)
                { cmbCustomer.SelectedItem = item; break; }
            }
        }

        private void SetCombo(ComboBox cmb, string? value)
        {
            int idx = cmb.Items.IndexOf(value ?? "");
            if (idx >= 0) cmb.SelectedIndex = idx;
        }

        // ── ADD JOB CARD ──────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtShipper.Text))
            {
                MessageBox.Show("Shipper is required!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // ✅ ADD THIS CHECK
            var cust = cmbCustomer.SelectedItem as CustomerItem;
            if (cust == null || cust.ID == 0)
            {
                MessageBox.Show("Please select a Customer!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(@"INSERT INTO JobCards
                (JobCardNo, JobCardType, DateCreated, Status, Shipper,
                 Consignee, InvoiceNumber, NoOfPkgs, ShippingBillNo,
                 PortOfLoading, PortOfDischarge, WeightKG, ShippingLine,
                 BookingNo, BookingReleaseDate, PlanVessel, SOB, ETD, ETA,
                 DraftReceived, InvoiceReceived, CHAName, CargoCartingDt,
                 StuffingDt, VGMSubmitDate, ShipmentHandoverDt,
                 CustomClearDate, HumidityReq, NoOfContrs,
                 LineSeal, CustSeal, FCLOrLCL, BLReleasedDate,
                 GateInPOLDate, HBLNo, MBLNo, SISubmitDt,
                 DocumentDeliveryDt, Notes, CustomerID)
                VALUES
                (@jcno,@type,@date,@status,@shipper,
                 @consignee,@invno,@pkgs,@shippingbill,
                 @pol,@pod,@weight,@shippingline,
                 @bookingno,@bookingrel,@vessel,@sob,@etd,@eta,
                 @draft,@invreceived,@cha,@cargoing,
                 @stuffing,@vgm,@handover,
                 @customclear,@humidity,@containers,
                 @lineseal,@custseal,@fcllcl,@blrelease,
                 @gatein,@hbl,@mbl,@sisubmit,
                 @docdelivery,@notes,@custid)", con);

            AddParameters(cmd);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Job Card added! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadJobCards();
        }

        // ── UPDATE JOB CARD ───────────────────────
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedJobCardID == -1)
            {
                MessageBox.Show("Select a job card first!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var custCheck = cmbCustomer.SelectedItem as CustomerItem;
            if (custCheck == null || custCheck.ID == 0)
            {
                MessageBox.Show("Please select a Customer!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(@"UPDATE JobCards SET
        JobCardNo=@jcno, JobCardType=@type, DateCreated=@date,
        Status=@status, Shipper=@shipper, Consignee=@consignee,
        InvoiceNumber=@invno, NoOfPkgs=@pkgs,
        ShippingBillNo=@shippingbill, PortOfLoading=@pol,
        PortOfDischarge=@pod, WeightKG=@weight,
        ShippingLine=@shippingline, BookingNo=@bookingno,
        BookingReleaseDate=@bookingrel, PlanVessel=@vessel,
        SOB=@sob, ETD=@etd, ETA=@eta,
        DraftReceived=@draft, InvoiceReceived=@invreceived,
        CHAName=@cha, CargoCartingDt=@cargoing,
        StuffingDt=@stuffing, VGMSubmitDate=@vgm,
        ShipmentHandoverDt=@handover, CustomClearDate=@customclear,
        HumidityReq=@humidity, NoOfContrs=@containers,
        LineSeal=@lineseal, CustSeal=@custseal,
        FCLOrLCL=@fcllcl, BLReleasedDate=@blrelease,
        GateInPOLDate=@gatein, HBLNo=@hbl, MBLNo=@mbl,
        SISubmitDt=@sisubmit, DocumentDeliveryDt=@docdelivery,
        Notes=@notes, CustomerID=@custid
        WHERE JobCardID=@id", con);

            AddParameters(cmd);
            cmd.Parameters.AddWithValue("@id", selectedJobCardID);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Job Card updated! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadJobCards();
        }
        // ── DELETE JOB CARD ───────────────────────
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedJobCardID == -1)
            {
                MessageBox.Show("Select a job card to delete!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this job card?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "DELETE FROM JobCards WHERE JobCardID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedJobCardID);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Job Card deleted! 🗑️", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadJobCards();
            }
        }

        // ── ADD REMINDER ──────────────────────────
        private void btnAddReminder_Click(object sender, EventArgs e)
        {
            if (selectedJobCardID == -1)
            {
                MessageBox.Show("Please select a Job Card first!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var form = new ReminderForm(txtJobCardNo.Text, selectedJobCardID);
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadReminders();
                MessageBox.Show("Reminder added! 🔔", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // ── SHARED PARAMETER HELPER ───────────────
        private void AddParameters(SqliteCommand cmd)
        {
            var cust = cmbCustomer.SelectedItem as CustomerItem;
            cmd.Parameters.AddWithValue("@jcno", txtJobCardNo.Text.Trim());
            cmd.Parameters.AddWithValue("@type", cmbJobCardType.SelectedItem?.ToString());
            cmd.Parameters.AddWithValue("@date", DTPVal(dtpDateCreated));
            cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem?.ToString());
            cmd.Parameters.AddWithValue("@shipper", txtShipper.Text.Trim());
            cmd.Parameters.AddWithValue("@consignee", txtConsignee.Text.Trim());
            cmd.Parameters.AddWithValue("@invno", txtInvoiceNo.Text.Trim());
            cmd.Parameters.AddWithValue("@pkgs", txtNoOfPkgs.Text.Trim());
            cmd.Parameters.AddWithValue("@shippingbill", txtShippingBillNo.Text.Trim());
            cmd.Parameters.AddWithValue("@pol", txtPortOfLoading.Text.Trim());
            cmd.Parameters.AddWithValue("@pod", txtPortOfDischarge.Text.Trim());
            cmd.Parameters.AddWithValue("@weight", txtWeightKG.Text.Trim());
            cmd.Parameters.AddWithValue("@shippingline", txtShippingLine.Text.Trim());
            cmd.Parameters.AddWithValue("@bookingno", txtBookingNo.Text.Trim());
            cmd.Parameters.AddWithValue("@bookingrel", DTPVal(dtpBookingRelDate));
            cmd.Parameters.AddWithValue("@vessel", txtPlanVessel.Text.Trim());
            cmd.Parameters.AddWithValue("@sob", DTPVal(dtpSOB));
            cmd.Parameters.AddWithValue("@etd", DTPVal(dtpETD));
            cmd.Parameters.AddWithValue("@eta", DTPVal(dtpETA));
            cmd.Parameters.AddWithValue("@draft", DTPVal(dtpDraftReceived));
            cmd.Parameters.AddWithValue("@invreceived", DTPVal(dtpInvoiceReceived));
            cmd.Parameters.AddWithValue("@cha", txtCHAName.Text.Trim());
            cmd.Parameters.AddWithValue("@cargoing", DTPVal(dtpCargoCartingDt));
            cmd.Parameters.AddWithValue("@stuffing", DTPVal(dtpStuffingDt));
            cmd.Parameters.AddWithValue("@vgm", DTPVal(dtpVGMSubmitDate));
            cmd.Parameters.AddWithValue("@handover", DTPVal(dtpShipHandoverDt));
            cmd.Parameters.AddWithValue("@customclear", DTPVal(dtpCustomClearDate));
            cmd.Parameters.AddWithValue("@humidity", txtHumidity.Text.Trim());
            cmd.Parameters.AddWithValue("@containers", txtNoContrs.Text.Trim());
            cmd.Parameters.AddWithValue("@lineseal", txtLineSeal.Text.Trim());
            cmd.Parameters.AddWithValue("@custseal", txtCustSeal.Text.Trim());
            cmd.Parameters.AddWithValue("@fcllcl", cmbFCLLCL.SelectedItem?.ToString());
            cmd.Parameters.AddWithValue("@blrelease", DTPVal(dtpBLRelDate));
            cmd.Parameters.AddWithValue("@gatein", DTPVal(dtpGateInPOL));
            cmd.Parameters.AddWithValue("@hbl", txtHBLNo.Text.Trim());
            cmd.Parameters.AddWithValue("@mbl", txtMBLNo.Text.Trim());
            cmd.Parameters.AddWithValue("@sisubmit", DTPVal(dtpSISubmitDt));
            cmd.Parameters.AddWithValue("@docdelivery", DTPVal(dtpDocDeliveryDt));
            cmd.Parameters.AddWithValue("@notes", txtNotes.Text.Trim());
            cmd.Parameters.AddWithValue("@custid",
                (cust == null || cust.ID == 0) ? DBNull.Value : (object)cust.ID);
        }

        // ── CLEAR FORM ────────────────────────────
        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            selectedJobCardID = -1;
            txtJobCardNo.Text = GenerateJobCardNo();
            txtShipper.Text = txtConsignee.Text = txtInvoiceNo.Text = "";
            txtNoOfPkgs.Text = txtShippingBillNo.Text = txtWeightKG.Text = "";
            txtPortOfLoading.Text = txtPortOfDischarge.Text = "";
            txtShippingLine.Text = txtBookingNo.Text = txtPlanVessel.Text = "";
            txtCHAName.Text = txtHumidity.Text = txtNoContrs.Text = "";
            txtLineSeal.Text = txtCustSeal.Text = txtHBLNo.Text = "";
            txtMBLNo.Text = txtNotes.Text = txtSearch.Text = "";

            // Reset all date pickers
            ResetDTP(dtpBookingRelDate);
            ResetDTP(dtpSOB);
            ResetDTP(dtpETD);
            ResetDTP(dtpETA);
            ResetDTP(dtpDraftReceived);
            ResetDTP(dtpInvoiceReceived);
            ResetDTP(dtpCargoCartingDt);
            ResetDTP(dtpStuffingDt);
            ResetDTP(dtpVGMSubmitDate);
            ResetDTP(dtpShipHandoverDt);
            ResetDTP(dtpCustomClearDate);
            ResetDTP(dtpBLRelDate);
            ResetDTP(dtpGateInPOL);
            ResetDTP(dtpSISubmitDt);
            ResetDTP(dtpDocDeliveryDt);

            // Date Created always defaults to today
            dtpDateCreated.Checked = true;
            dtpDateCreated.Value = DateTime.Today;

            cmbJobCardType.SelectedIndex = 0;
            cmbFCLLCL.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            cmbCustomer.SelectedIndex = 0;
        }
    }
}