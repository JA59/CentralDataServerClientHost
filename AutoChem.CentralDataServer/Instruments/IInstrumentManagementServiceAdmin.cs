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
    /// Defines the operations available in the instrument management admin service.
    /// </summary>
    [ServiceContract(Namespace = ServicesHelper.ServiceNameSpace)]
    public interface IInstrumentManagementServiceAdmin
    {
        /// <summary>
        /// Adds an instrument to the system.
        /// </summary>
        [OperationContract]
        InstrumentInfo AddInstrument(string hostAddress, string hostDescription);

        /// <summary>
        /// Removes an instrument from the system.
        /// </summary>
        [OperationContract]
        void RemoveInstrument(string hostAddress);
    }
}
