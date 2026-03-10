using Microsoft.Data.Sqlite;
using System.Data;

namespace AVN_SHIPPING_FZE
{
    public partial class UC_Shipments : UserControl
    {
        private int selectedShipmentID = -1;

        public UC_Shipments()
        {
            InitializeComponent();
            LoadCustomersDropdown();
            LoadShipments();
            txtSearch.TextChanged += (s, e) => LoadShipments(txtSearch.Text);
        }

        // ── LOAD CUSTOMERS INTO DROPDOWN ──────────
        private void LoadCustomersDropdown()
        {
            cmbCustomer.Items.Clear();
            cmbCustomer.Items.Add(new CustomerItem(0, "-- Select Customer --"));

            using var con = DatabaseHelper.GetConnection();
            con.Open();

            using var cmd = new SqliteCommand(
                "SELECT CustomerID, FullName, CompanyName FROM Customers ORDER BY FullName", con);
            using var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                string display = reader["FullName"].ToString()!;
                if (!string.IsNullOrEmpty(reader["CompanyName"].ToString()))
                    display += $" ({reader["CompanyName"]})";

                cmbCustomer.Items.Add(new CustomerItem(
                    Convert.ToInt32(reader["CustomerID"]), display));
            }
            cmbCustomer.SelectedIndex = 0;
        }

        // ── LOAD SHIPMENTS INTO GRID ──────────────
        private void LoadShipments(string search = "")
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();

                string query = @"SELECT s.ShipmentID, c.FullName as Customer,
                                s.Origin, s.Destination, s.ShipmentDate,
                                s.Weight, s.Status, s.Description
                                FROM Shipments s
                                LEFT JOIN Customers c ON s.CustomerID = c.CustomerID
                                WHERE c.FullName LIKE @search
                                OR s.Origin LIKE @search
                                OR s.Destination LIKE @search
                                OR s.Status LIKE @search
                                ORDER BY s.ShipmentID DESC";

                using var cmd = new SqliteCommand(query, con);
                cmd.Parameters.AddWithValue("@search", $"%{search}%");

                var table = new DataTable();
                table.Columns.Add("ShipmentID");
                table.Columns.Add("Customer");
                table.Columns.Add("Origin");
                table.Columns.Add("Destination");
                table.Columns.Add("ShipmentDate");
                table.Columns.Add("Weight");
                table.Columns.Add("Status");
                table.Columns.Add("Description");

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    table.Rows.Add(
                        reader["ShipmentID"].ToString(),
                        reader["Customer"].ToString(),
                        reader["Origin"].ToString(),
                        reader["Destination"].ToString(),
                        reader["ShipmentDate"].ToString(),
                        reader["Weight"].ToString(),
                        reader["Status"].ToString(),
                        reader["Description"].ToString()
                    );
                }

                dgvShipments.DataSource = null;
                dgvShipments.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading shipments: " + ex.Message);
            }
        }

        // ── CLICK ROW → FILL FORM ─────────────────
        private void dgvShipments_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvShipments.Rows[e.RowIndex];
            selectedShipmentID = Convert.ToInt32(row.Cells["ShipmentID"].Value);

            // Find and select the correct customer in dropdown
            foreach (CustomerItem item in cmbCustomer.Items)
            {
                if (item.Name == row.Cells["Customer"].Value?.ToString())
                {
                    cmbCustomer.SelectedItem = item;
                    break;
                }
            }

            txtOrigin.Text = row.Cells["Origin"].Value?.ToString();
            txtDestination.Text = row.Cells["Destination"].Value?.ToString();
            txtShipDate.Text = row.Cells["ShipmentDate"].Value?.ToString();
            txtWeight.Text = row.Cells["Weight"].Value?.ToString();
            txtDescription.Text = row.Cells["Description"].Value?.ToString();

            // Set status dropdown
            string status = row.Cells["Status"].Value?.ToString() ?? "Pending";
            int idx = cmbStatus.Items.IndexOf(status);
            if (idx >= 0) cmbStatus.SelectedIndex = idx;
        }

        // ── ADD SHIPMENT ──────────────────────────
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedIndex == 0)
            {
                MessageBox.Show("Please select a customer!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtOrigin.Text) ||
                string.IsNullOrWhiteSpace(txtDestination.Text))
            {
                MessageBox.Show("Origin and Destination are required!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customer = (CustomerItem)cmbCustomer.SelectedItem!;

            using var con = DatabaseHelper.GetConnection();
            con.Open();

            using var cmd = new SqliteCommand(@"INSERT INTO Shipments
                (CustomerID, Origin, Destination, ShipmentDate, 
                 Weight, Status, Description)
                VALUES (@cid, @origin, @dest, @date, 
                        @weight, @status, @desc)", con);

            cmd.Parameters.AddWithValue("@cid", customer.ID);
            cmd.Parameters.AddWithValue("@origin", txtOrigin.Text.Trim());
            cmd.Parameters.AddWithValue("@dest", txtDestination.Text.Trim());
            cmd.Parameters.AddWithValue("@date", txtShipDate.Text.Trim());
            cmd.Parameters.AddWithValue("@weight", txtWeight.Text.Trim());
            cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem!.ToString());
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Shipment added! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadShipments();
        }

        // ── UPDATE SHIPMENT ───────────────────────
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedShipmentID == -1)
            {
                MessageBox.Show("Please select a shipment first!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var customer = (CustomerItem)cmbCustomer.SelectedItem!;

            using var con = DatabaseHelper.GetConnection();
            con.Open();

            using var cmd = new SqliteCommand(@"UPDATE Shipments SET
                CustomerID=@cid, Origin=@origin, Destination=@dest,
                ShipmentDate=@date, Weight=@weight,
                Status=@status, Description=@desc
                WHERE ShipmentID=@id", con);

            cmd.Parameters.AddWithValue("@cid", customer.ID);
            cmd.Parameters.AddWithValue("@origin", txtOrigin.Text.Trim());
            cmd.Parameters.AddWithValue("@dest", txtDestination.Text.Trim());
            cmd.Parameters.AddWithValue("@date", txtShipDate.Text.Trim());
            cmd.Parameters.AddWithValue("@weight", txtWeight.Text.Trim());
            cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem!.ToString());
            cmd.Parameters.AddWithValue("@desc", txtDescription.Text.Trim());
            cmd.Parameters.AddWithValue("@id", selectedShipmentID);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Shipment updated! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadShipments();
        }

        // ── DELETE SHIPMENT ───────────────────────
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedShipmentID == -1)
            {
                MessageBox.Show("Please select a shipment to delete!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "DELETE FROM Shipments WHERE ShipmentID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedShipmentID);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Shipment deleted! 🗑️", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadShipments();
            }
        }

        // ── CLEAR FORM ────────────────────────────
        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            selectedShipmentID = -1;
            cmbCustomer.SelectedIndex = 0;
            txtOrigin.Text = "";
            txtDestination.Text = "";
            txtShipDate.Text = DateTime.Today.ToString("yyyy-MM-dd");
            txtWeight.Text = "";
            txtDescription.Text = "";
            txtSearch.Text = "";
            cmbStatus.SelectedIndex = 0;
        }
    }

    // ── HELPER CLASS FOR CUSTOMER DROPDOWN ────────
    public class CustomerItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public CustomerItem(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override string ToString() => Name;
    }
}