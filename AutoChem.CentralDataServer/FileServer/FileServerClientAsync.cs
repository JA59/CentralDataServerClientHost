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
**    Copyright © 2015 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/
using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace AutoChem.Core.CentralDataServer.FileServer
{
    /// <summary>
    /// Client wrapper for calling methods within iC Data Center File Server service
    /// </summary>
    public class FileServerClientAsync : ClientBase<IFileServerServiceAsync>, IFileServerServiceAsync
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FileServerClientAsync(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }
    }
}
