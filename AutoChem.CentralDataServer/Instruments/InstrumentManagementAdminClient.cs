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
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Instruments
{
    /// <summary>
    /// This is a simple client that connects to the InstrumentManagementService.
    /// </summary>
    public class InstrumentManagementAdminClient : ClientBase<IInstrumentManagementServiceAdmin>, IInstrumentManagementServiceAdmin
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding (passed to base class)</param>
        /// <param name="remoteAddress">endpoint address (passed to base class)</param>
        public InstrumentManagementAdminClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Adds an instrument to the system.
        /// </summary>
        public InstrumentInfo AddInstrument(string hostAddress, string hostDescription)
        {
            return Channel.AddInstrument(hostAddress, hostDescription);
        }

        /// <summary>
        /// Adds an instrument to the system.
        /// </summary>
        public void RemoveInstrument(string hostAddress)
        {
            Channel.RemoveInstrument(hostAddress);
        }
    }
}
