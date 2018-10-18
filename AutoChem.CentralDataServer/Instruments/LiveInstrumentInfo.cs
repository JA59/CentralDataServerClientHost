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
**    Copyright © 2011 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using AutoChem.Core.CentralDataServer.Experiments;

namespace AutoChem.Core.CentralDataServer.Instruments
{
    /// <summary>
    /// Represents the live status of the instrument
    /// </summary>
    [DataContract]
    public class LiveInstrumentInfo : IEquatable<LiveInstrumentInfo>
    {
        /// <summary>
        /// Creates a new LiveInstrumentInfo.
        /// </summary>
        public LiveInstrumentInfo()
        {
        }

        /// <summary>
        /// Creates a new LiveInstrumentInfo with the provided InstrumentInfo
        /// </summary>
        public LiveInstrumentInfo(InstrumentInfo instrumentInfo)
        {
            InstrumentInfo = instrumentInfo;
        }

        /// <summary>
        /// The address of the host.
        /// </summary>
        public string HostAddress
        {
            get { return InstrumentInfo.HostAddress; }
            set { InstrumentInfo.HostAddress = value; }
        }

        /// <summary>
        /// The status of the instrument.
        /// </summary>
        public InstrumentStatus Status
        {
            get { return InstrumentInfo.Status; }
        }

        /// <summary>
        /// The generally static information about the instrument.
        /// </summary>
        [DataMember]
        public InstrumentInfo InstrumentInfo { get; set; }

        /// <summary>
        /// The time difference between server and the instrument.
        /// </summary>
        [DataMember]
        public TimeSpan? TimeDifference { get; set; }

        /// <summary>
        /// The last time the system successfully communicated with the instrument.
        /// </summary>
        [DataMember]
        public DateTime? LastSuccessfulCommunication
        {
            get { return InstrumentInfo.LastSuccessfulCommunication; }
            set
            {
                if (value.HasValue)
                {
                    InstrumentInfo.LastSuccessfulCommunication = value.Value.ToUniversalTime();
                }
            }
        }

        /// <summary>
        /// The experiments currently being downloaded by the instrument.
        /// </summary>
        [DataMember]
        public IEnumerable<ExperimentInfo> DownloadingExperiments { get; set; }

        /// <summary>
        /// Counts unsuccessful attempts to contact instrument
        /// </summary>
        [DataMember]
        public int ErrorCount { get; set; }

        /// <summary>
        /// True if the instrument has one or more errors
        /// </summary>
        [DataMember]
        public bool InstrumentHasErrors { get; set; }

        /// <summary>
        /// True if the instrument has one or more warnings
        /// </summary>
        [DataMember]
        public bool InstrumentHasWarnings { get; set; }        
        
        /// <summary>
        /// The text to display for Reactor 1 Value
        /// </summary>
        [DataMember]
        public string Reactor1Value { get; set; }

        /// <summary>
        /// The text to display for Reactor 2 Value
        /// </summary>
        [DataMember]
        public string Reactor2Value { get; set; }

        /// <summary>
        /// Returns true if the LiveInstrumentInfo has the same information.
        /// </summary>
        public bool Equals(LiveInstrumentInfo other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            else
            {
                return InstrumentInfo.Equals(other.InstrumentInfo) && Status == other.Status &&
                    TimeDifference == other.TimeDifference && LastSuccessfulCommunication == other.LastSuccessfulCommunication &&
                    ErrorCount == other.ErrorCount && DownloadingExperimentsEqual(other);
            }
        }

        private bool DownloadingExperimentsEqual(LiveInstrumentInfo other)
        {
            if (DownloadingExperiments == null || other.DownloadingExperiments == null)
            {
                return (DownloadingExperiments == null) && (other.DownloadingExperiments == null);
            }
            else
            {
                return DownloadingExperiments.SequenceEqual(other.DownloadingExperiments);
            }
        }
    }
}
