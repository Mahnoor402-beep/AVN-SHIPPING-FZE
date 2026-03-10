using Microsoft.Data.Sqlite;
using System.Data;

namespace AVN_SHIPPING_FZE
{
    public partial class UC_Billing : UserControl
    {
        private int selectedBillingID = -1;

        public UC_Billing()
        {
            InitializeComponent();
            LoadCustomers();
            LoadShipments();
            LoadBilling();
            txtSearch.TextChanged += (s, e) => LoadBilling(txtSearch.Text);
            txtAmount.TextChanged += CalculateTotal;
            txtTax.TextChanged += CalculateTotal;
        }

        private void CalculateTotal(object? sender, EventArgs e)
        {
            try
            {
                double amount = double.Parse(
                    string.IsNullOrWhiteSpace(txtAmount.Text) ? "0" : txtAmount.Text);
                double tax = double.Parse(
                    string.IsNullOrWhiteSpace(txtTax.Text) ? "0" : txtTax.Text);
                txtTotal.Text = (amount + (amount * tax / 100)).ToString("F2");
            }
            catch { txtTotal.Text = "0.00"; }
        }

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
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    cmbCustomer.Items.Add(new CustomerItem(
                        Convert.ToInt32(reader["CustomerID"]),
                        reader["FullName"].ToString()!));
                cmbCustomer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadShipments()
        {
            try
            {
                cmbShipment.Items.Clear();
                cmbShipment.Items.Add(new ShipmentItem(0, "-- Select Shipment --"));
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(@"
                    SELECT s.ShipmentID, c.FullName, s.Origin, s.Destination
                    FROM Shipments s
                    LEFT JOIN Customers c ON s.CustomerID = c.CustomerID
                    ORDER BY s.ShipmentID DESC", con);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    cmbShipment.Items.Add(new ShipmentItem(
                        Convert.ToInt32(reader["ShipmentID"]),
                        $"#{reader["ShipmentID"]} {reader["FullName"]} " +
                        $"({reader["Origin"]} → {reader["Destination"]})"));
                cmbShipment.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void LoadBilling(string search = "")
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(@"
                    SELECT b.BillingID, c.FullName as Customer,
                           b.InvoiceDate, b.Amount, b.Tax,
                           b.TotalAmount, b.PaymentStatus
                    FROM Billing b
                    LEFT JOIN Customers c ON b.CustomerID = c.CustomerID
                    WHERE c.FullName LIKE @s OR b.PaymentStatus LIKE @s
                    ORDER BY b.BillingID DESC", con);
                cmd.Parameters.AddWithValue("@s", $"%{search}%");

                var table = new DataTable();
                table.Columns.Add("BillingID");
                table.Columns.Add("Customer");
                table.Columns.Add("Invoice Date");
                table.Columns.Add("Amount");
                table.Columns.Add("Tax %");
                table.Columns.Add("Total");
                table.Columns.Add("Status");

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    table.Rows.Add(
                        reader["BillingID"].ToString(),
                        reader["Customer"].ToString(),
                        reader["InvoiceDate"].ToString(),
                        reader["Amount"].ToString(),
                        reader["Tax"].ToString(),
                        reader["TotalAmount"].ToString(),
                        reader["PaymentStatus"].ToString());

                dgvBilling.DataSource = null;
                dgvBilling.DataSource = table;

                foreach (DataGridViewRow row in dgvBilling.Rows)
                {
                    string status = row.Cells["Status"].Value?.ToString() ?? "";
                    row.DefaultCellStyle.BackColor = status switch
                    {
                        "Paid" => Color.FromArgb(200, 255, 200),
                        "Unpaid" => Color.FromArgb(255, 220, 220),
                        "Overdue" => Color.FromArgb(255, 180, 180),
                        "Partial" => Color.FromArgb(255, 255, 200),
                        _ => Color.White
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvBilling_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvBilling.Rows[e.RowIndex];
            selectedBillingID = Convert.ToInt32(row.Cells["BillingID"].Value);

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(
                "SELECT * FROM Billing WHERE BillingID=@id", con);
            cmd.Parameters.AddWithValue("@id", selectedBillingID);
            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return;

            txtAmount.Text = reader["Amount"].ToString();
            txtTax.Text = reader["Tax"].ToString();
            txtTotal.Text = reader["TotalAmount"].ToString();

            if (DateTime.TryParse(reader["InvoiceDate"].ToString(), out DateTime d))
                dtpInvoiceDate.Value = d;

            int idx = cmbStatus.Items.IndexOf(
                reader["PaymentStatus"].ToString() ?? "Unpaid");
            if (idx >= 0) cmbStatus.SelectedIndex = idx;

            int custID = Convert.ToInt32(reader["CustomerID"]);
            foreach (CustomerItem item in cmbCustomer.Items)
                if (item.ID == custID)
                { cmbCustomer.SelectedItem = item; break; }

            int shipID = Convert.ToInt32(reader["ShipmentID"]);
            foreach (ShipmentItem item in cmbShipment.Items)
                if (item.ID == shipID)
                { cmbShipment.SelectedItem = item; break; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a customer!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                MessageBox.Show("Please enter amount!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cust = (CustomerItem)cmbCustomer.SelectedItem!;
            var ship = cmbShipment.SelectedItem as ShipmentItem;

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(@"
                INSERT INTO Billing
                (CustomerID, ShipmentID, InvoiceDate,
                 Amount, Tax, TotalAmount, PaymentStatus)
                VALUES
                (@cid, @sid, @date,
                 @amount, @tax, @total, @status)", con);

            cmd.Parameters.AddWithValue("@cid", cust.ID);
            cmd.Parameters.AddWithValue("@sid", ship?.ID ?? 0);
            cmd.Parameters.AddWithValue("@date", dtpInvoiceDate.Value
                                                   .ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@amount", txtAmount.Text.Trim());
            cmd.Parameters.AddWithValue("@tax", txtTax.Text.Trim());
            cmd.Parameters.AddWithValue("@total", txtTotal.Text.Trim());
            cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem!.ToString());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Invoice added! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadBilling();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedBillingID == -1)
            {
                MessageBox.Show("Select an invoice first!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cust = (CustomerItem)cmbCustomer.SelectedItem!;
            var ship = cmbShipment.SelectedItem as ShipmentItem;

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(@"
                UPDATE Billing SET
                CustomerID=@cid, ShipmentID=@sid,
                InvoiceDate=@date, Amount=@amount,
                Tax=@tax, TotalAmount=@total,
                PaymentStatus=@status
                WHERE BillingID=@id", con);

            cmd.Parameters.AddWithValue("@cid", cust.ID);
            cmd.Parameters.AddWithValue("@sid", ship?.ID ?? 0);
            cmd.Parameters.AddWithValue("@date", dtpInvoiceDate.Value
                                                   .ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@amount", txtAmount.Text.Trim());
            cmd.Parameters.AddWithValue("@tax", txtTax.Text.Trim());
            cmd.Parameters.AddWithValue("@total", txtTotal.Text.Trim());
            cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem!.ToString());
            cmd.Parameters.AddWithValue("@id", selectedBillingID);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Invoice updated! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadBilling();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedBillingID == -1)
            {
                MessageBox.Show("Select an invoice to delete!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this invoice?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "DELETE FROM Billing WHERE BillingID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedBillingID);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Invoice deleted! 🗑️", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadBilling();
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            selectedBillingID = -1;
            cmbCustomer.SelectedIndex = 0;
            cmbShipment.SelectedIndex = 0;
            cmbStatus.SelectedIndex = 0;
            txtAmount.Text = "";
            txtTax.Text = "5";
            txtTotal.Text = "0.00";
            txtSearch.Text = "";
            dtpInvoiceDate.Value = DateTime.Today;
        }
    }
}