namespace AVN_SHIPPING_FZE
{
    partial class UC_Employees
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
            lblName = new Label();
            lblRole = new Label();
            lblPhone = new Label();
            lblEmail = new Label();
            lblJoinDate = new Label();
            lblSalary = new Label();
            lblSearch = new Label();
            txtName = new TextBox();
            txtRole = new TextBox();
            txtPhone = new TextBox();
            txtEmail = new TextBox();
            txtSalary = new TextBox();
            txtSearch = new TextBox();
            dtpJoinDate = new DateTimePicker();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            dgvEmployees = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Arial", 16F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 51, 102);
            lblTitle.Location = new Point(20, 15);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(264, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "👷  EMPLOYEES";
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblName.Location = new Point(20, 70);
            lblName.Name = "lblName";
            lblName.Size = new Size(110, 21);
            lblName.TabIndex = 1;
            lblName.Text = "Full Name *";
            // 
            // lblRole
            // 
            lblRole.AutoSize = true;
            lblRole.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblRole.Location = new Point(400, 70);
            lblRole.Name = "lblRole";
            lblRole.Size = new Size(50, 21);
            lblRole.TabIndex = 2;
            lblRole.Text = "Role";
            // 
            // lblPhone
            // 
            lblPhone.AutoSize = true;
            lblPhone.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblPhone.Location = new Point(20, 115);
            lblPhone.Name = "lblPhone";
            lblPhone.Size = new Size(66, 21);
            lblPhone.TabIndex = 3;
            lblPhone.Text = "Phone";
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblEmail.Location = new Point(400, 115);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(59, 21);
            lblEmail.TabIndex = 4;
            lblEmail.Text = "Email";
            // 
            // lblJoinDate
            // 
            lblJoinDate.AutoSize = true;
            lblJoinDate.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblJoinDate.Location = new Point(20, 160);
            lblJoinDate.Name = "lblJoinDate";
            lblJoinDate.Size = new Size(92, 21);
            lblJoinDate.TabIndex = 5;
            lblJoinDate.Text = "Join Date";
            // 
            // lblSalary
            // 
            lblSalary.AutoSize = true;
            lblSalary.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblSalary.Location = new Point(400, 160);
            lblSalary.Name = "lblSalary";
            lblSalary.Size = new Size(120, 21);
            lblSalary.TabIndex = 6;
            lblSalary.Text = "Salary (AED)";
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Arial", 9F, FontStyle.Bold);
            lblSearch.Location = new Point(20, 260);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(105, 21);
            lblSearch.TabIndex = 7;
            lblSearch.Text = "🔍 Search:";
            // 
            // txtName
            // 
            txtName.Font = new Font("Arial", 9F);
            txtName.Location = new Point(130, 67);
            txtName.Name = "txtName";
            txtName.Size = new Size(220, 28);
            txtName.TabIndex = 8;
            // 
            // txtRole
            // 
            txtRole.Font = new Font("Arial", 9F);
            txtRole.Location = new Point(480, 67);
            txtRole.Name = "txtRole";
            txtRole.Size = new Size(220, 28);
            txtRole.TabIndex = 9;
            // 
            // txtPhone
            // 
            txtPhone.Font = new Font("Arial", 9F);
            txtPhone.Location = new Point(130, 112);
            txtPhone.Name = "txtPhone";
            txtPhone.Size = new Size(220, 28);
            txtPhone.TabIndex = 10;
            // 
            // txtEmail
            // 
            txtEmail.Font = new Font("Arial", 9F);
            txtEmail.Location = new Point(480, 112);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(220, 28);
            txtEmail.TabIndex = 11;
            // 
            // txtSalary
            // 
            txtSalary.Font = new Font("Arial", 9F);
            txtSalary.Location = new Point(480, 157);
            txtSalary.Name = "txtSalary";
            txtSalary.Size = new Size(220, 28);
            txtSalary.TabIndex = 12;
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Arial", 9F);
            txtSearch.Location = new Point(130, 257);
            txtSearch.Name = "txtSearch";
            txtSearch.Size = new Size(250, 28);
            txtSearch.TabIndex = 13;
            // 
            // dtpJoinDate
            // 
            dtpJoinDate.Font = new Font("Arial", 9F);
            dtpJoinDate.Format = DateTimePickerFormat.Short;
            dtpJoinDate.Location = new Point(130, 157);
            dtpJoinDate.Name = "dtpJoinDate";
            dtpJoinDate.Size = new Size(220, 28);
            dtpJoinDate.TabIndex = 14;
            dtpJoinDate.Value = new DateTime(2026, 3, 9, 0, 0, 0, 0);
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(0, 150, 0);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 210);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(110, 35);
            btnAdd.TabIndex = 15;
            btnAdd.Text = "➕ Add";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(0, 102, 204);
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(140, 210);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(110, 35);
            btnUpdate.TabIndex = 16;
            btnUpdate.Text = "✏️ Update";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(200, 0, 0);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(260, 210);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(110, 35);
            btnDelete.TabIndex = 17;
            btnDelete.Text = "🗑️ Delete";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(100, 100, 100);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Arial", 9F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(380, 210);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(110, 35);
            btnClear.TabIndex = 18;
            btnClear.Text = "🔄 Clear";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // dgvEmployees
            // 
            dgvEmployees.AllowUserToAddRows = false;
            dgvEmployees.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEmployees.BackgroundColor = Color.White;
            dgvEmployees.BorderStyle = BorderStyle.None;
            dgvEmployees.ColumnHeadersHeight = 34;
            dgvEmployees.Font = new Font("Arial", 9F);
            dgvEmployees.Location = new Point(20, 295);
            dgvEmployees.MultiSelect = false;
            dgvEmployees.Name = "dgvEmployees";
            dgvEmployees.ReadOnly = true;
            dgvEmployees.RowHeadersVisible = false;
            dgvEmployees.RowHeadersWidth = 62;
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployees.Size = new Size(1226, 72);
            dgvEmployees.TabIndex = 19;
            dgvEmployees.CellClick += dgvEmployees_CellClick;
            // 
            // UC_Employees
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            BackColor = Color.FromArgb(240, 242, 245);
            Controls.Add(lblTitle);
            Controls.Add(lblName);
            Controls.Add(lblRole);
            Controls.Add(lblPhone);
            Controls.Add(lblEmail);
            Controls.Add(lblJoinDate);
            Controls.Add(lblSalary);
            Controls.Add(lblSearch);
            Controls.Add(txtName);
            Controls.Add(txtRole);
            Controls.Add(txtPhone);
            Controls.Add(txtEmail);
            Controls.Add(txtSalary);
            Controls.Add(txtSearch);
            Controls.Add(dtpJoinDate);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(btnClear);
            Controls.Add(dgvEmployees);
            Name = "UC_Employees";
            Size = new Size(1326, 472);
            ((System.ComponentModel.ISupportInitialize)dgvEmployees).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblTitle, lblName, lblRole, lblPhone;
        private Label lblEmail, lblJoinDate, lblSalary, lblSearch;
        private TextBox txtName, txtRole, txtPhone;
        private TextBox txtEmail, txtSalary, txtSearch;
        private DateTimePicker dtpJoinDate;
        private Button btnAdd, btnUpdate, btnDelete, btnClear;
        private DataGridView dgvEmployees;
    }
}