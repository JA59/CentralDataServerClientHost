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
**    Copyright © 2018 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AutoChem.Core.DataAccess.Metadata
{
    /// <summary>
    /// Class that contains metadata of the planned experiment.
    /// </summary>
    [Serializable]
    [DataContract]
    public class PlannedExperimentMetadata
    {
        /// <summary>
        /// Creates a new PlannedExpeirmentMetadata
        /// </summary>
        public PlannedExperimentMetadata()
        {
            PlannedExperimentSelectedStages = new List<S88StageMetadata>();
            PlannedExperimentOriginalStages = new List<S88StageMetadata>();
        }

        /// <summary>
        /// Planned Experiment User Name
        /// </summary>
        [DataMember]
        public string PlannedExperimentUserID { get; set; }
        /// <summary>
        /// Planned Experiment Project
        /// </summary>
        [DataMember]
        public string PlannedExperimentProject { get; set; }
        /// <summary>
        /// Planned Experiment Name
        /// </summary>
        [DataMember]
        public string PlannedExperimentName { get; set; }

        /// <summary>
        /// The time the planned experiment was created.
        /// </summary>
        [DataMember]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// Planned Experiment Process
        /// </summary>
        [DataMember]
        public string PlannedExperimentProcess { get; set; }

        /// <summary>
        /// Gets or sets the planned experiment unique identifier.
        /// </summary>
        [DataMember]
        public Guid PlannedExperimentUniqueID { get; set; }

        /// <summary>
        /// Planned Experiment Selected Stages
        /// </summary>
        [DataMember]
        public ICollection<S88StageMetadata> PlannedExperimentSelectedStages { get; private set; }

        /// <summary>
        /// Planned Experiment Selected Stages
        /// </summary>
        [DataMember]
        public ICollection<S88StageMetadata> PlannedExperimentOriginalStages { get; private set; }

        /// <summary>
        /// returns a copy of the object
        /// </summary>
        /// <returns></returns>
        public PlannedExperimentMetadata Clone()
        {
            var clone = new PlannedExperimentMetadata();
            clone.PlannedExperimentName = PlannedExperimentName;
            clone.CreatedTime = CreatedTime;
            clone.PlannedExperimentOriginalStages = PlannedExperimentOriginalStages.ToList();
            clone.PlannedExperimentProcess = this.PlannedExperimentProcess;
            clone.PlannedExperimentProject = this.PlannedExperimentProject;
            clone.PlannedExperimentSelectedStages = this.PlannedExperimentSelectedStages.ToList();
            clone.PlannedExperimentUserID = PlannedExperimentUserID;
            clone.PlannedExperimentUniqueID = PlannedExperimentUniqueID;
            return clone;
        }
    }
}
