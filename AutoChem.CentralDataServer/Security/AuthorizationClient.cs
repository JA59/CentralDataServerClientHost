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

namespace AutoChem.Core.CentralDataServer.Security
{
    /// <summary>
    /// A client to the Authorization service.
    /// </summary>    
    public class AuthorizationClient : ClientBase<IAuthorizationService>, IAuthorizationService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding (passed to base class)</param>
        /// <param name="remoteAddress">endpoint address (passed to base class)</param>
        public AuthorizationClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Gets the configuration settings for the central data server.
        /// </summary>
        public UserAuthorization GetUserAuthorization()
        {
            return Channel.GetUserAuthorization();
        }

    }
}
