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
    public class GeneralManagementAdminClient : ClientBase<IGeneralManagementServiceAdmin>, IGeneralManagementServiceAdmin
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding (passed to base class)</param>
        /// <param name="remoteAddress">endpoint address (passed to base class)</param>
        public GeneralManagementAdminClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Get recent user log entries
        /// </summary>
        public IEnumerable<UserLogEntry> GetUserLogEntriesRecentDays(int maxDays)
        {
            return Channel.GetUserLogEntriesRecentDays(maxDays);
        }

        /// <summary>
        /// Get recent user log entries (currently past 6 months)
        /// </summary>
        public IEnumerable<UserLogEntry> GetUserLogEntries()
        {
            return Channel.GetUserLogEntries();
        }

        /// <summary>
        /// Marks the provided log entry as resolved.
        /// </summary>
        public void MarkResolved(UserLogEntry entry)
        {
            Channel.MarkResolved(entry);
        }

        /// <summary>
        /// Gets a code for downloading log files.
        /// </summary>
        public string GetLogFileCode()
        {
            return Channel.GetLogFileCode();
        }
    }
}
