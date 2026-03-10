namespace AVN_SHIPPING_FZE
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(
                UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) =>
                MessageBox.Show("CRASH: " + e.Exception.Message
                + "\n\nLocation: " + e.Exception.StackTrace);

            ApplicationConfiguration.Initialize();
            DatabaseHelper.InitializeDatabase();
            Application.Run(new MainForm());
        }
    }
}