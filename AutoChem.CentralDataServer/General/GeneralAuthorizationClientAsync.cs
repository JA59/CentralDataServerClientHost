//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// This code was auto-generated by ContractConverterTools.AsyncContractConverterTool, version 1.9.0.0
// 
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
using System.Threading.Tasks;
using AutoChem.Core.CentralDataServer.Logging;
using AutoChem.Core.CentralDataServer.Server;
using AutoChem.Core.CentralDataServer.Statistics;
using AutoChem.Core.ServiceModel;

namespace AutoChem.Core.CentralDataServer.General
{
    /// <summary>
    /// A client to the General management service.
    /// </summary>
    public class GeneralAuthorizationClientAsync : ClientBase<IGeneralAuthorizationServiceAsync>, IGeneralAuthorizationServiceAsync
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding (passed to base class)</param>
        /// <param name="remoteAddress">endpoint address (passed to base class)</param>
        public GeneralAuthorizationClientAsync(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

 
        /// <summary>
        /// Is the server configured for anonymous access
        /// </summary>
        /// <returns></returns>
        public IAsyncResult BeginIsAnonymousAccessAllowed(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginIsAnonymousAccessAllowed(callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling IsAnonymousAccessAllowed on the server that corresponds to the result.
        ///</summary>
        public bool EndIsAnonymousAccessAllowed(System.IAsyncResult result)
        {
            return base.Channel.EndIsAnonymousAccessAllowed(result);
        }

        ///<summary>
        /// Returns the result of calling IsAnonymousAccessAllowed on the server as an async Task.
        ///</summary>
        public Task<bool> IsAnonymousAccessAllowedAsync()
        {
            var taskSource = new TaskCompletionSource<bool>();
            BeginIsAnonymousAccessAllowed(asyncResult =>
            {
                try
                {
                    var result = EndIsAnonymousAccessAllowed(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }

        ///<summary>
        /// Calls IsAnonymousAccessAllowed on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public bool IsAnonymousAccessAllowed()
        {
            IAsyncResult result = BeginIsAnonymousAccessAllowed(null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndIsAnonymousAccessAllowed(result);
        }

        /// <summary>
        /// Is client executing on the same PC as the server
        /// </summary>
        /// <returns></returns>
        public IAsyncResult BeginIsClientComputerSameAsServer(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginIsClientComputerSameAsServer(callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling IsClientComputerSameAsServer on the server that corresponds to the result.
        ///</summary>
        public bool EndIsClientComputerSameAsServer(System.IAsyncResult result)
        {
            return base.Channel.EndIsClientComputerSameAsServer(result);
        }

        ///<summary>
        /// Returns the result of calling IsClientComputerSameAsServer on the server as an async Task.
        ///</summary>
        public Task<bool> IsClientComputerSameAsServerAsync()
        {
            var taskSource = new TaskCompletionSource<bool>();
            BeginIsClientComputerSameAsServer(asyncResult =>
            {
                try
                {
                    var result = EndIsClientComputerSameAsServer(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }

        ///<summary>
        /// Calls IsClientComputerSameAsServer on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public bool IsClientComputerSameAsServer()
        {
            IAsyncResult result = BeginIsClientComputerSameAsServer(null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndIsClientComputerSameAsServer(result);
        }


    }
}
