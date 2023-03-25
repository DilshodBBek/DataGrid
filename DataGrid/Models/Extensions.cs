using System.Globalization;

namespace DataGrid.Models
{
    public static class Extensions
    {
        static readonly string[] possibleFormats =
            {

                    "dd/MM/yyyy",
                    "dd/M/yyyy",
                    "d/MM/yyyy",
                    "d/M/yyyy",
                    "dd-MM-yyyy",
                    "d-MM-yyyy",
                    "dd-M-yyyy",
                    "d-M-yyyy",
                    "dd.MM.yyyy",
                    "d.MM.yyyy",
                    "dd.M.yyyy",
                    "d.M.yyyy"
             };
        public static DateTime? StringToDateTime(this string value)
        {
            try
            {
                DateTime parsedDateTime;
                foreach (string format in possibleFormats)
                {
                    if (DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDateTime))
                    {
                        return parsedDateTime;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
