using Microsoft.Data.Sqlite;

namespace AVN_SHIPPING_FZE
{
    public partial class ReminderForm : Form
    {
        private int jobCardID;
        private string jobCardNo;

        private Label lblEventType, lblEventDate, lblReminderDate;
        private Label lblEmail, lblPriority, lblNotes;
        private ComboBox cmbEventType, cmbPriority;
        private TextBox txtEventDate, txtReminderDate;
        private TextBox txtEmail, txtNotes;
        private Button btnSave, btnCancel;

        public ReminderForm(string jcNo, int jcID)
        {
            jobCardNo = jcNo;
            jobCardID = jcID;
            InitializeReminderForm();
        }

        private void InitializeReminderForm()
        {
            Text = $"🔔 Add Reminder — {jobCardNo}";
            Size = new Size(450, 380);
            StartPosition = FormStartPosition.CenterParent;
            BackColor = Color.FromArgb(240, 240, 240);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            void SL(Label l, string t, int y)
            {
                l.Text = t; l.Location = new Point(20, y);
                l.AutoSize = true; l.Font = new Font("Arial", 9F, FontStyle.Bold);
            }

            lblEventType = new Label(); SL(lblEventType, "Event Type *", 20);
            lblEventDate = new Label(); SL(lblEventDate, "Event Date", 65);
            lblReminderDate = new Label(); SL(lblReminderDate, "Reminder Date *", 110);
            lblEmail = new Label(); SL(lblEmail, "Recipient Email *", 155);
            lblPriority = new Label(); SL(lblPriority, "Priority", 200);
            lblNotes = new Label(); SL(lblNotes, "Notes", 245);

            cmbEventType = new ComboBox
            {
                Location = new Point(160, 17),
                Size = new Size(250, 24),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbEventType.Items.AddRange(new string[]
            {
                "ETD", "ETA", "Booking Release", "VGM Submit",
                "SI Submit", "Stuffing", "Cargo Carting",
                "Document Delivery", "BL Release",
                "Custom Clearance", "Shipment Handover", "Other"
            });
            cmbEventType.SelectedIndex = 0;

            txtEventDate = new TextBox
            {
                Location = new Point(160, 62),
                Size = new Size(250, 24),
                Text = DateTime.Today.ToString("yyyy-MM-dd")
            };

            txtReminderDate = new TextBox
            {
                Location = new Point(160, 107),
                Size = new Size(250, 24),
                Text = DateTime.Today.ToString("yyyy-MM-dd")
            };

            txtEmail = new TextBox
            {
                Location = new Point(160, 152),
                Size = new Size(250, 24),
                PlaceholderText = "email@example.com"
            };

            cmbPriority = new ComboBox
            {
                Location = new Point(160, 197),
                Size = new Size(250, 24),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPriority.Items.AddRange(new string[] { "Low", "Normal", "High", "Urgent" });
            cmbPriority.SelectedIndex = 1;

            txtNotes = new TextBox
            {
                Location = new Point(160, 242),
                Size = new Size(250, 24)
            };

            btnSave = new Button
            {
                Text = "💾 Save Reminder",
                Location = new Point(80, 290),
                Size = new Size(150, 35),
                BackColor = Color.FromArgb(0, 150, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Arial", 9F, FontStyle.Bold)
            };
            btnSave.Click += BtnSave_Click;

            btnCancel = new Button
            {
                Text = "Cancel",
                Location = new Point(245, 290),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(150, 0, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += (s, e) => { DialogResult = DialogResult.Cancel; Close(); };

            Controls.AddRange(new Control[]
            {
                lblEventType, cmbEventType,
                lblEventDate, txtEventDate,
                lblReminderDate, txtReminderDate,
                lblEmail, txtEmail,
                lblPriority, cmbPriority,
                lblNotes, txtNotes,
                btnSave, btnCancel
            });
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Recipient email is required!", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using var con = DatabaseHelper.GetConnection();
            con.Open();

            using var cmd = new SqliteCommand(@"INSERT INTO Reminders
                (JobCardID, JobCardNo, EventType, EventDate,
                 ReminderDate, Status, EmailSent,
                 RecipientEmail, Priority, Notes)
                VALUES (@jcid, @jcno, @evtype, @evdate,
                        @remdate, 'Pending', 0,
                        @email, @priority, @notes)", con);

            cmd.Parameters.AddWithValue("@jcid", jobCardID);
            cmd.Parameters.AddWithValue("@jcno", jobCardNo);
            cmd.Parameters.AddWithValue("@evtype", cmbEventType.SelectedItem?.ToString());
            cmd.Parameters.AddWithValue("@evdate", txtEventDate.Text.Trim());
            cmd.Parameters.AddWithValue("@remdate", txtReminderDate.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
            cmd.Parameters.AddWithValue("@priority", cmbPriority.SelectedItem?.ToString());
            cmd.Parameters.AddWithValue("@notes", txtNotes.Text.Trim());
            cmd.ExecuteNonQuery();

            DialogResult = DialogResult.OK;
            Close();
        }
    }
}