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
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Experiments
{
    /// <summary>
    /// Represents a file type
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable] // Needed for communication between domains.
    public class FileType
    {
        /// <summary>
        /// The display name for the file type (e.g. Word Document, iControl, Excel)
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The extension for the file type (e.g. docx, iControl, xlsx)
        /// </summary>
        [DataMember]
        public string Extension { get; set; }
    }
}
