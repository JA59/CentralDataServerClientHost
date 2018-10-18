/*
**
**COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**    Copyright © 2006 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Text.RegularExpressions;

namespace AutoChem.Core
{
    /// <summary>
    /// Provides method for formatting objects as text.
    /// </summary>
    public class TextFormatter
    {
        /// <summary>
        /// Formats a timespan down to milliseconds.
        /// </summary>
        /// <param name="time">The time span.</param>
        /// <returns>The text representation including milliseconds.</returns>
        public static string FormatToMS(TimeSpan time)
        {
            int hours = (int)System.Math.Floor(System.Math.Abs(time.TotalHours));
            if (time < TimeSpan.Zero)
            {
                return String.Format("-{0:00}:{1:00}:{2:00}.{3:000}", hours, -time.Minutes, -time.Seconds, -time.Milliseconds);
            }
            else
            {
                return String.Format("{0:00}:{1:00}:{2:00}.{3:000}", hours, time.Minutes, time.Seconds, time.Milliseconds);
            }
        }

        /// <summary>
        /// Rounds a timespan milliseconds
        /// </summary>
        /// <param name="time">The time span.</param>
        /// <returns>The text representation without milliseconds.</returns>
        public static string RoundToSeconds(TimeSpan time)
        {
            return RoundMilliseconds(time, 0);
        }

        /// <summary>
        /// Rounds a timespan to tenth of a second.
        /// </summary>
        /// <param name="time">The time span.</param>
        /// <returns>The text representation including tenth of a second.</returns>
        public static string RoundToTenths(TimeSpan time)
        {
            return RoundMilliseconds(time, 1);
        }

        /// <summary>
        /// Rounds a timespan to hundredth of a second.
        /// </summary>
        /// <param name="time">The time span.</param>
        /// <returns>The text representation including hundred of a second.</returns>
        public static string RoundToHundredths(TimeSpan time)
        {
            return RoundMilliseconds(time, 2);
        }

        /// <summary>
        /// Rounds a timespan to thousandth of a second.
        /// </summary>
        /// <param name="time">The time span.</param>
        /// <returns>The text representation including thousandth of a second.</returns>
        public static string RoundToThousandths(TimeSpan time)
        {
            return RoundMilliseconds(time, 3);
        }

        private static string RoundMilliseconds(TimeSpan time, int significantMillisecondsDigits)
        {
            string sign = time.CompareTo(TimeSpan.FromSeconds(0)) >= 0 ? string.Empty : "-";

            if (sign == "-")
            {
                time = time.Negate();
            }

            time = TimeSpan.FromSeconds(System.Math.Round(time.TotalSeconds, significantMillisecondsDigits));
            string timeSpanText = string.Format("{0}{1:00}:{2:00}:{3:00}", sign, (int)time.TotalHours, time.Minutes, time.Seconds);
            string millisecondsText = time.Milliseconds.ToString();

            string value;

            if (significantMillisecondsDigits == 0)
            {
                value = timeSpanText;
            }
            else if (time.Milliseconds == 0)
            {
                value = string.Format("{0}.{1}", timeSpanText, "".PadRight(significantMillisecondsDigits, '0'));
            }
            else
            {
                value = string.Format("{0}.{1}", timeSpanText, millisecondsText.PadLeft(3, '0').Substring(0, significantMillisecondsDigits));
            }

            return value;
        }

        /// <summary>
        /// Formats a timespan to HH:MM:SS (excluding the Milli-second) and round the seconds.
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <returns></returns>
        public static string TimeSpanToHHMMSS(TimeSpan timeSpan)
        {
            //work out the total number of seconds, so we can round this number
            //we need to take into account the fractional part of the timeSpan
            int seconds = (int)System.Math.Round(timeSpan.TotalSeconds);
            TimeSpan roundedTimeSpan = new TimeSpan(0, 0, seconds);

            int hour = System.Math.Max(0, roundedTimeSpan.Hours + 24 * roundedTimeSpan.Days);
            int mins = System.Math.Max(0, roundedTimeSpan.Minutes);
            int secs = System.Math.Max(0, roundedTimeSpan.Seconds);
            if (hour > 99)
                return string.Format("{0:000}:{1:00}:{2:00}", hour, mins, secs);
            else
                return string.Format("{0:00}:{1:00}:{2:00}", hour, mins, secs);
        }

        /// <summary>
        /// Escapes any unescaped braces in a string.  This can be used to when using a string that has {}'s
        /// in a string.Format call to escape the []'s.
        /// </summary>
        public static string EscapeBraces(string text)
        {
            // Replace single { with double {
            Regex regex = new Regex("(^|[^{]+)[{]([^{]+)");
            text = regex.Replace(text, "$1{{$2");

            // Replace single } with double }
            regex = new Regex("([^}]+)[}]([^}]+|$)");
            text = regex.Replace(text, "$1}}$2");

            return text;
        }
    }
}
