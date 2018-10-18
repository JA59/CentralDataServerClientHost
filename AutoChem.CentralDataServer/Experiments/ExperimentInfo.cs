/*
**COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**    Copyright © 2011 by Mettler Toledo AutoChemAutoChem.  All rights reserved.
**
**ENDHEADER:
*/

using System;
using System.Runtime.Serialization;
#if !SILVERLIGHT
using AutoChem.Core.CentralDataServer.DataAccess;
#endif
using AutoChem.Core.CentralDataServer.Instruments;
using AutoChem.Core.Time;
using AutoChem.Core.CentralDataServer.Applications;
using AutoChem.Core.Generics;

namespace AutoChem.Core.CentralDataServer.Experiments
{
    /// <summary>
    /// Data about an experiment
    /// </summary>
    [DataContract(Namespace=ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class ExperimentInfo : IEquatable<ExperimentInfo>
    {
        private DateTime m_StoredTime;

        private DateTime m_StartTime;
        private DateTime m_EndTime;
        private string m_InstrumentType;


        /// <summary>
        /// Default constructor needed by EntityFramework
        /// </summary>
        public ExperimentInfo()
        {
            ExperimentReference = new ExperimentReference();
        }

#if !SILVERLIGHT
        internal ExperimentInfo(ExperimentRecord record, InstrumentRecord instrumentRecord, ApplicationRecord applicationRecord) : this()
        {
            StartTime = record.StartTime;
            EndTime = record.EndTime;
            StoredTime = record.StoredTime;
            Status = record.Status;
            UserDefinedField = record.UserDefinedField;
            InstrumentType = record.InstrumentType;

            ExperimentReference.Name = record.Name;
            ExperimentReference.UncorrectedStartTime = record.UncorrectedStartTime;
            ExperimentReference.User = record.User;
            if (instrumentRecord != null)
            {
                ExperimentReference.Source = new InstrumentReference(instrumentRecord);
            }
            else if (applicationRecord != null)
            {
                ExperimentReference.Source = new ApplicationReference(applicationRecord);
            }
        }
#endif

        /// <summary>
        /// The ExperimentReference object
        /// </summary>
        [DataMember]
        public ExperimentReference ExperimentReference { get; set; }

        /// <summary>
        /// The time the experiment was first stored and registered with the system.
        /// </summary>
        [DataMember]
        public DateTime StoredTime
        {
            get { return m_StoredTime; }
            set { m_StoredTime = TimingUtility.GetUniversalTime(value); }
        }

        /// <summary>
        /// The corrected start time for the experiment.  The correction is calculate based on the time difference between the server and the instrument.
        /// </summary>
        [DataMember]
        public DateTime StartTime
        {
            get { return m_StartTime; }
            set { m_StartTime = TimingUtility.GetUniversalTime(value); }
        }

        /// <summary>
        /// The corrected end time for the experiment.
        /// </summary>
        [DataMember]
        public DateTime EndTime
        {
            get { return m_EndTime; }
            set { m_EndTime = TimingUtility.GetUniversalTime(value); }
        }

        /// <summary>
        /// Current status of the experiment
        /// </summary>
        [DataMember]
        public ExperimentStatus Status { get; set; }

        /// <summary>
        /// The value for the user defined field if there is one.
        /// </summary>
        [DataMember]
        public string UserDefinedField { get; set; }

        /// <summary>
        /// The type of instrument used in the experiment
        /// </summary>
        [DataMember]
        public string InstrumentType
        {
            get { return m_InstrumentType; }
            set { m_InstrumentType = value; }
        }

        /// <summary>
        /// Returns true of the Experiment Infos are equal.
        /// </summary>
        public bool Equals(ExperimentInfo other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            else
            {
                return (ExperimentReference.Equals(other.ExperimentReference) &&
                    StoredTime == other.StoredTime && StartTime == other.StartTime && EndTime == other.EndTime &&
                    Status == other.Status && UserDefinedField == other.UserDefinedField);
            }
        }
    }
}
