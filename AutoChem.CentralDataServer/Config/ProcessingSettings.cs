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
    public class ProcessingSettings
    {
        /// <summary>
        /// Creates a new ProcessingSettings.
        /// Processor settings determine which data outputs are created when processing an experiment.
        /// </summary>
        public ProcessingSettings()
        {
            GenerateReport = true;
            GenerateXml = false; // by default, XML files are not generated.
            GenerateExcel = true; 
            GenerateMetrics = true;
            GenerateAnalyticalData = false; // by default, analytical data files are not generated
        }

        /// <summary>
        /// If true, when processing an experiment, generate a report document.
        /// </summary>
        [DataMember]
        public bool GenerateReport { get; set; }

        /// <summary>
        /// If true, when processing an experiment, generate an XML file.
        /// </summary>
        [DataMember]
        public bool GenerateXml { get; set; }

        /// <summary>
        /// If true, when processing an experiment, generate an Excel spreadsheet.
        /// </summary>
        [DataMember]
        public bool GenerateExcel { get; set; }

        /// <summary>
        /// If true, when processing an experiment, generate metrics data in the database.
        /// </summary>
        [DataMember]
        public bool GenerateMetrics { get; set; }

        /// <summary>
        /// If true, when processing an experiment, generates analytical data files such as SPC files in the database.
        /// </summary>
        [DataMember]
        public bool GenerateAnalyticalData { get; set; }

    }
}


