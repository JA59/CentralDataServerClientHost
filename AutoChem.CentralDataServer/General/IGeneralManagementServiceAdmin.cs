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
using System.ServiceModel;
using AutoChem.Core.CentralDataServer.Logging;
using AutoChem.Core.CentralDataServer.Statistics;
using AutoChem.Core.CentralDataServer.Server;

namespace AutoChem.Core.CentralDataServer.General
{
    /// <summary>
    /// Defines the operations available in the general purpose management service.
    /// </summary>
    [ServiceContract(Namespace = ServicesHelper.ServiceNameSpace)]
    public interface IGeneralManagementServiceAdmin
    {
        /// <summary>
        /// Upload Recent UserLogEntries.
        /// </summary>
        [OperationContract]
        IEnumerable<UserLogEntry> GetUserLogEntriesRecentDays(int maxDays);

        /// <summary>
        /// Get Recent UserLogEntries (currently past 6 months)
        /// </summary>
        [OperationContract]
        IEnumerable<UserLogEntry> GetUserLogEntries();

        /// <summary>
        /// Marks the provided entry as resolved.
        /// </summary>
        [OperationContract]
        void MarkResolved(UserLogEntry entry);

        /// <summary>
        /// Gets a code for downloading log files.
        /// </summary>
        [OperationContract]
        string GetLogFileCode();
    }
}
