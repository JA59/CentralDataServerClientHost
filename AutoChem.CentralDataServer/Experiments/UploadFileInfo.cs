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
using System.ServiceModel;
using AutoChem.Core.CentralDataServer.Applications;
using AutoChem.Core.DataAccess.Metadata;

namespace AutoChem.Core.CentralDataServer.Experiments
{
    /// <summary>
    /// Provides the information about a file that is being uploaded and finally the contents of the file.
    /// </summary>
    [MessageContract(WrapperNamespace=ServicesHelper.TypeNameSpace)]
    public class UploadFileInfo : IDisposable
    {
        /// <summary>
        /// File extension for the experiment
        /// </summary>
        public string Extension { get; set; }


        /// <summary>
        /// Metadata (user name, experiment name, project name)
        /// </summary>
        public DocumentMetadata MetaData { get; set; }

        /// <summary>
        /// Time the experiment was started
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// The time the experiment ended
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// Information about the application that is sending the experiment.
        /// </summary>
        public ApplicationInfo ApplicationInfo { get; set; }

        /// <summary>
        /// Time the experiment was started
        /// </summary>
        public string InstrumentType { get; set; }

        /// <summary>
        /// Stream containing the experiment content
        /// </summary>
        [MessageBodyMember(Order = 1)]
        public System.IO.Stream ExperimentFileStream;

        /// <summary>
        /// Dispose method.
        /// </summary>
        public void Dispose()
        {
            if (ExperimentFileStream != null)
            {
                ExperimentFileStream.Close();
                ExperimentFileStream = null;
            }
        }
    }
}
