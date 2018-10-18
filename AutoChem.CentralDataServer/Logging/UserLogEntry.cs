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
**    Copyright © 2012 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using AutoChem.Core.CentralDataServer.Experiments;
using AutoChem.Core.Generics;

namespace AutoChem.Core.CentralDataServer.Logging
{
    /// <summary>
    /// Holds the information for an entry in the user log.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class UserLogEntry
    {
        private ExperimentSourceReference m_InstrumentOrApplication;

        /// <summary>
        /// Used for deserialization.
        /// </summary>
        public UserLogEntry()
        {
        }

#if !SILVERLIGHT
        internal UserLogEntry(int code, UserLogEntryType type, string message)
        {
            Timestamp = DateTime.UtcNow;
            Code = code;
            Type = type;
            Message = message;
            UserLogEntry_Common();
        }

        internal UserLogEntry(int code, UserLogEntryType type, ExperimentReference experiment, string message)
        {
            Timestamp = DateTime.UtcNow;
            Code = code;
            Type = type;
            Experiment = experiment;
            Message = message;
            UserLogEntry_Common();
        }

        internal UserLogEntry(int code, UserLogEntryType type, ExperimentSourceReference instrumentOrApplication, string message)
        {
            Timestamp = DateTime.UtcNow;
            Code = code;
            Type = type;
            InstrumentOrApplication = instrumentOrApplication;
            Message = message;
            UserLogEntry_Common();
        }
#endif

        private void UserLogEntry_Common()
        {
            if (Type == UserLogEntryType.Info)
            {
                Resolved = true;
            }
        }
        
        /// <summary>
        /// The time the entry was recorded.
        /// </summary>
        [DataMember]
        public DateTime Timestamp
        {
            get;
            set;
        }

        /// <summary>
        /// The code for the error or warning.  See UserLongEntryCodes for more details.
        /// This is used to identify repeat or duplicate errors.
        /// </summary>
        [DataMember]
        public int Code
        {
            get;
            set;
        }

        /// <summary>
        /// The type of log entry (info, warning, error)
        /// </summary>
        [DataMember]
        public UserLogEntryType Type { get; set; }

        /// <summary>
        /// A reference to the experiment for which this entry was generated.
        /// </summary>
        [DataMember]
        public ExperimentReference Experiment
        {
            get;
            set;
        }

        /// <summary>
        /// A reference to the instrument or application for which this entry was generated.
        /// </summary>
        [DataMember]
        public ExperimentSourceReference InstrumentOrApplication
        {
            get
            {
                if (Experiment != null)
                {
                    return Experiment.Source;
                }
                else
                {
                    return m_InstrumentOrApplication;
                }
            }
            set 
            {
                Trace.Assert(value == null || Experiment == null || Experiment.Source.Equals(value));
                m_InstrumentOrApplication = value; 
            }
        }

        /// <summary>
        /// The message to the user.
        /// </summary>
        [DataMember]
        public string Message
        {
            get;
            set;
        }

        /// <summary>
        /// True if the user or system has marked the message as resolved.
        /// </summary>
        [DataMember]
        public bool Resolved { get; set; }

        /// <summary>
        /// Indicates who marked the entry as resolved.
        /// </summary>
        [DataMember]
        public string ResolvedBy { get; set; }

        /// <summary>
        /// Do two entries have the same code and source.
        /// </summary>
        /// <param name="other">Entry to compare this entry with</param>
        public bool CodeAndSourceEqual(UserLogEntry other)
        {
            if (Code == UserLogCodes.Uncoded)
            {
                // Don't consider anything with code -1 equal.  This is used for informational messages where a code is not always neccessary.  That is you don't neccessarily want to remove the prior instance of a message just because there is a new one.
                return false;
            }
            if (other.Code != Code) return false;
            if (!Experiment.SafeEquals(other.Experiment))
                return false;
            if (!other.InstrumentOrApplication.SafeEquals(InstrumentOrApplication))
                return false;

            return true;
        }

#if !SILVERLIGHT
        /// <summary>
        /// Return an XML representation of this UserLogEntry
        /// </summary>
        /// <returns>UserLogEntry formatted as an XML string</returns>
        public string ToXmlFragement()
        {
            string entryText = String.Empty;

            try
            {
                DataContractSerializer serializer = new DataContractSerializer(typeof(UserLogEntry));

                StringBuilder stringData = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings() { Indent = true, OmitXmlDeclaration = true };
                using (var xmlWriter = XmlWriter.Create(stringData, settings))
                {
                    serializer.WriteObject(xmlWriter, this);
                    // Need to flush the writer to make sure everything is written out to the string builder.
                    xmlWriter.Flush();
                }
                entryText = stringData.ToString();
            }
            catch (Exception ex)
            {
                Trace.TraceError("Error converting UserLogEntry to XML: " + ex);
            }

            return entryText;
        }
#endif

    }
}
