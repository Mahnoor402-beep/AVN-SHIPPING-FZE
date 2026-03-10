using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using Microsoft.Data.Sqlite;

namespace AVN_SHIPPING_FZE
{
    public class InvoicePDF
    {
        private static readonly string LogoPath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Resources", "AVN-LOGO.png");

        // Company details
        private const string CompanyName = "AVN SHIPPING F.Z.E";
        private const string CompanyAddress = "Sharjah Airport International Free Zone (SAIF Zone), Sharjah, UAE";
        private const string CompanyPhone = "+971 6 123 4567";
        private const string CompanyEmail = "info@avnshipping.com";
        private const string CompanyTRN = "100XXXXXXXXX00003";

        // Bank details from your template
        private const string BankHolder = "AVN SHIPPING-F.Z.E";
        private const string BankName = "Wio Bank P.J.S.C";
        private const string BankAccount = "9952484786";
        private const string BankIBAN = "AE150860000009952484786";
        private const string BankSwift = "WIOBAEADXXX";

        public static void Generate(int invoiceID, string outputPath)
        {
            // Load invoice data
            var inv = LoadInvoice(invoiceID);
            var items = LoadItems(invoiceID);
            var cust = LoadCustomer(Convert.ToInt32(inv["CustomerID"]));

            using var writer = new PdfWriter(outputPath);
            using var pdf = new PdfDocument(writer);
            using var document = new Document(pdf, PageSize.A4);
            document.SetMargins(20, 30, 20, 30);

            var fontBold = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            var fontNormal = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            var navyColor = new DeviceRgb(0, 51, 102);
            var lightGray = new DeviceRgb(245, 245, 245);
            var borderColor = new DeviceRgb(200, 200, 200);

            // ── HEADER ────────────────────────────
            var headerTable = new Table(UnitValue.CreatePercentArray(new float[] { 1, 2 }))
                .UseAllAvailableWidth();

            // Logo cell
            var logoCell = new Cell().SetBorder(Border.NO_BORDER)
                .SetVerticalAlignment(VerticalAlignment.MIDDLE);
            if (File.Exists(LogoPath))
            {
                var logo = new iText.Layout.Element.Image(
                    ImageDataFactory.Create(LogoPath))
                    .SetWidth(80).SetHeight(80);
                logoCell.Add(logo);
            }
            else
            {
                logoCell.Add(new Paragraph(CompanyName)
                    .SetFont(fontBold).SetFontSize(14)
                    .SetFontColor(navyColor));
            }
            headerTable.AddCell(logoCell);

            // Company info cell
            var compCell = new Cell().SetBorder(Border.NO_BORDER)
                .SetTextAlignment(TextAlignment.RIGHT);
            compCell.Add(new Paragraph(CompanyName)
                .SetFont(fontBold).SetFontSize(16)
                .SetFontColor(navyColor));
            compCell.Add(new Paragraph(CompanyAddress)
                .SetFont(fontNormal).SetFontSize(8)
                .SetFontColor(ColorConstants.GRAY));
            compCell.Add(new Paragraph($"Tel: {CompanyPhone}  |  Email: {CompanyEmail}")
                .SetFont(fontNormal).SetFontSize(8)
                .SetFontColor(ColorConstants.GRAY));
            compCell.Add(new Paragraph($"TRN: {CompanyTRN}")
                .SetFont(fontNormal).SetFontSize(8)
                .SetFontColor(ColorConstants.GRAY));
            headerTable.AddCell(compCell);
            document.Add(headerTable);

            // ── TAX INVOICE TITLE ─────────────────
            document.Add(new Paragraph("TAX INVOICE")
                .SetFont(fontBold).SetFontSize(20)
                .SetFontColor(navyColor)
                .SetTextAlignment(TextAlignment.CENTER)
                .SetMarginTop(5).SetMarginBottom(5));

            // Divider line
            var divTable = new Table(1).UseAllAvailableWidth();
            divTable.AddCell(new Cell().SetHeight(3)
                .SetBackgroundColor(navyColor)
                .SetBorder(Border.NO_BORDER));
            document.Add(divTable);

            // ── CUSTOMER + INVOICE DETAILS ────────
            var detailTable = new Table(
                UnitValue.CreatePercentArray(new float[] { 1, 1 }))
                .UseAllAvailableWidth()
                .SetMarginTop(8);

            // Left — Customer
            var custCell = new Cell().SetBorder(Border.NO_BORDER)
                .SetBackgroundColor(lightGray).SetPadding(8);
            custCell.Add(new Paragraph("BILL TO")
                .SetFont(fontBold).SetFontSize(9)
                .SetFontColor(navyColor));
            custCell.Add(new Paragraph(cust["FullName"]?.ToString() ?? "")
                .SetFont(fontBold).SetFontSize(11));
            if (!string.IsNullOrEmpty(cust["CompanyName"]?.ToString()))
                custCell.Add(new Paragraph(cust["CompanyName"].ToString()!)
                    .SetFont(fontNormal).SetFontSize(9));
            if (!string.IsNullOrEmpty(cust["Address"]?.ToString()))
                custCell.Add(new Paragraph(cust["Address"].ToString()!)
                    .SetFont(fontNormal).SetFontSize(9));
            custCell.Add(new Paragraph($"Tel: {cust["Phone"]}")
                .SetFont(fontNormal).SetFontSize(9));
            custCell.Add(new Paragraph($"Email: {cust["Email"]}")
                .SetFont(fontNormal).SetFontSize(9));
            if (!string.IsNullOrEmpty(inv["Attention"]?.ToString()))
                custCell.Add(new Paragraph($"Attn: {inv["Attention"]}")
                    .SetFont(fontNormal).SetFontSize(9));
            if (!string.IsNullOrEmpty(inv["TRN"]?.ToString()))
                custCell.Add(new Paragraph($"TRN: {inv["TRN"]}")
                    .SetFont(fontBold).SetFontSize(9));
            detailTable.AddCell(custCell);

            // Right — Invoice details
            var invCell = new Cell().SetBorder(Border.NO_BORDER)
                .SetPadding(8);

            void AddRow(string label, string value)
            {
                var t = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1 }))
                    .UseAllAvailableWidth();
                t.AddCell(new Cell().SetBorder(Border.NO_BORDER)
                    .Add(new Paragraph(label)
                        .SetFont(fontBold).SetFontSize(9)
                        .SetFontColor(navyColor)));
                t.AddCell(new Cell().SetBorder(Border.NO_BORDER)
                    .Add(new Paragraph(value)
                        .SetFont(fontNormal).SetFontSize(9)));
                invCell.Add(t);
            }

            AddRow("Invoice No:", inv["InvoiceNo"]?.ToString() ?? "");
            AddRow("Date:", inv["InvoiceDate"]?.ToString() ?? "");
            AddRow("Due Date:", inv["DueDate"]?.ToString() ?? "N/A");
            AddRow("Job No:", inv["JobNo"]?.ToString() ?? "");
            AddRow("Job Date:", inv["JobDate"]?.ToString() ?? "");
            AddRow("Job Type:", inv["JobType"]?.ToString() ?? "");
            AddRow("Shipment Type:", inv["ShipmentType"]?.ToString() ?? "");
            AddRow("Ex. Rate:", inv["ExRate"]?.ToString() ?? "3.685");
            detailTable.AddCell(invCell);
            document.Add(detailTable);

            // ── ITEMS TABLE ───────────────────────
            document.Add(new Paragraph("").SetMarginTop(8));

            var itemsTable = new Table(
                UnitValue.CreatePercentArray(new float[] { 0.5f, 4f, 0.8f, 0.8f, 1f, 1f }))
                .UseAllAvailableWidth();

            // Header row
            string[] headers = { "Sr.", "Description", "Cur", "Qty", "Unit Price", "Amount" };
            foreach (var h in headers)
            {
                itemsTable.AddHeaderCell(new Cell()
                    .SetBackgroundColor(navyColor)
                    .SetPadding(6)
                    .Add(new Paragraph(h)
                        .SetFont(fontBold).SetFontSize(9)
                        .SetFontColor(ColorConstants.WHITE)));
            }

            // Item rows
            bool altRow = false;
            foreach (var item in items)
            {
                var bg = altRow ? lightGray : ColorConstants.WHITE;
                altRow = !altRow;

                itemsTable.AddCell(new Cell().SetBackgroundColor(bg).SetPadding(5)
                    .Add(new Paragraph(item["SrNo"]?.ToString() ?? "")
                        .SetFont(fontNormal).SetFontSize(9)));
                itemsTable.AddCell(new Cell().SetBackgroundColor(bg).SetPadding(5)
                    .Add(new Paragraph(item["Description"]?.ToString() ?? "")
                        .SetFont(fontNormal).SetFontSize(9)));
                itemsTable.AddCell(new Cell().SetBackgroundColor(bg).SetPadding(5)
                    .Add(new Paragraph(item["Currency"]?.ToString() ?? "AED")
                        .SetFont(fontNormal).SetFontSize(9)));
                itemsTable.AddCell(new Cell().SetBackgroundColor(bg).SetPadding(5)
                    .SetTextAlignment(TextAlignment.CENTER)
                    .Add(new Paragraph(item["Qty"]?.ToString() ?? "")
                        .SetFont(fontNormal).SetFontSize(9)));
                itemsTable.AddCell(new Cell().SetBackgroundColor(bg).SetPadding(5)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .Add(new Paragraph(
                        Convert.ToDouble(item["UnitPrice"]).ToString("N2"))
                        .SetFont(fontNormal).SetFontSize(9)));
                itemsTable.AddCell(new Cell().SetBackgroundColor(bg).SetPadding(5)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .Add(new Paragraph(
                        Convert.ToDouble(item["TotalAmount"]).ToString("N2"))
                        .SetFont(fontNormal).SetFontSize(9)));
            }

            // Totals rows
            double subTotal = Convert.ToDouble(inv["SubTotal"]);
            double vat = Convert.ToDouble(inv["VAT"]);
            double roundOff = Convert.ToDouble(inv["RoundOff"]);
            double total = Convert.ToDouble(inv["TotalAmount"]);

            void AddTotalRow(string label, string value, bool isBold = false)
            {
                var f = isBold ? fontBold : fontNormal;
                itemsTable.AddCell(new Cell(1, 5)
                    .SetBorder(Border.NO_BORDER)
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetPadding(4)
                    .Add(new Paragraph(label).SetFont(f).SetFontSize(9)));
                itemsTable.AddCell(new Cell()
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetPadding(4)
                    .Add(new Paragraph(value).SetFont(f).SetFontSize(9)));
            }

            AddTotalRow("Sub Total (AED):", subTotal.ToString("N2"));
            AddTotalRow("VAT (5%):", vat.ToString("N2"));
            AddTotalRow("Round Off:", roundOff.ToString("N2"));
            AddTotalRow("INVOICE TOTAL (AED):", total.ToString("N2"), true);
            AddTotalRow("NET PAYABLE (AED):", total.ToString("N2"), true);

            document.Add(itemsTable);

            // ── AMOUNT IN WORDS ───────────────────
            document.Add(new Paragraph(
                $"Amount in Words: {inv["AmountInWords"]}")
                .SetFont(fontBold).SetFontSize(9)
                .SetFontColor(navyColor)
                .SetMarginTop(6)
                .SetBackgroundColor(lightGray)
                .SetPadding(6));

            // ── DECLARATION + BANK DETAILS ────────
            var footerTable = new Table(
                UnitValue.CreatePercentArray(new float[] { 1, 1 }))
                .UseAllAvailableWidth()
                .SetMarginTop(10);

            // Left — Declaration
            var declCell = new Cell().SetBorder(Border.NO_BORDER).SetPadding(5);
            declCell.Add(new Paragraph("Declaration")
                .SetFont(fontBold).SetFontSize(9).SetFontColor(navyColor));
            string[] declarations = {
                "1. We declare that this invoice shows the actual price of the goods described and that all particulars are true and correct.",
                "2. Our Terms & Conditions apply.",
                "3. Payment received by cheque, subject to realization.",
                "4. Kindly issue the cheque in favour of AVN SHIPPING F.Z.E.",
                "5. If there are any disputes regarding this invoice, please notify us in writing within 10 days from the invoice date."
            };
            foreach (var d in declarations)
                declCell.Add(new Paragraph(d)
                    .SetFont(fontNormal).SetFontSize(7.5f)
                    .SetMarginBottom(2));
            footerTable.AddCell(declCell);

            // Right — Bank Details
            var bankCell = new Cell()
                .SetBackgroundColor(lightGray).SetPadding(8);
            bankCell.Add(new Paragraph("Company's Bank Details")
                .SetFont(fontBold).SetFontSize(9).SetFontColor(navyColor));

            void AddBankRow(string label, string value)
            {
                var t = new Table(UnitValue.CreatePercentArray(new float[] { 1, 1.5f }))
                    .UseAllAvailableWidth();
                t.AddCell(new Cell().SetBorder(Border.NO_BORDER)
                    .Add(new Paragraph(label)
                        .SetFont(fontBold).SetFontSize(8)));
                t.AddCell(new Cell().SetBorder(Border.NO_BORDER)
                    .Add(new Paragraph(value)
                        .SetFont(fontNormal).SetFontSize(8)));
                bankCell.Add(t);
            }

            AddBankRow("A/c Holder:", BankHolder);
            AddBankRow("Bank Name:", BankName);
            AddBankRow("A/c No:", BankAccount);
            AddBankRow("IBAN:", BankIBAN);
            AddBankRow("Swift Code:", BankSwift);
            footerTable.AddCell(bankCell);
            document.Add(footerTable);

            // ── SIGNATURE ─────────────────────────
            document.Add(new Paragraph("Authorized Signature: _______________________")
                .SetFont(fontBold).SetFontSize(9)
                .SetTextAlignment(TextAlignment.RIGHT)
                .SetMarginTop(20));

            document.Close();
        }

        // ── DATA LOADERS ──────────────────────────
        private static Dictionary<string, object?> LoadInvoice(int id)
        {
            var result = new Dictionary<string, object?>();
            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(
                "SELECT * FROM Invoices WHERE InvoiceID=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var r = cmd.ExecuteReader();
            if (r.Read())
                for (int i = 0; i < r.FieldCount; i++)
                    result[r.GetName(i)] = r.IsDBNull(i) ? null : r.GetValue(i);
            return result;
        }

        private static List<Dictionary<string, object?>> LoadItems(int invoiceID)
        {
            var list = new List<Dictionary<string, object?>>();
            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(
                "SELECT * FROM InvoiceItems WHERE InvoiceID=@id ORDER BY SrNo", con);
            cmd.Parameters.AddWithValue("@id", invoiceID);
            using var r = cmd.ExecuteReader();
            while (r.Read())
            {
                var row = new Dictionary<string, object?>();
                for (int i = 0; i < r.FieldCount; i++)
                    row[r.GetName(i)] = r.IsDBNull(i) ? null : r.GetValue(i);
                list.Add(row);
            }
            return list;
        }

        private static Dictionary<string, object?> LoadCustomer(int id)
        {
            var result = new Dictionary<string, object?>();
            using var con = DatabaseHelper.GetConnection();
            con.Open();
            using var cmd = new SqliteCommand(
                "SELECT * FROM Customers WHERE CustomerID=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var r = cmd.ExecuteReader();
            if (r.Read())
                for (int i = 0; i < r.FieldCount; i++)
                    result[r.GetName(i)] = r.IsDBNull(i) ? null : r.GetValue(i);
            return result;
        }
    }
}