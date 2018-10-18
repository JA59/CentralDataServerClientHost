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
using System.Text;

namespace AutoChem.Core.CentralDataServer.Security
{
    /// <summary>
    /// Service for obtaining the user's authorization object
    /// </summary>
    [ServiceContract(Namespace = ServicesHelper.ServiceNameSpace)]
    public interface IAuthorizationService
    {
        /// <summary>
        /// Gets the user's authorization (admin or not) based on the windows credentials of the caller.
        /// </summary>
        [OperationContract]
        UserAuthorization GetUserAuthorization();


    }
}
