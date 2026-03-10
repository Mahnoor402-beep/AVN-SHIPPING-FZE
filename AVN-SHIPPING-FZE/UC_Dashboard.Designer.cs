using System.Drawing.Drawing2D;

namespace AVN_SHIPPING_FZE
{
    partial class UC_Dashboard
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
            this.SuspendLayout();

            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(245, 246, 250);
            this.Dock = DockStyle.Fill;
            this.AutoScroll = true;
            this.Name = "UC_Dashboard";
            this.Resize += (s, e) => LayoutControls();

            this.ResumeLayout(false);
        }

        private Label lblTitle, lblSubtitle;
        private Panel pnlCard1, pnlCard2, pnlCard3, pnlCard4;
        private Label lblCustCount, lblShipCount, lblJobCount, lblBillCount;
        private Panel pnlChart;
        private Label lblChartTitle;
    }
}