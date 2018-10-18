/*
**COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**    Copyright © 2011 by Mettler Toledo AutoChemAutoChem.  All rights reserved.
**
**ENDHEADER:
*/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using AutoChem.Core.CentralDataServer.Instruments;
using AutoChem.Core.Time;
using AutoChem.Core.CentralDataServer.Applications;
using AutoChem.Core.Generics;

namespace AutoChem.Core.CentralDataServer.Experiments
{
    /// <summary>
    /// Details about a search for experiments.
    /// Currently, only a simple string search is supported, but passing a search request object rather than
    /// just a string allows for easier expansion of search capabilities in the future.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class ExperimentSearchResult
    {
        /// <summary>
        /// Default constructor needed by EntityFramework
        /// </summary>
        public ExperimentSearchResult()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="totalMatches"></param>
        /// <param name="experimentInfos"></param>
        public ExperimentSearchResult(int totalMatches, IEnumerable<ExperimentInfo> experimentInfos)
            : this()
        {
            TotalMatches = totalMatches;
            ExperimentInfos = experimentInfos;
        }


        /// <summary>
        /// The number of matches that occurred
        /// Can be greater than the number returned
        /// </summary>
        [DataMember]
        public int TotalMatches { get; set; }

        /// <summary>
        /// The ExperimentInfos returned by the Search
        /// Can be less than the total matches
        /// </summary>
        [DataMember]
        public IEnumerable<ExperimentInfo> ExperimentInfos { get; set; }
    }
}

