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
    /// Class that holds instruments stats info
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)] // This will be returned through a web service to the web page as well.
    [Serializable]
    public class ExperimnetStatsInfo
    {
        /// <summary>
        /// The day mark period end
        /// </summary>
        [DataMember]
        public DateTime DateTimeMark { get; set; }

        /// <summary>
        /// Average hours experiments ran in a period
        /// </summary>
        [DataMember]
        public double AverageExperimentDuration { get; set; }

        /// <summary>
        /// Performed experiments
        /// </summary>
        [DataMember]
        public int NumberOfExperiments { get; set; }
        /// <summary>
        /// time period
        /// </summary>
        [DataMember]
        public IntervalEnum Interval { get; set; }

    }
    /// <summary>
    /// 
    /// </summary>
    public enum IntervalEnum
    {
        /// <summary>
        /// 
        /// </summary>
        Interval30Days,
        /// <summary>
        /// 
        /// </summary>
        Interval12Months,
        /// <summary>
        /// 
        /// </summary>
        Interval12Weeks
    }
}
