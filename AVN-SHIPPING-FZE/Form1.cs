namespace AVN_SHIPPING_FZE
{
    public partial class MainForm : Form
    {
        // Declare but don't create yet
        UC_Dashboard? ucDashboard = null;
        UC_Customers? ucCustomers = null;
        UC_Shipments? ucShipments = null;
        UC_JobCards? ucJobCards = null;
        UC_Billing? ucBilling = null;
        UC_Employees? ucEmployees = null;
        UC_Accounts? ucAccounts = null;
        public MainForm()
        {
            InitializeComponent();
        }

        // Loads any module safely
        private void LoadModule(UserControl module)
        {
            try
            {
                pnlContent.Controls.Clear();
                module.Dock = DockStyle.Fill;
                module.Visible = true;
                pnlContent.Controls.Add(module);
                module.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading module: " + ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ucDashboard = new UC_Dashboard();
            LoadModule(ucDashboard);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            ucDashboard ??= new UC_Dashboard();
            LoadModule(ucDashboard);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            ucCustomers ??= new UC_Customers();
            LoadModule(ucCustomers);
        }

        private void btnShipments_Click(object sender, EventArgs e)
        {
            ucShipments ??= new UC_Shipments();
            LoadModule(ucShipments);
        }

        private void btnJobCard_Click(object sender, EventArgs e)
        {
            ucJobCards ??= new UC_JobCards();
            LoadModule(ucJobCards);
        }

        private void btnBilling_Click(object sender, EventArgs e)
        {
            ucBilling ??= new UC_Billing();
            LoadModule(ucBilling);
        }

        private void btnEmployees_Click(object sender, EventArgs e)
        {
            ucEmployees ??= new UC_Employees();
            LoadModule(ucEmployees);
        }
        private void btnAccounts_Click(object sender, EventArgs e)
        {
            ucAccounts ??= new UC_Accounts();
            LoadModule(ucAccounts);
        }

    }
}