namespace AVN_SHIPPING_FZE
{
    public static class NumberToWords
    {
        private static string[] ones = {
            "", "One", "Two", "Three", "Four", "Five", "Six", "Seven",
            "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen",
            "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

        private static string[] tens = {
            "", "", "Twenty", "Thirty", "Forty", "Fifty",
            "Sixty", "Seventy", "Eighty", "Ninety"
        };

        public static string Convert(double amount)
        {
            try
            {
                int dirhams = (int)Math.Floor(amount);
                int fils = (int)Math.Round((amount - dirhams) * 100);

                string result = ConvertInt(dirhams) + " Dirham";
                if (dirhams != 1) result += "s";
                if (fils > 0)
                    result += " and " + ConvertInt(fils) + " Fils";
                result += " Only";
                return result.ToUpper();
            }
            catch { return ""; }
        }

        private static string ConvertInt(int n)
        {
            if (n == 0) return "Zero";
            if (n < 0) return "Minus " + ConvertInt(-n);
            string words = "";
            if (n >= 1000000)
            {
                words += ConvertInt(n / 1000000) + " Million ";
                n %= 1000000;
            }
            if (n >= 1000)
            {
                words += ConvertInt(n / 1000) + " Thousand ";
                n %= 1000;
            }
            if (n >= 100)
            {
                words += ones[n / 100] + " Hundred ";
                n %= 100;
            }
            if (n >= 20)
            {
                words += tens[n / 10] + " ";
                n %= 10;
            }
            if (n > 0)
                words += ones[n] + " ";
            return words.Trim();
        }
    }
}