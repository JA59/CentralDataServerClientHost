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

namespace AutoChem.Core.CentralDataServer.Statistics
{
    /// <summary>
    /// Class that holds disk space info
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)] // This will be returned through a web service to the web page as well.
    [Serializable]
    public class DiskSpaceInfo
    {
        /// <summary>
        /// The size of the disk
        /// </summary>
        [DataMember]
        public double TotalGBSize { get; set; }

        /// <summary>
        /// The size of the disk that is  available for current user
        /// </summary>
        [DataMember]
        public double AvailableFreeGBSpace { get; set; }
        /// <summary>
        /// Percentage available free space for the current user
        /// </summary>
        public int PercentUsedSpace
        {
            get 
            {
                if(TotalGBSize == 0)
                {
                    return 0;
                }
                int percentFreeSpace = (int) (100 *((TotalGBSize-AvailableFreeGBSpace)/TotalGBSize));
                return percentFreeSpace;
            }
        }
        /// <summary>
        /// Compiled string to display
        /// </summary>
        public string DisplayString
        {
            get
            {
                if (TotalGBSize == 0)
                {
                    return "Unable to compute";
                }
                return 100 - PercentUsedSpace + "% free (out of " + (int)TotalGBSize + "GB)";
            }
        }
    }
}
