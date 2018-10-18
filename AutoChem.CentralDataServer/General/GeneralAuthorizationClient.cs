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
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using AutoChem.Core.CentralDataServer.Logging;
using AutoChem.Core.CentralDataServer.Server;
using AutoChem.Core.CentralDataServer.Statistics;
using AutoChem.Core.ServiceModel;

namespace AutoChem.Core.CentralDataServer.General
{
    /// <summary>
    /// A client to the General management service.
    /// </summary>
    public class GeneralAuthorizationClient : ClientBase<IGeneralAuthorizationService>, IGeneralAuthorizationService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding (passed to base class)</param>
        /// <param name="remoteAddress">endpoint address (passed to base class)</param>
        public GeneralAuthorizationClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

 
        /// <summary>
        /// Is the server configured for anonymous access
        /// </summary>
        /// <returns></returns>
        public bool IsAnonymousAccessAllowed()
        {
            return Channel.IsAnonymousAccessAllowed();
        }

        /// <summary>
        /// Is client executing on the same PC as the server
        /// </summary>
        /// <returns></returns>
        public bool IsClientComputerSameAsServer()
        {
            return Channel.IsClientComputerSameAsServer();
        }


    }
}
