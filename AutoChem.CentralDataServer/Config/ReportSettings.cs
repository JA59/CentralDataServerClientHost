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
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using AutoChem.Core.CentralDataServer.Experiments;

namespace AutoChem.Core.CentralDataServer.Config
{
    /// <summary>
    /// Stores the settings concerning reports
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class ReportSettings
    {
        /// <summary>
        /// Creates a new ReportSettings
        /// </summary>
        public ReportSettings()
        {
            ShowExperimentTime = false;
        }

        /// <summary>
        /// Show experiment (absolute) time in reports if true; show relative time if not
        /// </summary>
        [DataMember]
        public bool ShowExperimentTime { get; set; }

        /// <summary>
        /// The dotx word template to use for creating the word document from the experiment report.
        /// </summary>
        [DataMember]
        public string ReportWordTemplate { get; set; }
    }
}

