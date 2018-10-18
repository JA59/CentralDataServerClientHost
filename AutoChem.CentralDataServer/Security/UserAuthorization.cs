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
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.ServiceModel;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Security
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UserAuthorization
    {
        private const string AdminRole = "Administrator";
        private const string NonAdminRole = "";  //"Not An Administrator";
        private const string UnknownUserName = "Anonymous User";

        /// <summary>
        /// Constructor
        /// </summary>
        public UserAuthorization()
        {
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userId">User's Id (name).</param>
        /// <param name="isAdministrator">Is the user an administrator.</param>
        public UserAuthorization(string userId, bool isAdministrator)
        {
            UserId = String.IsNullOrEmpty(userId) ? UnknownUserName : userId;
            if (isAdministrator)
            {
                Role = AdminRole;
            }
            else
            {
                Role = NonAdminRole;
            }
        }

        /// <summary>
        /// Role ("Administrator" or "Not An Administrator")
        /// </summary>
        [DataMember]
        public string Role
        {
            get;
             set;
        }

        /// <summary>
        /// User ID (Windows user name or "Anonymous")
        /// </summary>
        [DataMember]
        public string UserId
        {
            get;
            set;
        }

        /// <summary>
        /// True if the user can act as an administrator
        /// </summary>
        public bool IsAdministrator
        {
            get
            {
                return Role == AdminRole;
            }
            set
            {
                Role = value ? AdminRole : NonAdminRole;
            }
        }        
        
        /// <summary>
        /// True if the user is anonymous
        /// </summary>
        public bool IsAnonymous
        {
            get
            {
                return UserId == UnknownUserName;
            }
        }
    }
}
