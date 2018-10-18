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
    /// Result data for a file data download
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    public class DownloadFileResult
    {
        /// <summary>
        /// The data itself
        /// </summary>
        [DataMember]
        public byte[] Data { get; set; }

        /// <summary>
        /// Offset in file to the first byte in this packet
        /// </summary>
        [DataMember]
        public long Offset { get; set; }        
        
        /// <summary>
        /// Number of bytes in this packet (size of the "Data" member)
        /// </summary>
        [DataMember]
        public int ByteCount { get; set; }
        
    }
}
