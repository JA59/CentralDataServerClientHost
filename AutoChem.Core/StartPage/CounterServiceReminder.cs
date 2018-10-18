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
**    Copyright © 2008 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Runtime.Serialization;

namespace AutoChem.Core.StartPage
{
    /// <summary>
    /// Service reminder that uses a counter (i.e. scraper blade count)
    /// instead of a time-based reminder.
    /// </summary>
    [DataContract]
    [KnownType(typeof (ServiceReminder))]
    [KnownType(typeof (CounterServiceReminder))]
    public class CounterServiceReminder : ServiceReminder
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType">Unique identifier for a type of service activity.</param>
        /// <param name="serviceInterval">Interval (counts) before service is required.</param>
        /// <param name="preServiceDelta">Counts prior to the interval that service is almost due.</param>
        /// <param name="postServiceDelta">Counts after the interval that service is past due.</param>
        /// <param name="reminderText">Text to display in the reminder on the start page.</param>
        /// <param name="fullText">Full text to display on the service form.</param>
        /// <param name="helpFile">Help file containing associated help.</param>
        /// <param name="helpTopic">Help topic for associated help.</param>
        /// <param name="instrumentId">ID of the instrument</param>
        public CounterServiceReminder(string serviceType, int serviceInterval,
                                      int preServiceDelta, int postServiceDelta,
                                      string reminderText, string fullText,
                                      string helpFile, string helpTopic, string instrumentId)
            : base(serviceType,
                   DateTime.MinValue, serviceInterval, preServiceDelta,
                   postServiceDelta, reminderText, fullText,
                   helpFile, helpTopic, instrumentId)
        {
            SetItemCount(0);
        }

        /// <summary>
        /// Current item count.
        /// Gets or set the counter for this reminder.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 0, Name = "ItemCount", IsRequired = true)]
        public int ItemCount { get; set; }

        /// <summary>
        /// Set the item count
        /// </summary>
        /// <param name="count">New item count</param>
        public void SetItemCount(int count)
        {
            ItemCount = count;
            if (count == 0)
            {
                // Reset
                NextServiceDate = DateTime.MaxValue;
            }
            if (IsAlmostDue && (NextServiceDate > DateTime.Today))
            {
                // If almost due, mark it due today
                NextServiceDate = DateTime.Today;
            }
        }

        /// <summary>
        /// Is this reminder past due.
        /// </summary>
        public override bool IsPastDue
        {
            get { return (ItemCount > (ServiceInterval + PostServiceDelta)); }
        }

        /// <summary>
        /// Is this reminder soon to come due.
        /// </summary>
        public override bool IsAlmostDue
        {
            get { return (ItemCount > (ServiceInterval - PreServiceDelta)); }
        }
    }
}
