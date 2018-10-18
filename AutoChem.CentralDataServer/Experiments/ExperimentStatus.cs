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

namespace AutoChem.Core.CentralDataServer.Experiments
{
    /// <summary>
    /// Defines the state of experiment processing.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    public enum ExperimentStatus
    {
        /// <summary>
        /// The system does not know about this experiment yet.
        /// </summary>
        [EnumMember]
        NotRegistered,
        /// <summary>
        /// The experiment has been stored, but has not been processed.
        /// </summary>
        [EnumMember]
        Stored,
        /// <summary>
        /// The system has processed the experiment.
        /// </summary>
        [EnumMember]
        Processed
    }
}
