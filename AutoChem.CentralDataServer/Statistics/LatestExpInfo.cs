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
using AutoChem.Core.CentralDataServer.Experiments;

namespace AutoChem.Core.CentralDataServer.Statistics
{
    /// <summary>
    /// Class that holds latest completed exp info
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)] // This will be returned through a web service to the web page as well.
    [Serializable]
    public class LatestExpInfo
    {
        /// <summary>
        /// The name of the experiment
        /// </summary>
        [DataMember]
        public string ExperimentName { get; set; }
        /// <summary>
        /// The name of the experiment
        /// </summary>
        [DataMember]
        public string ExperimentAuthor { get; set; }
        /// <summary>
        /// The instrument name
        /// </summary>
        [DataMember]
        public string InstrumentOrAppName { get; set; }
        /// <summary>
        /// ComplitionTime
        /// </summary>
        [DataMember]
        public DateTime TimeCompleted { get; set; }
        /// <summary>
        /// experiment path UNC or local
        /// </summary>
        [DataMember]
        public string Uri { get; set; }
        /// <summary>
        /// experiment path UNC or local
        /// </summary>
        [DataMember]
        public ExperimentInfo ExperimentReference { get; set; }
        /// <summary>
        /// Combination Of author name and app instrument
        /// </summary>
        public string CombinedName
        {
            get 
            { 
                string name = string.Empty; 
                if(!string.IsNullOrWhiteSpace(ExperimentAuthor))
                {
                    name = "by " + ExperimentAuthor;
                }
                if (!string.IsNullOrWhiteSpace(InstrumentOrAppName))
                {
                    name += " on " + InstrumentOrAppName;
                }
                return name.Trim(); 
            }
        }
        /// <summary>
        /// Property that determines if dashboard hyperlink is a plane text
        /// </summary>
        public bool IsHyperLink
        {
            get { return !string.IsNullOrWhiteSpace(Uri); }
        }

    }
}
