using Microsoft.Data.Sqlite;
using System.Data;

namespace AVN_SHIPPING_FZE
{
    public partial class UC_Employees : UserControl
    {
        private int selectedEmployeeID = -1;

        public UC_Employees()
        {
            InitializeComponent();
            LoadEmployees();
            txtSearch.TextChanged += (s, e) => LoadEmployees(txtSearch.Text);
        }

        private void LoadEmployees(string search = "")
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();

                using var cmd = new SqliteCommand(@"
                    SELECT EmployeeID, FullName, Role,
                           Phone, Email, JoinDate, Salary
                    FROM Employees
                    WHERE FullName LIKE @s
                    OR Role LIKE @s
                    OR Phone LIKE @s
                    ORDER BY FullName", con);
                cmd.Parameters.AddWithValue("@s", $"%{search}%");

                var table = new DataTable();
                table.Columns.Add("EmployeeID");
                table.Columns.Add("Full Name");
                table.Columns.Add("Role");
                table.Columns.Add("Phone");
                table.Columns.Add("Email");
                table.Columns.Add("Join Date");
                table.Columns.Add("Salary");

                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                    table.Rows.Add(
                        reader["EmployeeID"].ToString(),
                        reader["FullName"].ToString(),
                        reader["Role"].ToString(),
                        reader["Phone"].ToString(),
                        reader["Email"].ToString(),
                        reader["JoinDate"].ToString(),
                        reader["Salary"].ToString()
                    );

                dgvEmployees.DataSource = null;
                dgvEmployees.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void dgvEmployees_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var row = dgvEmployees.Rows[e.RowIndex];

            selectedEmployeeID = Convert.ToInt32(row.Cells["EmployeeID"].Value);
            txtName.Text = row.Cells["Full Name"].Value?.ToString();
            txtRole.Text = row.Cells["Role"].Value?.ToString();
            txtPhone.Text = row.Cells["Phone"].Value?.ToString();
            txtEmail.Text = row.Cells["Email"].Value?.ToString();
            txtSalary.Text = row.Cells["Salary"].Value?.ToString();

            if (DateTime.TryParse(
                row.Cells["Join Date"].Value?.ToString(), out DateTime d))
                dtpJoinDate.Value = d;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Full Name is required!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(@"
                INSERT INTO Employees
                (FullName, Role, Phone, Email, JoinDate, Salary)
                VALUES
                (@name, @role, @phone, @email, @join, @salary)", con);

            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@role", txtRole.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@join", dtpJoinDate.Value
                                                   .ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@salary", txtSalary.Text.Trim());
            cmd.ExecuteNonQuery();

            MessageBox.Show("Employee added! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadEmployees();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeID == -1)
            {
                MessageBox.Show("Select an employee first!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(@"
                UPDATE Employees SET
                FullName=@name, Role=@role, Phone=@phone,
                Email=@email, JoinDate=@join, Salary=@salary
                WHERE EmployeeID=@id", con);

            cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
            cmd.Parameters.AddWithValue("@role", txtRole.Text.Trim());
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@join", dtpJoinDate.Value
                                                   .ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@salary", txtSalary.Text.Trim());
            cmd.Parameters.AddWithValue("@id", selectedEmployeeID);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Employee updated! ✅", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClearForm();
            LoadEmployees();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedEmployeeID == -1)
            {
                MessageBox.Show("Select an employee to delete!", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Delete this employee?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();
                using var cmd = new SqliteCommand(
                    "DELETE FROM Employees WHERE EmployeeID=@id", con);
                cmd.Parameters.AddWithValue("@id", selectedEmployeeID);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Employee deleted! 🗑️", "Deleted",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearForm();
                LoadEmployees();
            }
        }

        private void btnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            selectedEmployeeID = -1;
            txtName.Text = "";
            txtRole.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtSalary.Text = "";
            txtSearch.Text = "";
            dtpJoinDate.Value = DateTime.Today;
        }
    }
}