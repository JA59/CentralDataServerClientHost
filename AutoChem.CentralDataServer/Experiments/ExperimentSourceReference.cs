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
using System.Text;
#if !SILVERLIGHT
using System.ComponentModel.DataAnnotations;
#endif
using System.Runtime.Serialization;
using AutoChem.Core.CentralDataServer.Instruments;
using AutoChem.Core.CentralDataServer.Applications;

namespace AutoChem.Core.CentralDataServer.Experiments
{
    /// <summary>
    /// Defines an object as a source for an experiment.
    /// </summary>
    [DataContract(Namespace=ServicesHelper.TypeNameSpace)]
    [KnownType(typeof(InstrumentReference))]
    [KnownType(typeof(ApplicationReference))]
    [Serializable]
    public abstract class ExperimentSourceReference : IEquatable<ExperimentSourceReference>
    {
        /// <summary>
        /// Returns true if the other reference is equal to this one.
        /// </summary>
        public abstract bool Equals(ExperimentSourceReference other);
    }
}
