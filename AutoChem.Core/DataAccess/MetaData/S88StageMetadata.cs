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
using System.Text;
using System.Threading.Tasks;

namespace AutoChem.Core.DataAccess.Metadata
{
    /// <summary>
    /// Class that contains metadata of the S88 stage.
    /// </summary>
    [Serializable]
    [DataContract]
    public class S88StageMetadata
    {
        /// <summary>
        ///default constructor
        /// </summary>
        public S88StageMetadata()
        {
            S88StageName = "Unknown";
            S88StageUniqueID = Guid.NewGuid();
        }
        /// <summary>
        /// S88 stage constructor with parameters
        /// </summary>
        /// <param name="s88stageName"></param>
        /// <param name="order"></param>
        public S88StageMetadata(string s88stageName, int order)
        {
            S88StageName = s88stageName;
            S88StageOrder = order;
            S88StageUniqueID = Guid.NewGuid();
        }
        /// <summary>
        /// S88 stage constructor with parameters
        /// </summary>
        /// <param name="s88stageName"></param>
        /// <param name="s88StageUniqueID"></param>
        /// <param name="order"></param>
        public S88StageMetadata(string s88stageName, Guid s88StageUniqueID, int order)
        {
            S88StageName = s88stageName;
            S88StageOrder = order;
            S88StageUniqueID = s88StageUniqueID;
        }
        /// <summary>
        /// Gets or sets the S88 stage unique identifier.
        /// </summary>
        [DataMember]
        public Guid S88StageUniqueID { get; set; }
        /// <summary>
        /// S88 stage Name(Kind)
        /// </summary>
        [DataMember]
        public string S88StageName { get; set; }

        /// <summary>
        /// S88 stage order
        /// </summary>
        [DataMember]
        public int S88StageOrder { get; set; }

    }
}
