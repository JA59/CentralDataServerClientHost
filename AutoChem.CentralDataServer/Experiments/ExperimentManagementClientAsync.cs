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

namespace AutoChem.Core.CentralDataServer.Experiments
{
    /// <summary>
    /// This is a simple client that connects to the InstrumentManagementService.
    /// </summary>
    public class ExperimentManagementClientAsync : ClientBase<IExperimentManagementServiceAsync>, IExperimentManagementServiceAsync
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding">binding (passed to base class)</param>
        /// <param name="remoteAddress">endpoint address (passed to base class)</param>
        public ExperimentManagementClientAsync(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

#if !SILVERLIGHT
        /// <summary>
        /// Upload an experiment file.
        /// </summary>
        /// <example>
        ///             string serverUri = string.Format(UrlExperimentManagementService, Environment.MachineName);
        ///             EndpointAddress address = new EndpointAddress(serverUri);
        ///             BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportCredentialOnly);
        ///
        ///             binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
        ///             binding.MaxReceivedMessageSize = 16000000;
        ///             binding.MaxBufferSize = 16000000;
        ///             binding.TransferMode = TransferMode.Streamed;
        ///
        ///             ExperimentManagementClient client = new ExperimentManagementClient(binding, address);
        ///             UploadFileInfo info = new UploadFileInfo();
        ///             info.ExperimentName = "Experiment 1";
        ///             info.User = "Joe";
        ///             info.StartTime = DateTime.Now;
        ///             info.Extension = ".iControlRC1e";
        ///             using (System.IO.FileStream stream =
        ///                    new System.IO.FileStream("d:\\Experiments\\Experiment 1.iControlRC1e",
        ///                    System.IO.FileMode.Open, System.IO.FileAccess.Read))
        ///             {
        ///                  info.ExperimentFileStream = stream;
        ///                  client.UploadFile(info);
        ///             }
        /// </example>
        public IAsyncResult BeginUploadFile(UploadFileInfo request, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginUploadFile(request, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling UploadFile on the server that corresponds to the result.
        ///</summary>
        public UploadFileResult EndUploadFile(System.IAsyncResult result)
        {
            return base.Channel.EndUploadFile(result);
        }

#if !SILVERLIGHT
        ///<summary>
        /// Returns the result of calling UploadFile on the server as an async Task.
        ///</summary>
        public Task<UploadFileResult> UploadFileAsync(UploadFileInfo request)
        {
            return base.Channel.UploadFileAsync(request);
        }
#else
        ///<summary>
        /// Returns the result of calling UploadFile on the server as an async Task.
        ///</summary>
        public Task<UploadFileResult> UploadFileAsync(UploadFileInfo request)
        {
            var taskSource = new TaskCompletionSource<UploadFileResult>();
            BeginUploadFile(request, asyncResult =>
            {
                try
                {
                    var result = EndUploadFile(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

        ///<summary>
        /// Calls UploadFile on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public UploadFileResult UploadFile(UploadFileInfo request)
        {
            IAsyncResult result = BeginUploadFile(request, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndUploadFile(result);
        }

        /// <summary>
        /// Initiates the uploading of a file.  Use the UploadID returned to upload the data.
        /// </summary>
        public IAsyncResult BeginInitiateFileUpload(UploadFileRequest request, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginInitiateFileUpload(request, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling InitiateFileUpload on the server that corresponds to the result.
        ///</summary>
        public UploadFileRequestResult EndInitiateFileUpload(System.IAsyncResult result)
        {
            return base.Channel.EndInitiateFileUpload(result);
        }

#if !SILVERLIGHT
        ///<summary>
        /// Returns the result of calling InitiateFileUpload on the server as an async Task.
        ///</summary>
        public Task<UploadFileRequestResult> InitiateFileUploadAsync(UploadFileRequest request)
        {
            return base.Channel.InitiateFileUploadAsync(request);
        }
#else
        ///<summary>
        /// Returns the result of calling InitiateFileUpload on the server as an async Task.
        ///</summary>
        public Task<UploadFileRequestResult> InitiateFileUploadAsync(UploadFileRequest request)
        {
            var taskSource = new TaskCompletionSource<UploadFileRequestResult>();
            BeginInitiateFileUpload(request, asyncResult =>
            {
                try
                {
                    var result = EndInitiateFileUpload(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

        ///<summary>
        /// Calls InitiateFileUpload on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public UploadFileRequestResult InitiateFileUpload(UploadFileRequest request)
        {
            IAsyncResult result = BeginInitiateFileUpload(request, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndInitiateFileUpload(result);
        }

        /// <summary>
        /// Uploads data for a file with the ID returned from InitiateFileUpload.
        /// </summary>
        public IAsyncResult BeginUploadFileData(Guid uploadID, byte[] fileData, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginUploadFileData(uploadID, fileData, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling UploadFileData on the server that corresponds to the result.
        ///</summary>
        public void EndUploadFileData(System.IAsyncResult result)
        {
            base.Channel.EndUploadFileData(result);
        }

#if !SILVERLIGHT
        ///<summary>
        /// Returns the result of calling UploadFileData on the server as an async Task.
        ///</summary>
        public Task UploadFileDataAsync(Guid uploadID, byte[] fileData)
        {
            return base.Channel.UploadFileDataAsync(uploadID, fileData);
        }
#else
        ///<summary>
        /// Returns the result of calling UploadFileData on the server as an async Task.
        ///</summary>
        public Task UploadFileDataAsync(Guid uploadID, byte[] fileData)
        {
            var taskSource = new TaskCompletionSource<object>();
            BeginUploadFileData(uploadID, fileData, asyncResult =>
            {
                try
                {
                    EndUploadFileData(asyncResult); var result = new object();
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

        ///<summary>
        /// Calls UploadFileData on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public void UploadFileData(Guid uploadID, byte[] fileData)
        {
            IAsyncResult result = BeginUploadFileData(uploadID, fileData, null, null);
            result.AsyncWaitHandle.WaitOne();

            EndUploadFileData(result);
        }

        /// <summary>
        /// Ends a file upload that was initiated by InitiateFileUpload and where the data was upload using UploadFileData.
        /// </summary>
        public IAsyncResult BeginEndFileUpload(Guid uploadID, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginEndFileUpload(uploadID, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling EndFileUpload on the server that corresponds to the result.
        ///</summary>
        public EndFileUploadResult EndEndFileUpload(System.IAsyncResult result)
        {
            return base.Channel.EndEndFileUpload(result);
        }

#if !SILVERLIGHT
        ///<summary>
        /// Returns the result of calling EndFileUpload on the server as an async Task.
        ///</summary>
        public Task<EndFileUploadResult> EndFileUploadAsync(Guid uploadID)
        {
            return base.Channel.EndFileUploadAsync(uploadID);
        }
#else
        ///<summary>
        /// Returns the result of calling EndFileUpload on the server as an async Task.
        ///</summary>
        public Task<EndFileUploadResult> EndFileUploadAsync(Guid uploadID)
        {
            var taskSource = new TaskCompletionSource<EndFileUploadResult>();
            BeginEndFileUpload(uploadID, asyncResult =>
            {
                try
                {
                    var result = EndEndFileUpload(asyncResult);
                    taskSource.SetResult(result);
                }
                catch (Exception exception)
                {
                    taskSource.SetException(exception);
                }
            }, null);
            return taskSource.Task;
        }
#endif

        ///<summary>
        /// Calls EndFileUpload on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public EndFileUploadResult EndFileUpload(Guid uploadID)
        {
            IAsyncResult result = BeginEndFileUpload(uploadID, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndEndFileUpload(result);
        }
#endif

    }
}
