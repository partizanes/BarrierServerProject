using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarrierServerProject
{
    static class TimeSpanExtensions
    {
        private static bool IsBetween(this TimeSpan time,
                                      TimeSpan startTime, TimeSpan endTime)
        {
            if (endTime == startTime)
            {
                return true;
            }

            if (endTime < startTime)
            {
                return time <= endTime ||
                    time >= startTime;
            }

            return time >= startTime &&
                time <= endTime;
        }

        public static bool IsTimeWork()
        {
            if (DateTime.Now.TimeOfDay.IsBetween(new TimeSpan(23, 00, 0), new TimeSpan(7, 30, 0)))
            {
                Color.WriteLineColor("Ночью LsTrade монопольно используется планировщиком. Пропущено.", ConsoleColor.Red);
                return false;
            }
            else
                return true;
        }
    }
}
