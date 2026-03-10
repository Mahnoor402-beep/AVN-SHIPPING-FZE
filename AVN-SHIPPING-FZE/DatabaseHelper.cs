using Microsoft.Data.Sqlite;

namespace AVN_SHIPPING_FZE
{
    public class DatabaseHelper
    {
        private static string dbPath = "AVN_Shipping.db";
        private static string connectionString = $"Data Source={dbPath}";

        public static void InitializeDatabase()
        {
            using var con = new SqliteConnection(connectionString);
            con.Open();
            var cmd = con.CreateCommand();

            // CUSTOMERS
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Customers (
                CustomerID   INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName     TEXT NOT NULL,
                CompanyName  TEXT,
                Phone        TEXT,
                Email        TEXT,
                Address      TEXT,
                TRN          TEXT,
                CreatedDate  TEXT DEFAULT (date('now')));";
            cmd.ExecuteNonQuery();

            // SHIPMENTS
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Shipments (
                ShipmentID     INTEGER PRIMARY KEY AUTOINCREMENT,
                CustomerID     INTEGER,
                Origin         TEXT,
                Destination    TEXT,
                ShipmentDate   TEXT,
                DeliveryDate   TEXT,
                Status         TEXT DEFAULT 'Pending',
                Weight         REAL,
                Description    TEXT);";
            cmd.ExecuteNonQuery();

            // JOB CARDS
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS JobCards (
                JobCardID           INTEGER PRIMARY KEY AUTOINCREMENT,
                JobCardNo           TEXT,
                JobCardType         TEXT,
                DateCreated         TEXT,
                Status              TEXT DEFAULT 'Open',
                Shipper             TEXT,
                Consignee           TEXT,
                InvoiceNumber       TEXT,
                NoOfPkgs            TEXT,
                ShippingBillNo      TEXT,
                PortOfLoading       TEXT,
                PortOfDischarge     TEXT,
                WeightKG            REAL,
                ShippingLine        TEXT,
                BookingNo           TEXT,
                BookingReleaseDate  TEXT,
                PlanVessel          TEXT,
                SOB                 TEXT,
                ETD                 TEXT,
                ETA                 TEXT,
                DraftReceived       TEXT,
                InvoiceReceived     TEXT,
                CHAName             TEXT,
                CargoCartingDt      TEXT,
                StuffingDt          TEXT,
                VGMSubmitDate       TEXT,
                ShipmentHandoverDt  TEXT,
                CustomClearDate     TEXT,
                HumidityReq         TEXT,
                NoOfContrs          TEXT,
                LineSeal            TEXT,
                CustSeal            TEXT,
                FCLOrLCL            TEXT DEFAULT 'FCL',
                BLReleasedDate      TEXT,
                GateInPOLDate       TEXT,
                HBLNo               TEXT,
                MBLNo               TEXT,
                SISubmitDt          TEXT,
                DocumentDeliveryDt  TEXT,
                Notes               TEXT,
                CustomerID          INTEGER);";
            cmd.ExecuteNonQuery();

            // REMINDERS
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Reminders (
                ReminderID      INTEGER PRIMARY KEY AUTOINCREMENT,
                JobCardID       INTEGER,
                JobCardNo       TEXT,
                EventType       TEXT,
                EventDate       TEXT,
                ReminderDate    TEXT,
                Status          TEXT DEFAULT 'Pending',
                EmailSent       INTEGER DEFAULT 0,
                RecipientEmail  TEXT,
                Priority        TEXT DEFAULT 'Normal',
                Notes           TEXT);";
            cmd.ExecuteNonQuery();

            // BILLING
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Billing (
                BillingID      INTEGER PRIMARY KEY AUTOINCREMENT,
                ShipmentID     INTEGER,
                CustomerID     INTEGER,
                InvoiceDate    TEXT,
                Amount         REAL,
                Tax            REAL,
                TotalAmount    REAL,
                PaymentStatus  TEXT DEFAULT 'Unpaid');";
            cmd.ExecuteNonQuery();

            // EMPLOYEES
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Employees (
                EmployeeID     INTEGER PRIMARY KEY AUTOINCREMENT,
                FullName       TEXT NOT NULL,
                Role           TEXT,
                Phone          TEXT,
                Email          TEXT,
                JoinDate       TEXT,
                Salary         REAL);";
            cmd.ExecuteNonQuery();

            // INVOICES
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Invoices (
                InvoiceID       INTEGER PRIMARY KEY AUTOINCREMENT,
                InvoiceNo       TEXT,
                CustomerID      INTEGER,
                JobNo           TEXT,
                JobDate         TEXT,
                JobType         TEXT,
                ShipmentType    TEXT,
                ExRate          REAL DEFAULT 3.685,
                Attention       TEXT,
                TRN             TEXT,
                InvoiceDate     TEXT,
                DueDate         TEXT,
                SubTotal        REAL,
                VAT             REAL,
                RoundOff        REAL DEFAULT 0,
                TotalAmount     REAL,
                AmountInWords   TEXT,
                PaymentStatus   TEXT DEFAULT 'Unpaid',
                Notes           TEXT,
                CreatedDate     TEXT DEFAULT (date('now')));";
            cmd.ExecuteNonQuery();

            // INVOICE ITEMS
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS InvoiceItems (
                ItemID          INTEGER PRIMARY KEY AUTOINCREMENT,
                InvoiceID       INTEGER,
                SrNo            INTEGER,
                Description     TEXT,
                Currency        TEXT DEFAULT 'AED',
                Qty             REAL,
                UnitPrice       REAL,
                TotalAmount     REAL);";
            cmd.ExecuteNonQuery();

            // EXPENSES
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS Expenses (
                ExpenseID       INTEGER PRIMARY KEY AUTOINCREMENT,
                ExpenseDate     TEXT,
                Category        TEXT,
                Description     TEXT,
                Amount          REAL,
                PaymentMethod   TEXT,
                Reference       TEXT,
                Notes           TEXT);";
            cmd.ExecuteNonQuery();
        }

        public static SqliteConnection GetConnection()
        {
            return new SqliteConnection(connectionString);
        }
    }
}