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
using AutoChem.Core.Generics;
using AutoChem.Core.Time;
using System.Runtime.Serialization;

namespace AutoChem.Core.CentralDataServer.Experiments
{
    /// <summary>
    /// A reference to a particular experiment.
    /// </summary>
    [DataContract(Namespace=ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class ExperimentReference : IEquatable<ExperimentReference>
    {
        private string m_User;
        private DateTime m_UncorrectedStartTime;

        /// <summary>
        /// Default constructor needed by EntityFramework
        /// </summary>
        public ExperimentReference()
        {}
        
        /// <summary>
        /// Creates a new experiment reference.
        /// </summary>
        public ExperimentReference(string name, string user, DateTime uncorrectedStartTime, ExperimentSourceReference source)
        {
            Name = name;
            User = user;
            m_UncorrectedStartTime = uncorrectedStartTime.ToUniversalTime();
            Source = source;
        }

        /// <summary>
        /// The name of the experiment
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The user who executed the experiment.
        /// </summary>
        [DataMember]
        public string User
        {
            get { return m_User; }
            set
            {
                 m_User = value ?? string.Empty;
            }
        }

        /// <summary>
        /// The start time as originally recorded by the instrument or application wihtout any correction applied.
        /// This is needed becase the amount of correction could very, but the uncorrected time will be constant and 
        /// can be used for checking if an experiment has already been collected.
        /// </summary>
        [DataMember]
        public DateTime UncorrectedStartTime
        {
            get { return m_UncorrectedStartTime; }
            set { m_UncorrectedStartTime = TimingUtility.GetUniversalTime(value); }
        }

        /// <summary>
        /// The source of the experiment (e.g. an instrument or application).
        /// </summary>
        [DataMember]
        public ExperimentSourceReference Source { get; set; }

        /// <summary>
        /// Returns a hash code that takes into account the name, user, and start time.
        /// </summary>
        public override int GetHashCode()
        {
            // ToString on start time is to be consistent with equals.
            return Name.ValueOrDefault(name => name.ToLower().GetHashCode()) ^ User.ValueOrDefault(user => user.ToLower().GetHashCode()) ^ 
                UncorrectedStartTime.ToString().GetHashCode() ^ Source.ValueOrDefault(source => source.GetHashCode());
        }

        private int GetHashCode(string str)
        {
            return str.ToLower().GetHashCode();
        }

        /// <summary>
        /// Returns true if the other experiment reference is equal to this one.
        /// </summary>
        public override bool Equals(object obj)
        {
            ExperimentReference otherReference = obj as ExperimentReference;
            return Equals(otherReference);
        }

        /// <summary>
        /// Returns true if the other experiment reference is equal to this one.
        /// </summary>
        public bool Equals(ExperimentReference otherReference)
        {
            if (ReferenceEquals(otherReference, null))
            {
                return false;
            }
            else
            {
                return (Name ?? string.Empty).ToLower() == (otherReference.Name ?? string.Empty).ToLower() &&
                    (User ?? string.Empty).ToLower() == (otherReference.User ?? string.Empty).ToLower() &&
                    UncorrectedStartTime.ToString() == otherReference.UncorrectedStartTime.ToString() && // I am not sure why we have StartTime.ToString, but it maybe the time returned from the database is not exactly what we put in the database.
                    Source.SafeEquals(otherReference.Source);
            }
        }
    }
}
