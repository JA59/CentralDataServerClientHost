/*
**  COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**  Copyright (c) 2006 by Mettler Toledo AutoChem.  All rights reserved.
**/

using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AutoChem.Core.Time
{
    /// <summary>
    /// Provides static methods for dealing with the performance counter and formatting TimeSpans.
    /// </summary>
    public static class TimingUtility
    {
		/// <summary>
		/// Time parsing error
		/// </summary>
	    public const string InvalidTimeDuplicatedParts = "More than one value specified for hours, minutes or seconds: {0}.";

	    /// <summary>
	    /// Time parsing error
	    /// </summary>
	    public const string InvalidTimeFormat = "Value must be specified in the format of hhh:mm:ss or ?h?m?s : {0}. Error: {1}";

#if !SILVERLIGHT
		/// <summary>
		/// Provides English US culture information
		/// </summary>
		static public readonly CultureInfo CultureInfo_US = new CultureInfo("en-US", false);

        /// <summary>
        /// Returns the current value of the performance counter.
        /// </summary>
        /// <returns>False if there is no performance counter.</returns>
        [DllImport("KERNEL32.DLL", SetLastError = true)]
        public static extern bool QueryPerformanceCounter(out long count);

        /// <summary>
        /// Returns the frequency of the performance counter.
        /// </summary>
        /// <returns>False if there is no performance counter.</returns>
        [DllImport("KERNEL32.DLL", SetLastError = true)]
        public static extern bool QueryPerformanceFrequency(out long frequency);

        /// <summary>
        /// Returns the diference between the starting count and the current performance count as a TimeSpan.
        /// </summary>
        public static TimeSpan GetPerformanceCountDelta(long startCount)
        {
            long stopCount;
            QueryPerformanceCounter(out stopCount);
            return GetPerformanceCountDelta(startCount, stopCount);
        }

        /// <summary>
        /// Takes the starting and stopping count of the performance counter and returns the difference as a TimeSpan.
        /// </summary>
        public static TimeSpan GetPerformanceCountDelta(long startCount, long stopCount)
        {
            long delta = stopCount - startCount;
            return GetPerformanceCountAsTimeSpan(delta);
        }

        /// <summary>
        /// Takes the difference between performance counts and returns them as a TimeSpan
        /// </summary>
        public static TimeSpan GetPerformanceCountAsTimeSpan(long countDiff)
        {
            long countsPerSec;
            QueryPerformanceFrequency(out countsPerSec);
            long countsPerMSec = countsPerSec / 1000;

            long ticks = countDiff * TimeSpan.TicksPerMillisecond / countsPerMSec;

            return TimeSpan.FromTicks(ticks);
        }
#else
		/// <summary>
		/// Provides English US culture information
		/// </summary>
		static public readonly CultureInfo CultureInfo_US = new CultureInfo("en-US");
#endif

		/// <summary>
        /// Returns the number of times the length of time divisor is equal to the length of time dividend.
        /// In other words it returns (dividend / divisor).
        /// </summary>
        public static double Divide(TimeSpan dividend, TimeSpan divisor)
        {
            return ((double)dividend.Ticks / divisor.Ticks);
        }

        /// <summary>
        /// Returns the length of time such that divisor such lengths of time equals the dividend.
        /// In other words it returns (dividend / divisor).
        /// </summary>
        public static TimeSpan Divide(TimeSpan dividend, int divisor)
        {
            return (TimeSpan.FromTicks(dividend.Ticks / divisor));
        }

        /// <summary>
        /// Returns the specified multiple of the timeSpan.
        /// </summary>
        public static TimeSpan Multiply(this TimeSpan timeSpan, int multiplier)
        {
            return TimeSpan.FromTicks(timeSpan.Ticks * multiplier);
        }

        /// <summary>
        /// Returns the specified multiple of the timeSpan.
        /// </summary>
        public static TimeSpan Multiply(this TimeSpan timeSpan, double multiplier)
        {
            // Add 0.5 to round to the nearest tick.
            return TimeSpan.FromTicks((long)(timeSpan.Ticks * multiplier + 0.5));
        }

        /// <summary>
        /// Returns the abolute different for the specified timeSpan.
        /// That is if the timespan is negative make it positive with the same magnitude.
        /// </summary>
        public static TimeSpan Abs(this TimeSpan timeSpan)
        {
            if (timeSpan < TimeSpan.Zero)
            {
                return TimeSpan.FromTicks(-timeSpan.Ticks);
            }
            else
            {
                return timeSpan;
            }
        }

        /// <summary>
        /// Returns the time as Universal time.  If the kind of the time is unspecified
        /// then universal time is assumed.
        /// </summary>
        public static DateTime GetUniversalTime(DateTime dateTime)
        {
            DateTime utcDateTime;
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                utcDateTime = DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }
            else
            {
                utcDateTime = dateTime.ToUniversalTime();
            }

            return utcDateTime;
        }

        /// <summary>
        /// Converts the provided DateTime to a string using local time.
        /// </summary>
        /// <param name="dateTime">The DateTime to convert</param>
        /// <param name="format">The format string</param>
        public static object ConvertToLocalTimeString(DateTime dateTime, string format)
        {
            return ConvertToLocalTimeString(dateTime, format, null);
        }

        /// <summary>
        /// Converts the provided DateTime to a string using local time.
        /// </summary>
        /// <param name="dateTime">The DateTime to convert</param>
        /// <param name="format">The format string</param>
        /// <param name="culture">The culture to use when formatting</param>
        public static object ConvertToLocalTimeString(DateTime dateTime, string format, CultureInfo culture)
        {
            dateTime = dateTime.ToLocalTime();

            return string.Format(culture ?? CultureInfo.CurrentCulture, format, dateTime);
        }

        /// <summary>
        /// Truncates the time to seconds removing fractional seconds.
        /// </summary>
        public static DateTime TruncateToSeconds(DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, 0,
                    dateTime.Kind);
        }

        /// <summary>
        /// Returns the formated timezone of this date in local time in the format "+hh:mm", e.g. +04:00 or -03:30
        /// </summary>
        public static string FormatToLocalTimeZone(DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToString("%K");
        }

        /// <summary>
        /// Returns the date as a formated string (local date) in ISO 8601 format without timezone information: "yyyy-MM-ddTHH:mm:ss" e.g. 2007-⁠12-⁠24T18:21
        /// </summary>
        public static string FormatToLocalTimeIsoFormatNoTimezone(DateTime dateTime)
        {
            return dateTime.ToLocalTime().ToString("s");
        }

        /// <summary>
        /// Takes the given time span and returns a string of the format HH:MM:SS:ff (with milliseconds)
		/// </summary>
        /// <param name="time">The time span to turn into a string</param>
        /// <returns>the formated string</returns>
        public static string FormatToMS(this TimeSpan time)
        {
            return TextFormatter.FormatToMS(time);
        }

		/// <summary>
		/// Takes the given time span and returns a string of the format HH:MM:SS with rounded milliseconds
		/// </summary>
		/// <param name="time">The time span to turn into a string</param>
		/// <returns>the formated string</returns>
		public static string RoundToSeconds(this TimeSpan time)
		{
			return TextFormatter.RoundToSeconds(time);
		}

        /// <summary>
		/// Takes the given time span and returns a string of the format HH:MM:SS:f
        /// </summary>
        /// <param name="time">The time span to turn into a string</param>
        /// <returns>the formated string</returns>
		public static string RoundToTenths(this TimeSpan time)
        {
            return TextFormatter.RoundToTenths(time);
        }

		/// <summary>
		/// Takes the given time span and returns a string of the format HH:MM:SS:ff
		/// </summary>
		/// <param name="time">The time span to turn into a string</param>
		/// <returns>the formated string</returns>
		public static string RoundToHundredths(this TimeSpan time)
		{
			return TextFormatter.RoundToHundredths(time);
		}

		/// <summary>
		/// Takes the given time span and returns a string of the format HH:MM:SS:ddd (where ddd is 3 digits rounded milliseconds)
		/// </summary>
		/// <param name="time">The time span to turn into a string</param>
		/// <returns>the formated string</returns>
		public static string RoundToThousandths(this TimeSpan time)
		{
			return TextFormatter.RoundToThousandths(time);
		}

        /// <summary>
		/// Formats the TimeSpan as a string with full units (1 hour 30 minutes).  This will exclude the millisecond portion
		/// </summary>
		/// <param name="time">Time to format</param>
		/// <returns></returns>
		public static string FormatNicely(TimeSpan time)
		{
			return FormatNicely(time, true);
		}

		/// <summary>
		/// Formats the TimeSpan as a string with full units (1 hour 30 minutes).  This will exclude the millisecond portion
		/// </summary>
		/// <param name="time">Time to format. If this value is null an empty string will be returned.</param>
		/// <returns></returns>
		public static string FormatNicely(TimeSpan? time)
		{
			return FormatNicely(time, true);
		}

	    /// <summary>
	    /// Formats the TimeSpan as a string with full units (1 hour 30 minutes).
	    /// </summary>
	    /// <param name="time">The time to format. If this value is null an empty string will be returned.</param>
	    /// <param name="hideMilliSecs">false to include milliseconds, true to exclude milliseconds</param>
	    /// <returns></returns>
	    public static string FormatNicely(TimeSpan? time, bool hideMilliSecs)
	    {
			if (time.HasValue)
			{
				return FormatNicely(time.Value, true);
			}

			return string.Empty;
	    }

		/// <summary>
		/// Formats the TimeSpan as a string with full units (1 hour 30 minutes).
		/// </summary>
		/// <param name="time">The time to format</param>
		/// <param name="hideMilliSecs">false to include milliseconds, true to exclude milliseconds</param>
		/// <returns></returns>
		public static string FormatNicely( TimeSpan time, bool hideMilliSecs )
		{
			//If the parameter is false, or if there are NO milliseconds in the time, then we DO not care 
			//about milliseconds and we want to ignore them
			bool weCareAboutMilliseconds = (!hideMilliSecs && time.Milliseconds != 0);
			
			//lets eliminate the millisecond portion to make the formating of TotalDay, TotalHours, and TotalMinutes go nicely
			if(!weCareAboutMilliseconds)
				time = new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds, 0);

			if ((int)time.TotalHours == 0 && time.Minutes == 0)
			{
				if (!weCareAboutMilliseconds)
					return time.Seconds + " second" + (time.Seconds > 1 ? "s" : string.Empty);
				else
				{
					//we only want to show milliseconds if the param is true AND there are actual millisec values.
					double milliSec = time.Milliseconds/1000.00;
					return string.Format("{0}{1} seconds", time.Seconds, milliSec.ToString("#.###", CultureInfo_US));
				}
			}

			else if ((int)time.TotalHours == 0 && time.Seconds == 0 && time.Milliseconds == 0)
				return time.Minutes + " minute" + (time.Minutes > 1 ? "s" : string.Empty);
			else if (time.Minutes == 0 && time.Seconds == 0 && time.Milliseconds == 0)
			{
				if (time.TotalDays >= 1 && time.Hours == 0)
					return time.TotalDays.ToString("#.###", CultureInfo_US) + " day" + (time.TotalDays > 1 ? "s" : string.Empty);
				else
					return time.TotalHours.ToString("#.###", CultureInfo_US) + " hour" + (time.TotalHours > 1 ? "s" : string.Empty);
			}
			else
			{
				if (!weCareAboutMilliseconds)
					return FormatToSecs(time);
				else 
					return FormatToMS(time);
			}
		}

    	/// <summary>
        /// Takes the given time span and returns a string of the format hh:mm:ss
        /// </summary>
        /// <param name="time">The time span to turn into a string</param>
        /// <returns>the string value in the format hh:mm:ss</returns>
		public static string FormatToSecs(this TimeSpan time)
    	{
    		int hours = (int) System.Math.Floor(System.Math.Abs(time.TotalHours));

            // If span is negative, but close to 0, make it 0. This removes the issue of showing a negative 0, like "-00:00:00"
            if (time > TimeSpan.FromSeconds(-1) && time < TimeSpan.Zero)
            {
                time = TimeSpan.Zero;
            }

    		if (time < TimeSpan.Zero)
    		{
    			return String.Format("-{0:00}:{1:00}:{2:00}", hours, -time.Minutes, -time.Seconds);
    		}
    		else
    		{
    			return String.Format("{0:00}:{1:00}:{2:00}", hours, time.Minutes, time.Seconds);
    		}
    	}

        /// <summary>
        /// Parse a string formatted in HH:MM:ss or 1hr5min3sec into a TimeSpan.  This will ignore any subsecond
        /// decimal calues.  i.e. 4.5 sec will return a time span of 4 secs.
        /// </summary>
        /// <param name="timeSpanValue">string to parse</param>
        /// <returns>the parsed Time Span</returns>
        public static TimeSpan ParseTimeSpan(string timeSpanValue)
        {
            return ParseTimeSpan(timeSpanValue, true);
        }

        /// <summary>
        /// Parse a string formatted in HH:MM:ss or 1hr5min3sec into a TimeSpan
        /// </summary>
        /// <param name="timeSpanValue">string to parse</param>
        /// <returns>the parsed Time Span</returns>
        /// <param name="truncateSubSecond">true to indicate dropping subsecond values for the returned time span.
        /// False to include the subsecond value</param>
        public static TimeSpan ParseTimeSpan(string timeSpanValue, bool truncateSubSecond)
        {
            return ParseTimeSpan(timeSpanValue, truncateSubSecond, null);
        }

        /// <summary>
        /// Parse a string formatted in HH:MM:ss or 1hr5min3sec into a TimeSpan
        /// </summary>
        /// <param name="timeSpanValue">string to parse</param>
        /// <returns>the parsed Time Span</returns>
        /// <param name="truncateSubSecond">true to indicate dropping subsecond values for the returned time span.
        /// False to include the subsecond value</param>
        /// <param name="defaultUnit">The default unit for times given without a unit.  This would typically be TimeSpan.FromMinutes(1) or TimeSpan.FromHours(1).
        /// If defaultUnit is not specified (null) then 1 minute will be used as the default unit.</param>
        public static TimeSpan ParseTimeSpan(string timeSpanValue, bool truncateSubSecond, TimeSpan? defaultUnit)
        {
            TimeSpanComponents components = new TimeSpanComponents();

	        try
	        {
		        components.GetComponents(timeSpanValue, truncateSubSecond);

		        // if time unit was not specified we are assuming that they are minutes.
		        if ((components.dayFound == 0) && (components.hourFound == 0) && (components.minFound == 0) && (components.secFound == 0))
		        {
			        double minutes = TimeSpanComponents.ConvertValue(timeSpanValue, 2);
					GetTimeWithDefaultUnits(components, minutes, defaultUnit);
		        }

				if (components.dayFound > 1 || components.hourFound > 1 || components.minFound > 1 || components.secFound > 1)
		        {
					throw new AutoChemException(string.Format(InvalidTimeDuplicatedParts, timeSpanValue));
		        }
	        }
	        catch (AutoChemException ex)
	        {
				Trace.TraceError(ex.ToString());
		        throw;
	        }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format(InvalidTimeFormat, timeSpanValue, ex));
                throw new AutoChemException(InvalidTimeFormat);
            }

            // Note we are doing this manually to support more than 24hours, or more than 60 minutes, or seconds.
            return components.BuildTimeSpan();
        }
        /// <summary>
        /// Parse a string formatted in HH:MM:ss or 1hr5min3sec into a TimeSpan
        /// </summary>
        /// <param name="timeSpanValue">string to parse</param>
        /// <returns>ok or failed to parse</returns>
        /// <param name="truncateSubSecond">true to indicate dropping subsecond values for the returned time span.
        /// False to include the subsecond value</param>
        /// <param name="parsedTimeSpan">Newly parsed Time Span</param>
        public static bool TryParseTimeSpan(string timeSpanValue, bool truncateSubSecond, out TimeSpan parsedTimeSpan)
        {
            return TryParseTimeSpan(timeSpanValue, truncateSubSecond, null, out parsedTimeSpan);
        }

        /// <summary>
        /// Parse a string formatted in HH:MM:ss or 1hr5min3sec into a TimeSpan
        /// </summary>
        /// <param name="timeSpanValue">string to parse</param>
        /// <returns>ok or failed to parse</returns>
        /// <param name="truncateSubSecond">true to indicate dropping subsecond values for the returned time span.
        /// False to include the subsecond value</param>
        /// <param name="defaultUnit">The default unit for times given without a unit.  This would typically be TimeSpan.FromMinutes(1) or TimeSpan.FromHours(1).
        /// If defaultUnit is not specified (null) then 1 minute will be used as the default unit.</param>
        /// <param name="parsedTimeSpan">Newly parsed Time Span</param>
        public static bool TryParseTimeSpan(string timeSpanValue, bool truncateSubSecond, TimeSpan? defaultUnit, out TimeSpan parsedTimeSpan)
        {
            parsedTimeSpan = TimeSpan.Zero;
            TimeSpanComponents components = new TimeSpanComponents();

            try
            {
                components.GetComponents(timeSpanValue, truncateSubSecond);

                if ((components.dayFound == 0) && (components.hourFound == 0) && (components.minFound == 0) && (components.secFound == 0))
                {
	                double minutes;
	                if (!TimeSpanComponents.TryConvertValue(timeSpanValue, 2, out minutes))
	                {
		                return false;
	                }

					GetTimeWithDefaultUnits(components, minutes, defaultUnit);
                }

				if (components.dayFound > 1 || components.hourFound > 1 || components.minFound > 1 || components.secFound > 1)
                {
                    return false;
                }
            }
            catch (Exception exception)
            {
                GC.KeepAlive(exception);
                return false;
            }

            // Note we are doing this manually to support more than 24hours, or more than 60 minutes, or seconds.
            parsedTimeSpan= components.BuildTimeSpan();
            return true;
        }

        private static void GetTimeWithDefaultUnits(TimeSpanComponents components, double parsedMinutes, TimeSpan? defaultUnit)
        {
            if (defaultUnit.HasValue)
            {
                var timeSpan = defaultUnit.Value.Multiply(parsedMinutes);
                components.minutes = timeSpan.TotalMinutes;
            }
            else
            {
                components.minutes = parsedMinutes;
            }
        }

        private class TimeSpanComponents
        {
            public double days = 0, hours = 0, minutes = 0, seconds = 0;
            public int dayFound = 0, hourFound = 0, minFound = 0, secFound = 0;
            private bool negative = false;

            public TimeSpan BuildTimeSpan()
            {
                long ticks = (long)
                             (days * TimeSpan.TicksPerDay +
                              hours * TimeSpan.TicksPerHour +
                              minutes * TimeSpan.TicksPerMinute +
                              seconds * TimeSpan.TicksPerSecond);
                if (negative)
                {
                    ticks *= -1;
                }

                TimeSpan timeSpan = new TimeSpan(ticks);

                return timeSpan;
            }

            public void GetComponents(string timeSpanValue, bool truncateSubSecond)
            {
				if (string.IsNullOrWhiteSpace(timeSpanValue))
				{
					Trace.TraceError(string.Format("{0} '{1}'", InvalidTimeFormat, timeSpanValue == null ? "null" : string.Empty));
					throw new AutoChemException(InvalidTimeFormat);
				}

				// Currently we are parsing decimal values separated with a dot character only.

                // The first line of the expression looks for a negative sign and applies with either the
                // second or third line.
                // The second line matches expressions of the form 1hr2min, 5min30sec, ...
                // The third line matches expressions of the form 1:20:30 or 1:15
                Regex regex1 = new Regex(
                    @"^\s*(?<negative>-)?(" +
                    @"((?<days>\.?\d+\.?\d*)\s*(?:d|day|days))?\s*" +
                    @"((?<hours>\.?\d+\.?\d*)\s*(?:h|hr|hour|hours))?\s*" +
                    @"((?<minutes>\.?\d+\.?\d*)\s*(?:m|min|minute|minutes))?\s*" +
                    @"((?<seconds>\.?\d+\.?\d*)\s*(?:s|sec|second|seconds))?\s*$" +
                    @"|(?:(?:(?:(?<days>\d+)\.)?(?<hours>\d+):)?(?<minutes>\d+):(?<seconds>\d+\.?\d*))$)",
                    RegexOptions.IgnoreCase);

                MatchCollection myMatches = regex1.Matches(timeSpanValue);

                foreach (Match match in myMatches)
                {
                    if (match.Groups["negative"].Success)
                    {
                        negative = true;
                    }

                    if (match.Groups["days"].Success)
                    {
						days = ConvertValue(match.Groups["days"].Value, 2);
                        dayFound++;
                    }

                    if (match.Groups["hours"].Success)
                    {
						hours = ConvertValue(match.Groups["hours"].Value, 2);
                        hourFound++;
                    }

                    if (match.Groups["minutes"].Success)
                    {
						minutes = ConvertValue(match.Groups["minutes"].Value, 2);
                        minFound++;
                    }

                    if (match.Groups["seconds"].Success)
                    {
						seconds = ConvertValue(match.Groups["seconds"].Value, truncateSubSecond ? 0 : 3);
	                    secFound++;
                    }
                }
            }

	        /// <summary>
	        /// Converts decimal string into double value.
	        /// !!! This method was introduced because previous attempts to just parse decimal strings using double.Parse() failed on
	        /// German OS.
	        /// String such "1.5h" parsed with double.Parse() will be parsed into 15 hours on German OS because dot character separates
	        /// thousands and not decimals.
	        /// This method parses integer and decimal parts separately.
	        /// </summary>
	        /// <param name="value"></param>
	        /// <param name="decimalLength"></param>
			/// <param name="valueOut"></param>
	        /// <returns></returns>
	        static public bool TryConvertValue(string value, int decimalLength, out double valueOut)
	        {
		        valueOut = 0;

		        try
		        {
                    string[] splittedValue = value.Trim().Split('.', ':', ' ');

                    if (splittedValue.Length > 2)
                    {
                        return false;
                    }

                    valueOut = ConvertValue(value, decimalLength);
			        return true;
		        }
		        catch (Exception)
		        {
			        return false;
		        }
	        }

			/// <summary>
			/// Converts decimal string into double value.
			/// !!! This method was introduced because previous attempts to just parse decimal strings using double.Parse() failed on
			/// German OS.
			/// String such "1.5h" parsed with double.Parse() will be parsed into 15 hours on German OS because dot character separates
			/// thousands and not decimals.
			/// This method parses integer and decimal parts separately.
			/// </summary>
			/// <param name="value"></param>
			/// <param name="decimalLength"></param>
			/// <returns></returns>
	        static public double ConvertValue(string value, int decimalLength)
	        {
				string[] splittedValue = value.Trim().Split('.', ':', ' ');

				if (splittedValue.Length > 2)
				{
					// No need to trace this error because if it fails here than the value should go through datetime parser
					throw new ArgumentException(string.Format("Invalid time value: {0}. The value must be in decimal or integer format.", value), "value");
				}

				double valueOut = splittedValue[0] == string.Empty ? 0 : int.Parse(splittedValue[0]);

		        if (splittedValue.Length == 1)
		        {
			        return valueOut;
		        }

				if (splittedValue.Length == 2 && decimalLength > 0)
		        {
			        // We will do conversion with extra digit in order to round it later.
					string decimalPartString = splittedValue[1].PadRight(decimalLength + 1, '0').Substring(0, decimalLength + 1);

			        // Parse to integer
			        int decimalPart = int.Parse(decimalPartString);

			        // Divide decimal part by 10 in power of the length of the decimal string.
			        // The output will look like we placed a 0 (zero) in front of decimal part without worring locale decimal character.
			        double decimalValue = decimalPart/System.Math.Pow(10, decimalLength + 1);

			        // Round this to requested length

					valueOut += System.Math.Round(decimalValue, decimalLength);
		        }

				return valueOut;
	        }
        }
    }
}
