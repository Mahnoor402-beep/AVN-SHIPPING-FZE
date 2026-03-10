using Microsoft.Data.Sqlite;
using System.Data;

namespace AVN_SHIPPING_FZE
{
    public partial class UC_Customers : UserControl
    {
        private int selectedCustomerID = -1;

        public UC_Customers()
        {
            InitializeComponent();
            LoadCustomers();
            txtSearch.TextChanged += (s, e) => LoadCustomers(txtSearch.Text);
            dgvCustomers.CellClick += dgvCustomers_CellClick;
        }

        private void LoadCustomers(string search = "")
{
    try
    {
        using var con = DatabaseHelper.GetConnection();
        con.Open();

        string query = @"SELECT CustomerID, FullName, CompanyName, 
                         Phone, Email, Address 
                         FROM Customers
                         WHERE FullName LIKE @search 
                         OR CompanyName LIKE @search
                         OR Phone LIKE @search
                         ORDER BY FullName";

        using var cmd = new SqliteCommand(query, con);
        cmd.Parameters.AddWithValue("@search", $"%{search}%");

        var table = new DataTable();
        table.Columns.Add("CustomerID");
        table.Columns.Add("FullName");
        table.Columns.Add("CompanyName");
        table.Columns.Add("Phone");
        table.Columns.Add("Email");
        table.Columns.Add("Address");

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            table.Rows.Add(
                reader["CustomerID"].ToString(),
                reader["FullName"].ToString(),
                reader["CompanyName"].ToString(),
                reader["Phone"].ToString(),
                reader["Email"].ToString(),
                reader["Address"].ToString()
            );
        }

        // ✅ Assign data first
        dgvCustomers.DataSource = null;
        dgvCustomers.DataSource = table;
    }
    catch (Exception ex)
    {
        MessageBox.Show("Error loading customers: " + ex.Message);
    }
}
        private void dgvCustomers_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvCustomers.Rows[e.RowIndex];

            selectedCustomerID = Convert.ToInt32(row.Cells["CustomerID"].Value);
            txtFullName.Text = row.Cells["FullName"].Value?.ToString() ?? "";
            txtCompany.Text = row.Cells["CompanyName"].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells["Phone"].Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            txtAddress.Text = row.Cells["Address"].Value?.ToString() ?? "";
            txtTRN.Text = row.Cells["TRN"].Value?.ToString() ?? "";  // ← ADD THIS
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Full Name is required!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var con = DatabaseHelper.GetConnection();
            con.Open();

            using var cmd = new SqliteCommand(
      "SELECT CustomerID, FullName, CompanyName, Phone, Email, Address, TRN FROM Customers ORDER BY FullName", con);
            cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
            cmd.Parameters.AddWithValue("@company", txtCompany.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@trn", txtTRN.Text.Trim());
            cmd.Parameters.AddWithValue("@date", DateTime.Today.ToString("yyyy-MM-dd"));

            MessageBox.Show("Customer added! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadCustomers();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedCustomerID == -1)
            {
                MessageBox.Show("Please select a customer first!",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var con = DatabaseHelper.GetConnection();
            con.Open();

            using var cmd = new SqliteCommand(@"UPDATE Customers SET
                FullName=@name, CompanyName=@company, Phone=@phone,
                Email=@email, Address=@address
                WHERE CustomerID=@id", con);

            cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
            cmd.Parameters.AddWithValue("@company", txtCompany.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
            cmd.Parameters.AddWithValue("@id", selectedCustomerID);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Customer updated! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadCustomers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedCustomerID == -1)
            {
                MessageBox.Show("Please select a customer to delete!",
                    "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "DELETE FROM Customers WHERE CustomerID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedCustomerID);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Customer deleted! 🗑️", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadCustomers();
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            selectedCustomerID = -1;
            txtFullName.Text = "";
            txtCompany.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
            txtSearch.Text = "";
            txtTRN.Text = "";

        }

        private void txtFullName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}