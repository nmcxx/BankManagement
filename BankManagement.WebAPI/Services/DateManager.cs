using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BankManagement.WebAPI.Services
{
    public class DateManager
    {
        static bool IsMonthAssigned { get; set; }



        public static DateTime GetDate(string d)
        {
            char[] splitsoptions = { '/', '-', ' ' };
            foreach (var i in splitsoptions)
            {
                var y = 0;
                var m = 0;
                var day = 0;
                if (d.IndexOf(i) > 0)
                {
                    try
                    {
                        foreach (var e in d.Split(i))
                        {


                            if (e.Length == 4)
                            {
                                y = Convert.ToInt32(e);

                                continue;
                            }
                            if (Convert.ToInt32(e) <= 12 )
                            {
                                m = Convert.ToInt32(e);
                                IsMonthAssigned = true;
                                continue;
                            }
                            day = Convert.ToInt32(e);


                        }

                        return new DateTime(y, m, day);
                    }
                    catch
                    {
                        //We are silent about this but we  could set a message about wrong date input in ViewBag    and display to user if this  this method returns null
                    }
                }
            }
            return DateTime.Now;


        }
        // Another overload. this will catch more date formats without manually checking as above

        public static DateTime GetDate(string d, bool custom)
        {
            CultureInfo culture = new CultureInfo("en-US");

            string[] dateFormats =
            {
                "dd/MM/yyyy", "MM/dd/yyyy", "yyyy/MM/dd", "yyyy/dd/MM", "dd-MM-yyyy", "MM-dd-yyyy", "yyyy-MM-dd",
                "yyyy-dd-MM", "dd MM yyyy", "MM dd yyyy", "yyyy MM dd", "yyyy dd MM", "dd.MM.yyyy", "MM.dd.yyyy",
                "yyyy.MM.dd", "yyyy.dd.MM","yyyyMMdd","yyyyddMM","MMddyyyy","ddMMyyyy"
            };//add your own to the array if any

            culture.DateTimeFormat.SetAllDateTimePatterns(dateFormats, 'Y');

            if (DateTime.TryParseExact(d, dateFormats, culture, DateTimeStyles.None, out var date))
                return date;

            return DateTime.Now;


        }
    }
}
