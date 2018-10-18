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
using System.Runtime.Serialization;
#if !SILVERLIGHT
using AutoChem.Core.CentralDataServer.DataAccess;
#endif
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
    public class ExperimentSearchRequest
    {
        /// <summary>
        /// Default constructor needed by EntityFramework
        /// </summary>
        public ExperimentSearchRequest()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="maxItems"></param>
        public ExperimentSearchRequest(string searchString, int maxItems)
            : this()
        {
            SearchString = searchString;
            MaxItems = maxItems;
        }


        /// <summary>
        /// The string to search for object
        /// </summary>
        [DataMember]
        public string SearchString { get; set; }
        
        /// <summary>
        /// The maximum number of experiments to return
        /// </summary>
        [DataMember]
        public int MaxItems { get; set; }
    }
}
