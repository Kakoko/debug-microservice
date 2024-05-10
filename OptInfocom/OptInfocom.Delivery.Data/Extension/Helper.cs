using System.Globalization;

namespace OptInfocom.Delivery.Data.Extension
{
    public static class Helper
    {
        public static DateTime ExtChangeDateTimeFormat(this string dateString)
        {
            DateTime dateTime = new DateTime();
            string FormatType = "dd-MM-yyyy";
            try
            {
                if (!string.IsNullOrEmpty(dateString))
                    dateTime = Convert.ToDateTime(DateTime.ParseExact(dateString, FormatType, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
                else
                    dateTime = Convert.ToDateTime(DateTime.ParseExact("01-01-1900", FormatType, CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"));
            }
            catch (FormatException)
            {
                
            }
            return dateTime;
        }

        public static int ExtConvertToInt(this string intValue)
        {
            int value = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(intValue))
                    value = 0;
                else
                    value = Convert.ToInt32(intValue);
            }
            catch (FormatException)
            {

            }
            return value;
        }

        public static int ExtFinancialYear(this DateTime dateTime)
        {
            int FinancialYear = 0;
            try
            {
                if (dateTime.Month >= 4)
                    FinancialYear = string.Format("{0}{1}", dateTime.Year, dateTime.Year + 1).ExtConvertToInt();
                else
                    FinancialYear = string.Format("{0}{1}", dateTime.Year - 1, dateTime.Year).ExtConvertToInt();
                return FinancialYear;
            }
            catch (Exception)
            {
                return FinancialYear;
            }
        }
    }
}
