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
**  Copyright © 2012 by Mettler Toledo AutoChem.  All rights reserved.
**/

using System;
using System.Runtime.Serialization;

namespace AutoChem.Core.DataAccess.Metadata
{
    /// <summary>
    /// Class to store experiment metadata as 
    /// </summary>
    [Serializable]
    [DataContract]
    public class DocumentMetadata
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DocumentMetadata()
            : this(string.Empty, string.Empty, string.Empty) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentName"></param>
        public DocumentMetadata(string documentName)
            : this(string.Empty, string.Empty, documentName) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentUserName"></param>
        /// <param name="userDefinedField"></param>
        /// <param name="documentName"> </param>
        public DocumentMetadata(string documentUserName, string userDefinedField, string documentName)
        {
            DocumentUserName = documentUserName;
            UserDefinedField = userDefinedField;
            DocumentName = documentName;
        }

        /// <summary>
        /// ExperimentUserName
        /// </summary>
        [DataMember]
        public string DocumentUserName { get; set; }
        
        /// <summary>
        /// UserDefinedField
        /// </summary>
        [DataMember]
        public string UserDefinedField { get; set; }
        
        /// <summary>
        /// Experiment Name
        /// </summary>
        [DataMember]
        public string DocumentName { get; set; }
        
        /// <summary>
        /// If this is a planned experiment then the planned experiment metadata is not null
        /// </summary>
        [DataMember]
        public PlannedExperimentMetadata PlannedExperimentMetadata { get; set; }
        
        /// <summary>
        /// Gets a value indicating whether the S88 process support is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [DataMember]
        public bool S88ProcessSupportEnabled { get; set; }
        
        /// <summary>
        /// Experiment Name Main Part
        /// 
        /// </summary>
        [DataMember]
        public string DocumentNameMainPart { get; set; }
        
        /// <summary>
        /// Experiment Name Suffix
        /// </summary>
        [DataMember]
        public string DocumentNameSuffix { get; set; }
    }
}
