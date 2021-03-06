//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
//
// This code was auto-generated by ContractConverterTools.AsyncContractConverterTool, version 1.10.0.0
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
**    Copyright © 2012 by Mettler Toledo AutoChem.  All rights reserved.
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

namespace AutoChem.Core.CentralDataServer.Statistics
{
    /// <summary>
    /// A client that connects to the Statistics service
    /// </summary>
    public class StatisticsManagmentClientAsync : ClientBase<IStatisticsServiceAsync>, IStatisticsServiceAsync
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="binding"></param>
        /// <param name="remoteAddress"></param>
        public StatisticsManagmentClientAsync(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
            
        }
        #region Implementation of IStatisticsService

        ///<summary>
        /// Calls GetInstrumentInfos on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        public IAsyncResult BeginGetInstrumentStatsInfos(DateTime startTime, DateTime endTime, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetInstrumentStatsInfos(startTime, endTime, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetInstrumentStatsInfos on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<InstrumentStatsInfo> EndGetInstrumentStatsInfos(System.IAsyncResult result)
        {
            return base.Channel.EndGetInstrumentStatsInfos(result);
        }

        ///<summary>
        /// Returns the result of calling GetInstrumentStatsInfos on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<InstrumentStatsInfo>> GetInstrumentStatsInfosAsync(DateTime startTime, DateTime endTime)
        {
            var taskSource = new TaskCompletionSource<IEnumerable<InstrumentStatsInfo>>();
            BeginGetInstrumentStatsInfos(startTime, endTime, asyncResult =>
            {
                try
                {
                    var result = EndGetInstrumentStatsInfos(asyncResult);
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
        /// Calls GetInstrumentStatsInfos on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<InstrumentStatsInfo> GetInstrumentStatsInfos(DateTime startTime, DateTime endTime)
        {
            IAsyncResult result = BeginGetInstrumentStatsInfos(startTime, endTime, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetInstrumentStatsInfos(result);
        }

        ///<summary>
        /// Calls GetInstrumentInfos on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        public IAsyncResult BeginGetInstrumentStatsInfosWithinInterval(IntervalEnum interval, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetInstrumentStatsInfosWithinInterval(interval, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetInstrumentStatsInfosWithinInterval on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<InstrumentStatsInfo> EndGetInstrumentStatsInfosWithinInterval(System.IAsyncResult result)
        {
            return base.Channel.EndGetInstrumentStatsInfosWithinInterval(result);
        }

        ///<summary>
        /// Returns the result of calling GetInstrumentStatsInfosWithinInterval on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<InstrumentStatsInfo>> GetInstrumentStatsInfosWithinIntervalAsync(IntervalEnum interval)
        {
            var taskSource = new TaskCompletionSource<IEnumerable<InstrumentStatsInfo>>();
            BeginGetInstrumentStatsInfosWithinInterval(interval, asyncResult =>
            {
                try
                {
                    var result = EndGetInstrumentStatsInfosWithinInterval(asyncResult);
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
        /// Calls GetInstrumentStatsInfosWithinInterval on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<InstrumentStatsInfo> GetInstrumentStatsInfosWithinInterval(IntervalEnum interval)
        {
            IAsyncResult result = BeginGetInstrumentStatsInfosWithinInterval(interval, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetInstrumentStatsInfosWithinInterval(result);
        }

        ///<summary>
        /// Calls GetInstrumentInfos on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        public IAsyncResult BeginGetiCInstrumentStatsInfosWithinInterval(IntervalEnum interval, string iCProductName, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetiCInstrumentStatsInfosWithinInterval(interval, iCProductName, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetiCInstrumentStatsInfosWithinInterval on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<InstrumentStatsInfo> EndGetiCInstrumentStatsInfosWithinInterval(System.IAsyncResult result)
        {
            return base.Channel.EndGetiCInstrumentStatsInfosWithinInterval(result);
        }

        ///<summary>
        /// Returns the result of calling GetiCInstrumentStatsInfosWithinInterval on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<InstrumentStatsInfo>> GetiCInstrumentStatsInfosWithinIntervalAsync(IntervalEnum interval, string iCProductName)
        {
            var taskSource = new TaskCompletionSource<IEnumerable<InstrumentStatsInfo>>();
            BeginGetiCInstrumentStatsInfosWithinInterval(interval, iCProductName, asyncResult =>
            {
                try
                {
                    var result = EndGetiCInstrumentStatsInfosWithinInterval(asyncResult);
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
        /// Calls GetiCInstrumentStatsInfosWithinInterval on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<InstrumentStatsInfo> GetiCInstrumentStatsInfosWithinInterval(IntervalEnum interval, string iCProductName)
        {
            IAsyncResult result = BeginGetiCInstrumentStatsInfosWithinInterval(interval, iCProductName, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetiCInstrumentStatsInfosWithinInterval(result);
        }

        ///<summary>
        /// Calls GetExperimentStatsInfos on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        public IAsyncResult BeginGetExperimentStatsInfos(DateTime startTime, DateTime endTime, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetExperimentStatsInfos(startTime, endTime, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetExperimentStatsInfos on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<ExperimnetStatsInfo> EndGetExperimentStatsInfos(System.IAsyncResult result)
        {
            return base.Channel.EndGetExperimentStatsInfos(result);
        }

        ///<summary>
        /// Returns the result of calling GetExperimentStatsInfos on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<ExperimnetStatsInfo>> GetExperimentStatsInfosAsync(DateTime startTime, DateTime endTime)
        {
            var taskSource = new TaskCompletionSource<IEnumerable<ExperimnetStatsInfo>>();
            BeginGetExperimentStatsInfos(startTime, endTime, asyncResult =>
            {
                try
                {
                    var result = EndGetExperimentStatsInfos(asyncResult);
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
        /// Calls GetExperimentStatsInfos on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<ExperimnetStatsInfo> GetExperimentStatsInfos(DateTime startTime, DateTime endTime)
        {
            IAsyncResult result = BeginGetExperimentStatsInfos(startTime, endTime, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetExperimentStatsInfos(result);
        }

        ///<summary>
        /// Calls GetExperimentStatsInfos on the server and may or may not wait for a response see client.
        /// If this is synchronous it should not be called on the UI thread.
        ///</summary>
        public IAsyncResult BeginGetExperimentStatsInfosWithinInterval(IntervalEnum interval, System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetExperimentStatsInfosWithinInterval(interval, callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetExperimentStatsInfosWithinInterval on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<ExperimnetStatsInfo> EndGetExperimentStatsInfosWithinInterval(System.IAsyncResult result)
        {
            return base.Channel.EndGetExperimentStatsInfosWithinInterval(result);
        }

        ///<summary>
        /// Returns the result of calling GetExperimentStatsInfosWithinInterval on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<ExperimnetStatsInfo>> GetExperimentStatsInfosWithinIntervalAsync(IntervalEnum interval)
        {
            var taskSource = new TaskCompletionSource<IEnumerable<ExperimnetStatsInfo>>();
            BeginGetExperimentStatsInfosWithinInterval(interval, asyncResult =>
            {
                try
                {
                    var result = EndGetExperimentStatsInfosWithinInterval(asyncResult);
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
        /// Calls GetExperimentStatsInfosWithinInterval on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<ExperimnetStatsInfo> GetExperimentStatsInfosWithinInterval(IntervalEnum interval)
        {
            IAsyncResult result = BeginGetExperimentStatsInfosWithinInterval(interval, null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetExperimentStatsInfosWithinInterval(result);
        }

        /// <summary>
        /// Gets all systems stats info
        /// </summary>
        /// <returns></returns>
        public IAsyncResult BeginGetAllSystemsStatsInfo(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetAllSystemsStatsInfo(callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetAllSystemsStatsInfo on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<InstrumentAppStatsInfo> EndGetAllSystemsStatsInfo(System.IAsyncResult result)
        {
            return base.Channel.EndGetAllSystemsStatsInfo(result);
        }

        ///<summary>
        /// Returns the result of calling GetAllSystemsStatsInfo on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<InstrumentAppStatsInfo>> GetAllSystemsStatsInfoAsync()
        {
            var taskSource = new TaskCompletionSource<IEnumerable<InstrumentAppStatsInfo>>();
            BeginGetAllSystemsStatsInfo(asyncResult =>
            {
                try
                {
                    var result = EndGetAllSystemsStatsInfo(asyncResult);
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
        /// Calls GetAllSystemsStatsInfo on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<InstrumentAppStatsInfo> GetAllSystemsStatsInfo()
        {
            IAsyncResult result = BeginGetAllSystemsStatsInfo(null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetAllSystemsStatsInfo(result);
        }

        /// <summary>
        /// Gets latest exp infos
        /// </summary>
        /// <returns></returns>
        public IAsyncResult BeginGetLatestExperementsInfos(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetLatestExperementsInfos(callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetLatestExperementsInfos on the server that corresponds to the result.
        ///</summary>
        public IEnumerable<LatestExpInfo> EndGetLatestExperementsInfos(System.IAsyncResult result)
        {
            return base.Channel.EndGetLatestExperementsInfos(result);
        }

        ///<summary>
        /// Returns the result of calling GetLatestExperementsInfos on the server as an async Task.
        ///</summary>
        public Task<IEnumerable<LatestExpInfo>> GetLatestExperementsInfosAsync()
        {
            var taskSource = new TaskCompletionSource<IEnumerable<LatestExpInfo>>();
            BeginGetLatestExperementsInfos(asyncResult =>
            {
                try
                {
                    var result = EndGetLatestExperementsInfos(asyncResult);
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
        /// Calls GetLatestExperementsInfos on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public IEnumerable<LatestExpInfo> GetLatestExperementsInfos()
        {
            IAsyncResult result = BeginGetLatestExperementsInfos(null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetLatestExperementsInfos(result);
        }

        /// <summary>
        /// Get the info about the disk space
        /// </summary>
        /// <returns></returns>
        public IAsyncResult BeginGetDiskSpaceInfo(System.AsyncCallback callback, object asyncState)
        {
            return base.Channel.BeginGetDiskSpaceInfo(callback, asyncState);
        }

        ///<summary>
        /// Returns the result of calling GetDiskSpaceInfo on the server that corresponds to the result.
        ///</summary>
        public DiskSpaceInfo EndGetDiskSpaceInfo(System.IAsyncResult result)
        {
            return base.Channel.EndGetDiskSpaceInfo(result);
        }

        ///<summary>
        /// Returns the result of calling GetDiskSpaceInfo on the server as an async Task.
        ///</summary>
        public Task<DiskSpaceInfo> GetDiskSpaceInfoAsync()
        {
            var taskSource = new TaskCompletionSource<DiskSpaceInfo>();
            BeginGetDiskSpaceInfo(asyncResult =>
            {
                try
                {
                    var result = EndGetDiskSpaceInfo(asyncResult);
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
        /// Calls GetDiskSpaceInfo on the server and waits for a response (synchronous).
        /// This should not be called on a UI thread
        ///</summary>
        public DiskSpaceInfo GetDiskSpaceInfo()
        {
            IAsyncResult result = BeginGetDiskSpaceInfo(null, null);
            result.AsyncWaitHandle.WaitOne();

            return EndGetDiskSpaceInfo(result);
        }

        #endregion
    }
}
