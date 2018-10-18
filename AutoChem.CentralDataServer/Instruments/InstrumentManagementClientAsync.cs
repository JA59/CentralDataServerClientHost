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
using System.Threading.Tasks;

namespace AutoChem.Core.CentralDataServer.Instruments
{
    /// <summary>
    /// This is a simple client that connects to the InstrumentManagementService.
    /// </summary>
    public class InstrumentManagementClientAsync : ClientBase<IInstrumentManagementServiceAsync>, IInstrumentManagementServiceAsync
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding (passed to base class)</param>
        /// <param name="remoteAddress">endpoint address (passed to base class)</param>
        public InstrumentManagementClientAsync(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Gets the instruments registered on the system.
        /// </summary>
        public IAsyncResult BeginGetRegisteredInstruments(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetRegisteredInstruments(callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetRegisteredInstruments on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<LiveInstrumentInfo> EndGetRegisteredInstruments(System.IAsyncResult result)
        {
            return base.Channel.EndGetRegisteredInstruments(result);
        }

        ///<summary>
        /// Returns the result of calling GetRegisteredInstruments on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<LiveInstrumentInfo>> GetRegisteredInstrumentsAsync()
        {
            var taskSource = new TaskCompletionSource<IEnumerable<LiveInstrumentInfo>>();
            BeginGetRegisteredInstruments(asyncResult =>
            {
                try
                {
                    var result = EndGetRegisteredInstruments(asyncResult);
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
        /// Calls GetRegisteredInstruments on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<LiveInstrumentInfo> GetRegisteredInstruments()
        {
            IAsyncResult result = BeginGetRegisteredInstruments(null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetRegisteredInstruments(result);
        }

    }
}
