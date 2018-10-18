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
    public interface IGeneralManagementService
    {
        /// <summary>
        /// Send an error to the server to be logged with server side messages.
        /// Message is logged as an error message.
        /// </summary>
        [OperationContract]
        void TraceError(string message);

        /// <summary>
        /// Send an array of messages to the server to be logged with server side messages.
        /// Messages are logged as a block of information messages.
        /// </summary>
        [OperationContract]
        void TraceClientLogMessages(string[] messages);
        
        /// <summary>
        /// Get the version string
        /// </summary>
        [OperationContract]
        string GetServerVersion ();

        /// <summary>
        /// Get overall system state
        /// </summary>
        [OperationContract]  
        SystemStateInfo GetSystemStateInfo();

        /// <summary>
        /// Registers an observer to have a collection of events be retrievable for the observer.
        /// </summary>
        [OperationContract]
        void AddServerObserver(Guid observerID);

        /// <summary>
        /// Gets the events for the observer.
        /// </summary>
        [OperationContract]
        ServerEvents GetEvents(Guid observerID);
    }
}
