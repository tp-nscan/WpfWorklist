﻿using System;

namespace Utils
{
    public class DateUtils
    {
        public static DateTime AddWorkingDays(DateTime dtFrom, int nDays)
        {
            // determine if we are increasing or decreasing the days
            int nDirection = 1;
            if (nDays < 0)
            {
                nDirection = -1;
            }

            // move ahead the day of week
            int nWeekday = nDays % 5;
            while (nWeekday != 0)
            {
                dtFrom = dtFrom.AddDays(nDirection);

                if (dtFrom.DayOfWeek != DayOfWeek.Saturday
                    && dtFrom.DayOfWeek != DayOfWeek.Sunday)
                {
                    nWeekday -= nDirection;
                }
            }

            // move ahead the number of weeks
            int nDayweek = (nDays / 5) * 7;
            dtFrom = dtFrom.AddDays(nDayweek);

            return dtFrom;
        }
    }
}
