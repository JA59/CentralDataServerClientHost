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
    /// A service reminder is a reminder (for the start page) that relates to the
    /// need for servicing.  The reminder has a due date (when the next service is due,) and
    /// a service interval (how often the service must be performed.)  A pre-service delta
    /// determines how soon before the due date the reminder should appear in the start page.
    /// A post-service delta determines how long after the due date (overdue) before the
    /// reminder is displayed as critical.
    /// </summary>
    [DataContract]
    [KnownType(typeof(ServiceReminder))]
    [KnownType(typeof(EquipmentServiceReminder))]
    [KnownType(typeof(CounterServiceReminder))]
    public class ServiceReminder
    {

        private string m_ServiceType;
        private DateTime m_NextServiceDate;
        private int m_ServiceInterval;
        private int m_PreServiceDelta;
        private int m_PostServiceDelta;
        private string m_ReminderText;
        private string m_FullText;
        private string m_HelpFile;
        private string m_HelpTopic;
        private string m_InstrumentID;
        private string m_captionText = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType">Unique identifier for a type of service activity.</param>
        /// <param name="nextServiceDate">Due date for the next servicing.</param>
        /// <param name="serviceInterval">Interval between services in days.</param>
        /// <param name="preServiceDelta">Days prior to service date when an information reminder is required.</param>
        /// <param name="postServiceDelta">Days after service date until a warning reminder is required.</param>
        /// <param name="reminderText">Text to display in the reminder on the start page.</param>
        /// <param name="fullText">Full text to display on the service form.</param>
        /// <param name="helpFile">Help file containing associated help.</param>
        /// <param name="helpTopic">Help topic for associated help.</param>
        public ServiceReminder(string serviceType,
            DateTime nextServiceDate, int serviceInterval, int preServiceDelta,
            int postServiceDelta, string reminderText, string fullText,
            string helpFile, string helpTopic)
        {
            m_ServiceType = serviceType;
            m_NextServiceDate = nextServiceDate;
            m_ServiceInterval = serviceInterval;
            m_PreServiceDelta = preServiceDelta;
            m_PostServiceDelta = postServiceDelta;
            m_ReminderText = reminderText;
            m_FullText = fullText;
            m_HelpFile = helpFile;
            m_HelpTopic = helpTopic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceType">Unique identifier for a type of service activity.</param>
        /// <param name="nextServiceDate">Due date for the next servicing.</param>
        /// <param name="serviceInterval">Interval between services in days.</param>
        /// <param name="preServiceDelta">Days prior to service date when an information reminder is required.</param>
        /// <param name="postServiceDelta">Days after service date until a warning reminder is required.</param>
        /// <param name="reminderText">Text to display in the reminder on the start page.</param>
        /// <param name="fullText">Full text to display on the service form.</param>
        /// <param name="helpFile">Help file containing associated help.</param>
        /// <param name="helpTopic">Help topic for associated help.</param>
        /// <param name="instrumentId">ID of the instrument</param>
        public ServiceReminder(string serviceType,
            DateTime nextServiceDate, int serviceInterval, int preServiceDelta,
            int postServiceDelta, string reminderText, string fullText,
            string helpFile, string helpTopic, string instrumentId) :
            this(serviceType, nextServiceDate, serviceInterval, preServiceDelta, postServiceDelta, reminderText, fullText, helpFile, helpTopic)
        {
            m_InstrumentID = instrumentId;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="serviceType">Unique identifier for a type of service activity.</param>
        /// <param name="nextServiceDate">Due date for the next servicing.</param>
        /// <param name="serviceInterval">Interval between services in days.</param>
        /// <param name="preServiceDelta">Days prior to service date when an information reminder is required.</param>
        /// <param name="postServiceDelta">Days after service date until a warning reminder is required.</param>
        /// <param name="reminderText">Text to display in the reminder on the start page.</param>
        /// <param name="fullText">Full text to display on the service form.</param>
        /// <param name="helpFile">Help file containing associated help.</param>
        /// <param name="helpTopic">Help topic for associated help.</param>
        /// <param name="instrumentId">ID of the instrument</param>
        /// <param name="captionText">Caption (if any) to show on the start page</param>
        public ServiceReminder(string serviceType,
            DateTime nextServiceDate, int serviceInterval, int preServiceDelta,
            int postServiceDelta, string reminderText, string fullText,
            string helpFile, string helpTopic, string instrumentId, string captionText):
            this(serviceType, nextServiceDate, serviceInterval, preServiceDelta, postServiceDelta, reminderText, fullText, helpFile, helpTopic, instrumentId)
        {
            m_captionText = captionText;
        }

        /// <summary>
        /// Unique identifier for the service reminder.
        /// </summary>
        public virtual string ID
        {
            get { return m_ServiceType; }
        }

        /// <summary>
        /// Unique identifier for the type of service activity this reminder is for.
        /// </summary>
        [DataMember(Order = 0, Name = "ServiceType", IsRequired = true)]
        public string ServiceType
        {
            get { return m_ServiceType; }
#if !SILVERLIGHT
            private
#endif
            set { m_ServiceType = value; }
        }

        /// <summary>
        /// Is this reminder past due.
        /// </summary>
        public virtual bool IsPastDue
        {
            get
            {
                DateTime pastDue = m_NextServiceDate + TimeSpan.FromDays(m_PostServiceDelta);
                if (DateTime.Now > pastDue) return true;
                return false;
            }
        }

        /// <summary>
        /// Is this reminder soon to come due.
        /// </summary>
        public virtual bool IsAlmostDue
        {
            get
            {
                DateTime almostDue = m_NextServiceDate;
                if (almostDue != DateTime.MinValue)
                {
                    almostDue -= TimeSpan.FromDays(m_PreServiceDelta);
                }

                if (DateTime.Now > almostDue) return true;
                return false;
            }
        }

        /// <summary>
        /// Due date for the next service.
        /// Gets or set the next service date for this reminder.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 1, Name = "NextServiceDate", IsRequired = true)]
        public DateTime NextServiceDate
        {
            get { return m_NextServiceDate; }
            set { m_NextServiceDate = value; }
        }

        /// <summary>
        /// Interval between services in days.
        /// Gets or set the service interval.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 2, Name = "ServiceInterval", IsRequired = true)]
        public int ServiceInterval
        {
            get { return m_ServiceInterval; }
            set { m_ServiceInterval = value; }
        }

        /// <summary>
        /// Days prior to service date when an information reminder is required.
        /// Gets or set the pre service reminder period.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 3, Name = "PreServiceDelta", IsRequired = true)]
        public int PreServiceDelta
        {
            get { return m_PreServiceDelta; }
            set { m_PreServiceDelta = value; }
        }

        /// <summary>
        /// Days after service date until a warning reminder is required.
        /// Gets or set the post date warning period.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 4, Name = "PostServiceDelta", IsRequired = true)]
        public int PostServiceDelta
        {
            get { return m_PostServiceDelta; }
            set { m_PostServiceDelta = value; }
        }

        /// <summary>
        /// Text to display in the reminder on the start page.
        /// Gets or set the reminder text.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 5, Name = "ReminderText", IsRequired = true)]
        public string ReminderText
        {
            get { return m_ReminderText; }
            set { m_ReminderText = value; }
        }

        /// <summary>
        /// Full text to display on the service form.
        /// Gets or set the full, long text explanation.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 6, Name = "FullText", IsRequired = true)]
        public string FullText
        {
            get { return m_FullText; }
            set { m_FullText = value; }
        }

        /// <summary>
        /// Help file containing associated help.
        /// Gets or set the help file.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 7, Name = "HelpFile", IsRequired = true)]
        public string HelpFile
        {
            get { return m_HelpFile; }
            set { m_HelpFile = value; }
        }

        /// <summary>
        /// Help topic for associated help.
        /// Gets or set the help topic.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 8, Name = "HelpTopic", IsRequired = true)]
        public string HelpTopic
        {
            get { return m_HelpTopic; }
            set { m_HelpTopic = value; }
        }

        /// <summary>
        /// Gets or set the instrument ID, some way to uniquely identify the instrument.
        /// The property is public so it can be serialized in Silverlight
        /// </summary>
        [DataMember(Order = 9, Name = "InstrumentID", IsRequired = false)]
        public string InstrumentID
        {
            get { return m_InstrumentID; }
            set{ m_InstrumentID = value;}
        }

        /// <summary>
        /// Gets or set the caption text, so we can show appropriate text on the Start Page.
        /// The property is public so it can be serialized in Silverlight
        /// Before this property was added we always used to display the service type.  Therefore, if the 
        /// caption text is not set, return the service type.
        /// </summary>
        [DataMember(Order = 9, Name = "CaptionText", IsRequired = false)]
        public string CaptionText
        {
            get
            {
                if (m_captionText == string.Empty)
                {
                    return m_ServiceType;
                }

                return m_captionText;
            }
            set { m_captionText = value; }
        }

        /// <summary>
        /// Overrides the inherited method.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ReminderText;
        }
    }
}
