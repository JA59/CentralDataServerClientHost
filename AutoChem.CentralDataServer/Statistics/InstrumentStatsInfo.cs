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
using System.Runtime.Serialization;


namespace AutoChem.Core.CentralDataServer.Statistics
{
    /// <summary>
    /// Class that holds instruments stats info
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)] // This will be returned through a web service to the web page as well.
    [Serializable]
    public class InstrumentStatsInfo
    {
        /// <summary>
        /// The weekday
        /// </summary>
        [DataMember]
        public DateTime WeekDay  { get; set; }

        /// <summary>
        /// Average hours used in the weekday
        /// </summary>
        [DataMember]
        public double AverageHoursUsage { get; set; }

        /// <summary>
        /// Number Of Experiment ran on an instrument that period
        /// </summary>
        [DataMember]
        public int NumberOfExperiments { get; set; }

        /// <summary>
        /// time period
        /// </summary>
        [DataMember]
        public IntervalEnum Interval { get; set; }

        /// <summary>
        /// time period
        /// </summary>
        [DataMember]
        public bool isICInfo { get; set; }
        /// <summary>
        /// time period
        /// </summary>
        [DataMember]
        public string ProductName { get; set; }

    }
}
