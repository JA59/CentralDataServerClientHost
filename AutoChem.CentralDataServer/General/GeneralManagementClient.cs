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
    public class GeneralManagementClient : ClientBase<IGeneralManagementService>, IGeneralManagementService
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding (passed to base class)</param>
        /// <param name="remoteAddress">endpoint address (passed to base class)</param>
        public GeneralManagementClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }   
        
        /// <summary>
        /// Send an error to the server to be logged with server side messages
        /// </summary>
        [AsyncResync]
        public void TraceError(string message)
        {
            Channel.TraceError(message);
        }

        /// <summary>
        /// Formats the error message and sends it to the server.  Note the send is asynchronous in the Async version.
        /// </summary>
        public void TraceError(string format, params object[] args)
        {
            string message = string.Format(format, args);
            TraceError(message);
        }

        /// <summary>
        /// Send an array of messages to the server to be logged with server side messages
        /// </summary>
        public void TraceClientLogMessages(string[] messages)
        {
            Channel.TraceClientLogMessages(messages);
        }

        /// <summary>
        /// Get the server version
        /// </summary>
        /// <returns></returns>
        public string GetServerVersion()
        {
            return Channel.GetServerVersion();
        }

        /// <summary>
        /// Get overall system state
        /// </summary>
        public SystemStateInfo GetSystemStateInfo()
        {
            return Channel.GetSystemStateInfo();
        }

        /// <summary>
        /// Registers an observer to have a collection of events be retrievable for the observer.
        /// </summary>
        public void AddServerObserver(Guid observerID)
        {
            Channel.AddServerObserver(observerID);
        }

        /// <summary>
        /// Gets the events for the observer.
        /// </summary>
        public ServerEvents GetEvents(Guid observerID)
        {
            return Channel.GetEvents(observerID);
        }
    }
}
