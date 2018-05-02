using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeActs.Service
{
    public static class IDGenerator
    {
        private static object lockobject = new object();

        public static Guid[] arrNewGuid(char serverCode, int qty)
        {
            //Returns array of unique Guids. Length of array is set by value passed of qty
            Guid[] arrReturn = new Guid[qty];
            long lngDayCount;
            long lngTodayMilliSeconds;
            long lngTotalDiff;
            DateTime currentDate;
            DateTime centuryBegin;
            lock (lockobject)
            {
                Thread.Sleep(1); //usually waits about 20 ms
                centuryBegin = new DateTime(1800, 1, 1);
                currentDate = DateTime.Now;
            }
            lngDayCount = Convert.ToInt64((currentDate.Date - centuryBegin.Date).TotalDays);
            lngTodayMilliSeconds = Convert.ToInt64(currentDate.TimeOfDay.TotalMilliseconds);
            lngTotalDiff = ((86400000 * lngDayCount) + lngTodayMilliSeconds) * 1000;

            for (int i = 0; i < qty; i++)
            {
                string strUnordered = lngTotalDiff.ToString().PadLeft(20, '0') + i.ToString().PadLeft(8, '0') + ((int)serverCode).ToString().PadLeft(4, '0');
                arrReturn[i] = Guid.ParseExact(
                strUnordered.Substring(30, 2) + strUnordered.Substring(28, 2) + strUnordered.Substring(26, 2) + strUnordered.Substring(24, 2)
                + "-" + strUnordered.Substring(22, 2) + strUnordered.Substring(20, 2)
                + "-" + strUnordered.Substring(18, 2) + strUnordered.Substring(16, 2)
                + "-" + strUnordered.Substring(12, 4) + "-" + strUnordered.Substring(0, 12)
                , "D");
            }
            return arrReturn;
        }
        public static Guid NewGuid(char serverCode)
        {
            /*
            Old version by Santosh that creates GUID that are not serial
            DateTime centuryBegin;
            DateTime currentDate;
            lock (lockobject)
            {
                Thread.Sleep(1);
                centuryBegin = new DateTime(1800, 1, 1);
                currentDate = DateTime.Now;
            }
            new Thread(() => { }).Start();
            long elapsedTicks = currentDate.Ticks - centuryBegin.Ticks;
            return Guid.ParseExact("00" + ((int)serverCode).ToString() + elapsedTicks.ToString().PadLeft(28, '0'), "N");
            */
            long lngDayCount;
            long lngTodayMilliSeconds;
            long lngTotalDiff;
            DateTime currentDate;
            DateTime centuryBegin;
            string strUnordered;
            lock (lockobject)
            {
                Thread.Sleep(1); //usually waits about 20 ms
                centuryBegin = new DateTime(1800, 1, 1);
                currentDate = DateTime.Now;
            }
            lngDayCount = Convert.ToInt64((currentDate.Date - centuryBegin.Date).TotalDays);
            lngTodayMilliSeconds = Convert.ToInt64(currentDate.TimeOfDay.TotalMilliseconds);
            lngTotalDiff = ((86400000 * lngDayCount) + lngTodayMilliSeconds) * 1000;

            strUnordered = lngTotalDiff.ToString().PadLeft(20, '0') + ((int)serverCode).ToString().PadLeft(12, '0');

            return Guid.ParseExact(
                strUnordered.Substring(30, 2) + strUnordered.Substring(28, 2) + strUnordered.Substring(26, 2) + strUnordered.Substring(24, 2)
                + "-" + strUnordered.Substring(22, 2) + strUnordered.Substring(20, 2)
                + "-" + strUnordered.Substring(18, 2) + strUnordered.Substring(16, 2)
                + "-" + strUnordered.Substring(12, 4) + "-" + strUnordered.Substring(0, 12)
                , "D");
        }
    }
}
