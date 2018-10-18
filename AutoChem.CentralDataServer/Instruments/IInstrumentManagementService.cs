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
**    Copyright © 2011 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Instruments
{
    /// <summary>
    /// Defines the operations available in the instrument management service.
    /// </summary>
    [ServiceContract(Namespace = ServicesHelper.ServiceNameSpace)]
    public interface IInstrumentManagementService
    {
        /// <summary>
        /// Gets the instruments registered on the system.
        /// </summary>
        [OperationContract]
        IEnumerable<LiveInstrumentInfo> GetRegisteredInstruments();
    }
}
