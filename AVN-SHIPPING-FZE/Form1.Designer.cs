namespace AVN_SHIPPING_FZE
{
    partial class MainForm
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
            pnlHeader = new Panel();
            pnlSidebar = new Panel();
            pnlContent = new Panel();
            btnDashboard = new Button();
            btnCustomers = new Button();
            btnShipments = new Button();
            btnJobCard = new Button();
            btnBilling = new Button();
            btnEmployees = new Button();
            btnAccounts = new Button();
            label1 = new Label();

            pnlHeader.SuspendLayout();
            pnlSidebar.SuspendLayout();
            SuspendLayout();

            // ── HEADER ──────────────────────────────
            pnlHeader.BackColor = Color.FromArgb(0, 51, 102);
            pnlHeader.Controls.Add(label1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Size = new Size(1178, 60);
            pnlHeader.Name = "pnlHeader";

            // ── HEADER LABEL ─────────────────────────
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 16F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(220, 15);
            label1.Name = "label1";
            label1.Text = "🚢 AVN Shipping FZE — Management System";

            // ── SIDEBAR ──────────────────────────────
            pnlSidebar.BackColor = Color.FromArgb(21, 71, 121);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Width = 200;
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Controls.Add(btnEmployees);
            pnlSidebar.Controls.Add(btnAccounts);
            pnlSidebar.Controls.Add(btnBilling);
            pnlSidebar.Controls.Add(btnJobCard);
            pnlSidebar.Controls.Add(btnShipments);
            pnlSidebar.Controls.Add(btnCustomers);
            pnlSidebar.Controls.Add(btnDashboard);

            // ── BUTTONS ──────────────────────────────
            void StyleButton(Button btn, string text, string name)
            {
                btn.Dock = DockStyle.Top;
                btn.Height = 50;
                btn.FlatStyle = FlatStyle.Flat;
                btn.ForeColor = Color.White;
                btn.BackColor = Color.FromArgb(21, 71, 121);
                btn.Font = new Font("Arial", 10F);
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(10, 0, 0, 0);
                btn.Text = text;
                btn.Name = name;
                btn.FlatAppearance.BorderSize = 0;
            }

            StyleButton(btnDashboard, "🏠  Dashboard", "btnDashboard");
            StyleButton(btnCustomers, "👤  Customers", "btnCustomers");
            StyleButton(btnShipments, "📦  Shipments", "btnShipments");
            StyleButton(btnJobCard, "🧾  Job Cards", "btnJobCard");
            StyleButton(btnBilling, "💰  Billing", "btnBilling");
            StyleButton(btnEmployees, "👷  Employees", "btnEmployees");
            StyleButton(btnAccounts, "💰  Accounts", "btnAccounts");

            // Wire up button clicks - handled in MainForm.cs
            btnDashboard.Click += btnDashboard_Click;
            btnCustomers.Click += btnCustomers_Click;
            btnShipments.Click += btnShipments_Click;
            btnJobCard.Click += btnJobCard_Click;
            btnBilling.Click += btnBilling_Click;
            btnEmployees.Click += btnEmployees_Click;
            btnAccounts.Click += btnAccounts_Click;
            // ── CONTENT PANEL ────────────────────────
            pnlContent.BackColor = Color.FromArgb(240, 240, 240);
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Name = "pnlContent";

            // ── MAIN FORM ────────────────────────────
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1178, 644);
            WindowState = FormWindowState.Maximized;
            Text = "AVN Shipping FZE";
            Name = "MainForm";

            // ORDER MATTERS: Add in this sequence
            Controls.Add(pnlContent);   // Fill (added first)
            Controls.Add(pnlSidebar);   // Left
            Controls.Add(pnlHeader);    // Top

            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            pnlSidebar.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Panel pnlHeader;
        private Panel pnlSidebar;
        private Panel pnlContent;
        private Label label1;
        private Button btnDashboard;
        private Button btnCustomers;
        private Button btnShipments;
        private Button btnJobCard;
        private Button btnBilling;
        private Button btnEmployees;
        private Button btnAccounts;
    }
}