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
using System.Runtime.Serialization;

using System.Diagnostics;

namespace AutoChem.Core.Time
{
	/// <summary>
	/// Summary description for TimeRange.
	/// </summary>

	public class TimeRange : IComparable
	{
		private const int Version = 1;

		private DateTime start = DateTime.MinValue;
		private DateTime end   = DateTime.MaxValue;

		private readonly static string FormatString = "H:mm:ss.fff";

        /// <summary>
        /// Creates a new time range covering all valid time.
        /// </summary>
		public TimeRange()
		{
		}
		
        /// <summary>
        /// Creates a new time range from another time range.
        /// </summary>
        /// <param name="timeRange"></param>
		public TimeRange(TimeRange timeRange) : this(timeRange.Start, timeRange.End)
		{
		}
		
        /// <summary>
        /// Creates a new time range with the specified start time and length.
        /// </summary>
		public TimeRange(DateTime startTime, TimeSpan interval) : this(startTime, startTime + interval)
		{
		}
		
        /// <summary>
        /// Creates a new time range with the specified start and end time.
        /// </summary>
		public TimeRange(DateTime start, DateTime end)
		{
			if (start > end)
			{
				throw new ArgumentException(string.Format("Start time ({0}) occurs after the end time ({1})", start, end));
			}

            if (start.Kind != end.Kind)
            {
                if (start.Kind == DateTimeKind.Unspecified)
                {
                    start = new DateTime(start.Ticks, end.Kind);
                }
                else if (end.Kind == DateTimeKind.Unspecified)
                {
                    end = new DateTime(end.Ticks, start.Kind);
                }
                else
                {
                    string message = string.Format("TimeRange: The start and end time should be of the same kind ({0} != {1})", start.Kind, end.Kind);
                    Debug.WriteLine(message);

                }
            }

			this.start = start;
			this.end = end;
		}
		
        /// <summary>
        /// Returns a new time range with the time included in both ranges.
        /// Or returns null if the ranges do not intersect.
        /// </summary>
		public TimeRange Intersect(TimeRange range2)
		{
			if ( this.Overlaps(range2))
			{
				DateTime intersectionStart = TimeRange.DateTimeMax(this.start, range2.Start);
				DateTime intersectionEnd = TimeRange.DateTimeMin(this.end, range2.End);
				return new TimeRange(intersectionStart, intersectionEnd);
			}
			else
			{
				return null;
			}
		}

        /// <summary>
        /// Returns a time range that includes the time of both time ranges.
        /// </summary>
		public TimeRange Union(TimeRange newRange)
		{
			return Union(this, newRange);
		}

		/// <summary>
		/// Changes either the start or end to the specified time
		/// If the specified time is within the range, nothing
		/// is changed.
		/// </summary>
		/// <param name="time"></param>
		public void Extend(DateTime time)
		{
			if (time < this.start)
			{
				this.start = time;
			}
			else if (time > this.end)
			{
				this.end = time;
			}
		}

		/// <summary>
		/// Returns a new Time range object that encompasses the
		/// ranges of the TimeRanges passed as arguments
		/// </summary>
		public static TimeRange Union(TimeRange range1, TimeRange range2)
		{
			if (range1 == null && range2 == null)
			{
				throw new ArgumentNullException("Both of the supplied TimeRange objects are null.");
			}

			if (range1 == null)
			{
				return new TimeRange(range2.Start, range2.End);
			}

			if (range2 == null)
			{
				return new TimeRange(range1.Start, range1.End);
			}

			DateTime earliest;
			DateTime latest;

			if (range1.Start < range2.Start)
				earliest = range1.Start;
			else
				earliest = range2.Start;

			if (range1.End > range2.End)
				latest = range1.End;
			else
				latest = range2.End;

			return new TimeRange(earliest, latest);
		}

        /// <summary>
        /// The start time of the time range.
        /// </summary>
		public DateTime Start 
		{
			get {return start;}
			set 
			{
				if (value > end)
				{
					throw new ArgumentException(string.Format("Supplied start time ({0}) occurs after the end time ({1})", value, end));
				}

				start = value;
			}
		}

        /// <summary>
        /// The end time of the time range.
        /// </summary>
		public DateTime End 
		{
			get {return end;}
			set 
			{
				if (value < start)
				{
					throw new ArgumentException(string.Format("Supplied endTime ({0}) occurs before the start time ({1})", value, start));
				}
				end = value;
			}
		}

        /// <summary>
        /// Returns the kind of times for the times.
        /// </summary>
        public DateTimeKind DateTimeKind
        {
            get { return start.Kind; }
        }

        /// <summary>
        /// Converts the time range to universal times.
        /// </summary>
        public TimeRange ToUniversalTime()
        {
            return new TimeRange(start.ToUniversalTime(), end.ToUniversalTime());
        }

        /// <summary>
        /// Converts the time range to local times.
        /// </summary>
        public TimeRange ToLocalTime()
        {
            return new TimeRange(start.ToLocalTime(), end.ToLocalTime());
        }

        /// <summary>
        /// Returns a string representation of the time range.
        /// </summary>
		public override string ToString()
		{
			return start.ToString(FormatString) + " to " + end.ToString(FormatString);
		}

		/// <summary>
		/// Compares to time ranges by thier start times and then by thier end times.
        /// Enables a list of time ranges to be sorted.
		/// </summary>
		public int CompareTo(object obj)
		{
			TimeRange timeRange = obj as TimeRange;
			if (timeRange == null)
			{
				throw new ArgumentException("Cannnot compare a TimeRange object to an object that is not a TimeRange.");
			}
			int result = this.Start.CompareTo(timeRange.start);
            if (result == 0)
            {
                result = end.CompareTo(timeRange.end);
            }

            return result;
		}

        /// <summary>
        /// Returns true if this time range contains the time range passed in.
        /// </summary>
		public bool Engulfs(TimeRange testRange)
		{
			if (testRange.start >= this.start && testRange.end <= this.end)
			{
				return true;
			}
			return false;
		}

        /// <summary>
        /// Returns true if the time ranges overlap at all.
        /// </summary>
        public bool Overlaps(TimeRange testRange)
        {
            if (DateTimeMin(this.end, testRange.End) >= DateTimeMax(this.start, testRange.Start))
            {
                return true;
            }

            return false;
        }
        /// <summary>
        /// Returns true if the time range contains the specfied date.
        /// </summary>
        public bool Contains(DateTime time)
        {
            if (time >= this.Start && time <= this.End)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Returns true if the time range contains the specfied date.
        /// </summary>
        public bool Contains(TimeRange timerange)
        {
            if (Contains(timerange.start) && Contains(timerange.End))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the minimum of two dates.
        /// </summary>
		public static DateTime DateTimeMin(DateTime dateTime1, DateTime dateTime2)
		{
			if(dateTime1 <= dateTime2)
				return dateTime1;
			else
				return dateTime2;
		}

        /// <summary>
        /// Returns the maximum of two dates.
        /// </summary>
        public static DateTime DateTimeMax(DateTime dateTime1, DateTime dateTime2)
		{
			if(dateTime1 >= dateTime2)
				return dateTime1;
			else
				return dateTime2;
		}

        /// <summary>
        /// Changes the start time by -inflateBy (When inflateBy is positive the start time is made earlier by inflateBy)
        /// Changes the end time by inflateBy (When inflateBy is positive the end time is made later by inflateBy)
        /// The length of the TimeRange will be increased by 2 * inflateBy (this may actually be a decrease if inflateBy is negative).
        /// Note this does not return a new time range but actually modifies this instance.
        /// </summary>
        /// <returns>This time range.</returns>
		public TimeRange Inflate(TimeSpan inflateBy)
		{
			if (inflateBy < TimeSpan.Zero && inflateBy > this.Length)
			{
				throw new ArgumentOutOfRangeException("inflateBy", "De-flating the timeRange by the specified amount will cause the start time to be after the end time.");
			}

			if (this.start != DateTime.MinValue)
			{
				this.start -= inflateBy;
			}
			if (this.end != DateTime.MaxValue)
			{
				this.end += inflateBy;
			}

			return this;
		}

        /// <summary>
        /// The length of the time range.
        /// </summary>
		public TimeSpan Length
		{
			get { return End - Start; }
		}

        /// <summary>
        /// Returns true if obj is a TimeRange with the same start and end time.
        /// </summary>
        public override bool Equals(object obj)
        {
            TimeRange otherRange = obj as TimeRange;

            bool otherRangeNotNull = !ReferenceEquals(otherRange, null);
            if (otherRangeNotNull)
            {
                return (Start == otherRange.Start && End == otherRange.End);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Returns a hash code based on the start and end time.
        /// </summary>
        public override int GetHashCode()
        {
            return (Start.GetHashCode() ^ End.GetHashCode());
        }
		
	}
}
