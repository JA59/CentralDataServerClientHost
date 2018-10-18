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
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoChem.Core.CentralDataServer.Instruments
{
    /// <summary>
    /// Defines the operations available in the instrument management service.
    /// </summary>
    [ServiceContract(Name = "IInstrumentManagementService", Namespace = ServicesHelper.ServiceNameSpace)]
    public interface IInstrumentManagementServiceAsync
    {
        /// <summary>
        /// Gets the instruments registered on the system.
        /// </summary>
        [OperationContract(AsyncPattern=true)]
        IAsyncResult BeginGetRegisteredInstruments(System.AsyncCallback callback, object asyncState);

        ///<summary>
        /// Returns the result of calling GetRegisteredInstruments on the server that corresponds to the result.
        ///</summary>
        IEnumerable<LiveInstrumentInfo> EndGetRegisteredInstruments(System.IAsyncResult result);

        ///<summary>
        /// Calls GetRegisteredInstruments on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        IEnumerable<LiveInstrumentInfo> GetRegisteredInstruments();
    }
}
