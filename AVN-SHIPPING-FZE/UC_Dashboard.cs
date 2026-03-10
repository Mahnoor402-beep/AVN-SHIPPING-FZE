using Microsoft.Data.Sqlite;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace AVN_SHIPPING_FZE
{
    public partial class UC_Dashboard : UserControl
    {
        private double[] monthlyData = new double[12];

        public UC_Dashboard()
        {
            InitializeComponent();
            BuildUI();
            this.Load += (s, e) => LayoutControls();
        }

        private void BuildUI()
        {
            // ── TITLE ─────────────────────────────
            lblTitle = new Label
            {
                Text = "Dashboard",
                Font = new Font("Arial", 24F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 25),
                AutoSize = true
            };

            lblSubtitle = new Label
            {
                Text = $"Welcome back!   |   {DateTime.Now:dddd, dd MMMM yyyy}",
                Font = new Font("Arial", 10F),
                ForeColor = Color.Gray,
                AutoSize = true
            };

            // ── CARDS ─────────────────────────────
            lblCustCount = new Label();
            lblShipCount = new Label();
            lblJobCount = new Label();
            lblBillCount = new Label();

            pnlCard1 = MakeCard("👤  Total Customers",
                lblCustCount, Color.FromArgb(0, 180, 120));
            pnlCard2 = MakeCard("📦  Total Shipments",
                lblShipCount, Color.FromArgb(0, 120, 220));
            pnlCard3 = MakeCard("🧾  Total Job Cards",
                lblJobCount, Color.FromArgb(255, 140, 0));
            pnlCard4 = MakeCard("💰  Unpaid Invoices",
                lblBillCount, Color.FromArgb(220, 50, 50));

            // ── CHART TITLE ───────────────────────
            lblChartTitle = new Label
            {
                Text = "Monthly Revenue (AED)  —  " + DateTime.Now.Year,
                Font = new Font("Arial", 13F, FontStyle.Bold),
                ForeColor = Color.FromArgb(25, 25, 25),
                AutoSize = true
            };

            // ── CHART PANEL ───────────────────────
            pnlChart = new Panel
            {
                BackColor = Color.White
            };
            pnlChart.Paint += PnlChart_Paint;
            pnlChart.Paint += PaintRoundedBorder;

            // Add all controls
            this.Controls.AddRange(new Control[]
            {
                lblTitle, lblSubtitle,
                pnlCard1, pnlCard2, pnlCard3, pnlCard4,
                lblChartTitle, pnlChart
            });
        }

        private Panel MakeCard(string title, Label countLabel, Color accent)
        {
            var card = new Panel { BackColor = Color.White };

            var lblT = new Label
            {
                Text = title,
                Font = new Font("Arial", 10F),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(20, 18)
            };

            countLabel.Text = "—";
            countLabel.Font = new Font("Arial", 36F, FontStyle.Bold);
            countLabel.ForeColor = Color.FromArgb(25, 25, 25);
            countLabel.AutoSize = true;
            countLabel.Location = new Point(18, 48);

            card.Controls.Add(lblT);
            card.Controls.Add(countLabel);

            // Draw rounded border + accent bar
            card.Paint += (s, e) =>
            {
                var g = e.Graphics;
                g.SmoothingMode = SmoothingMode.AntiAlias;
                var rect = new Rectangle(0, 0, card.Width - 1, card.Height - 1);
                using var path = RoundedPath(rect, 12);
                g.FillPath(new SolidBrush(Color.White), path);
                g.DrawPath(new Pen(Color.FromArgb(220, 220, 220), 1), path);
                g.FillRectangle(new SolidBrush(accent),
                    new Rectangle(0, 0, 6, card.Height));
            };

            return card;
        }

        // Layout everything centered based on current width
        private void LayoutControls()
        {
            int w = this.ClientSize.Width;
            int margin = 40;
            int usable = w - margin * 2;
            int cardW = (usable - 30 * 3) / 4;  // 4 cards with 3 gaps
            int cardH = 130;
            int startY = 30;

            lblTitle.Location = new Point(margin, startY);
            lblSubtitle.Location = new Point(margin + 3, startY + 55);

            // Cards row
            int cardY = startY + 100;
            pnlCard1.SetBounds(margin, cardY, cardW, cardH);
            pnlCard2.SetBounds(margin + (cardW + 30), cardY, cardW, cardH);
            pnlCard3.SetBounds(margin + (cardW + 30) * 2, cardY, cardW, cardH);
            pnlCard4.SetBounds(margin + (cardW + 30) * 3, cardY, cardW, cardH);

            // Chart
            int chartY = cardY + cardH + 30;
            lblChartTitle.Location = new Point(margin, chartY);

            pnlChart.SetBounds(margin, chartY + 35,
                usable, this.ClientSize.Height - chartY - 70);

            this.Refresh();
        }

        private void PnlChart_Paint(object? sender, PaintEventArgs e)
        {
            if (monthlyData == null) return;

            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            int padL = 60, padB = 40, padT = 20, padR = 20;
            int chartW = pnlChart.Width - padL - padR;
            int chartH = pnlChart.Height - padT - padB;

            if (chartW <= 0 || chartH <= 0) return;

            double maxVal = monthlyData.Max() == 0 ? 1000 : monthlyData.Max() * 1.2;

            string[] months = { "Jan","Feb","Mar","Apr","May","Jun",
                                 "Jul","Aug","Sep","Oct","Nov","Dec" };

            var gridPen = new Pen(Color.FromArgb(235, 235, 235), 1);
            var textBrush = new SolidBrush(Color.FromArgb(160, 160, 160));
            var font = new Font("Arial", 8F);
            var boldFont = new Font("Arial", 8F, FontStyle.Bold);

            // Grid lines + Y labels
            for (int i = 0; i <= 4; i++)
            {
                int y = padT + chartH - (chartH * i / 4);
                g.DrawLine(gridPen, padL, y, padL + chartW, y);
                double val = maxVal * i / 4;
                string label = val >= 1000
                    ? $"{val / 1000:F0}K" : $"{val:F0}";
                g.DrawString(label, font, textBrush,
                    new RectangleF(0, y - 10, padL - 5, 20),
                    new StringFormat
                    {
                        Alignment = StringAlignment.Far,
                        LineAlignment = StringAlignment.Center
                    });
            }

            // Bars
            int totalBars = 12;
            float barW = (float)chartW / totalBars;
            float barPad = barW * 0.25f;

            for (int i = 0; i < 12; i++)
            {
                float x = padL + i * barW + barPad;
                float bw = barW - barPad * 2;
                float barH = monthlyData[i] == 0 ? 3
                    : (float)(chartH * monthlyData[i] / maxVal);
                float y = padT + chartH - barH;

                // BG bar
                g.FillRectangle(
                    new SolidBrush(Color.FromArgb(230, 248, 240)),
                    x, padT, bw, chartH);

                // Value bar with gradient
                if (barH > 3)
                {
                    using var grad = new LinearGradientBrush(
                        new PointF(x, y),
                        new PointF(x, padT + chartH),
                        Color.FromArgb(0, 200, 140),
                        Color.FromArgb(0, 140, 100));
                    g.FillRectangle(grad, x, y, bw, barH);
                }

                // Value on top
                if (monthlyData[i] > 0)
                {
                    string val = monthlyData[i] >= 1000
                        ? $"{monthlyData[i] / 1000:F1}K"
                        : $"{monthlyData[i]:F0}";
                    g.DrawString(val, boldFont,
                        new SolidBrush(Color.FromArgb(30, 30, 30)),
                        new RectangleF(x - 5, y - 18, bw + 10, 16),
                        new StringFormat
                        { Alignment = StringAlignment.Center });
                }

                // Month label
                g.DrawString(months[i], font, textBrush,
                    new RectangleF(x - 5,
                        padT + chartH + 5, bw + 10, 20),
                    new StringFormat
                    { Alignment = StringAlignment.Center });
            }

            // X axis line
            g.DrawLine(new Pen(Color.FromArgb(200, 200, 200), 1),
                padL, padT + chartH, padL + chartW, padT + chartH);
        }

        private void PaintRoundedBorder(object? sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            if (p == null) return;
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
            using var path = RoundedPath(rect, 12);
            g.DrawPath(new Pen(Color.FromArgb(220, 220, 220), 1), path);
        }

        private static GraphicsPath RoundedPath(Rectangle b, int r)
        {
            var path = new GraphicsPath();
            path.AddArc(b.X, b.Y, r * 2, r * 2, 180, 90);
            path.AddArc(b.Right - r * 2, b.Y, r * 2, r * 2, 270, 90);
            path.AddArc(b.Right - r * 2, b.Bottom - r * 2, r * 2, r * 2, 0, 90);
            path.AddArc(b.X, b.Bottom - r * 2, r * 2, r * 2, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (Visible) LoadDashboard();
        }

        private void LoadDashboard()
        {
            try
            {
                using var con = DatabaseHelper.GetConnection();
                con.Open();

                lblCustCount.Text = GetCount(con,
                    "SELECT COUNT(*) FROM Customers");
                lblShipCount.Text = GetCount(con,
                    "SELECT COUNT(*) FROM Shipments");
                lblJobCount.Text = GetCount(con,
                    "SELECT COUNT(*) FROM JobCards");
                lblBillCount.Text = GetCount(con,
                    "SELECT COUNT(*) FROM Billing WHERE PaymentStatus='Unpaid'");

                // Monthly revenue
                monthlyData = new double[12];
                using var cmd = new SqliteCommand(@"
                    SELECT strftime('%m', InvoiceDate) as Month,
                           SUM(CAST(TotalAmount as REAL)) as Total
                    FROM Billing
                    WHERE strftime('%Y', InvoiceDate) = strftime('%Y','now')
                    GROUP BY Month", con);
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int m = int.Parse(reader["Month"].ToString()!);
                    monthlyData[m - 1] = Convert.ToDouble(reader["Total"]);
                }

                LayoutControls();
                pnlChart.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dashboard error: " + ex.Message);
            }
        }

        private string GetCount(SqliteConnection con, string sql)
        {
            using var cmd = new SqliteCommand(sql, con);
            return cmd.ExecuteScalar()?.ToString() ?? "0";
        }
    }
}