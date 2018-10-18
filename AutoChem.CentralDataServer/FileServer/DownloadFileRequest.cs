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
**    Copyright © 2015 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/
using System.Runtime.Serialization;

namespace AutoChem.Core.CentralDataServer.FileServer
{
    /// <summary>
    /// An abject that fully identifies the file or set of files to be downloaded.
    /// Currently, this request is just a keyword, and the server determines what file is associated with that keyword.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    public class DownloadFileRequest
    {
        /// <summary>
        /// Keyword that specifies a well known file to be downloaded.
        /// Keywords are specified in FileServerHelper
        /// </summary>
        [DataMember]
        public string Keyword { get; set; }
    }
}
