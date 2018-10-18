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
using System.Runtime.Serialization;
#if !SILVERLIGHT
using AutoChem.Core.CentralDataServer.DataAccess;
#endif

namespace AutoChem.Core.CentralDataServer.Instruments
{
    /// <summary>
    /// Holds Information about the instrument
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)] // This will be returned through a web service to the web page as well.
    [Serializable]
    public class InstrumentInfo : IEquatable<InstrumentInfo>
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public InstrumentInfo()
        {
        }

        /// <summary>
        /// Creates a new InstrumentInfo
        /// </summary>
        public InstrumentInfo(InstrumentStatus status, string hostAddress,
            string instrumentType, string version, string serialNumber = "", string description="")
        {
            Status = status;
            HostAddress = hostAddress;
            InstrumentType = instrumentType;
            Version = version;
            SerialNumber = serialNumber;
            Description = description;
        }

#if !SILVERLIGHT
        internal InstrumentInfo(InstrumentRecord instrumentRecord)
        {
            Status = instrumentRecord.Status;
            HostAddress = instrumentRecord.HostAddress;
            InstrumentType = instrumentRecord.InstrumentType;
            Version = instrumentRecord.Version;
            SerialNumber = instrumentRecord.SerialNumber;
            Description = instrumentRecord.Description;
            DateAdded = instrumentRecord.AddedOn;
            //By Default entity framework does not set DateTime.Kind when pulling values from the database.
            if (instrumentRecord.LastSuccessfullCommunication.HasValue)
            {
                LastSuccessfulCommunication = new DateTime(instrumentRecord.LastSuccessfullCommunication.Value.Ticks, DateTimeKind.Utc);
            }
        }
#endif

        /// <summary>
        /// A reference to the instrument.
        /// </summary>
        public InstrumentReference Reference
        {
            get { return new InstrumentReference(HostAddress, InstrumentType, Description, Version, SerialNumber, DateAdded); }
        }

        /// <summary>
        /// The status of the instrument
        /// </summary>
        [DataMember]
        public InstrumentStatus Status { get; set; }

        /// <summary>
        /// The address of an instrument. The inlcudes the host and optionally a port and /or path (e.g. IP or host name)
        /// </summary>
        [DataMember]
        public string HostAddress { get; set; }

        /// <summary>
        /// The type of the instrument (e.g. OptiMax or EasyMax)
        /// </summary>
        [DataMember]
        public string InstrumentType { get; set; }

        /// <summary>
        /// The version (firmware version) of the instrument.
        /// (Must be of type string, not System.Version, because System.Version can not be serialized to XML in Silverlight.)
        /// </summary>
        [DataMember]
        public string Version { get; set; }

        /// <summary>
        /// The serial number of the instrument.
        /// </summary>
        [DataMember]
        public string SerialNumber { get; set; }
        
        /// <summary>
        /// The date the instrument was added.
        /// </summary>
        [DataMember]
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// The description of the instrument.
        /// </summary>
        [DataMember]
        public string Description { get; set; }


        /// <summary>
        /// The last time the system successfully communicated with the instrument.
        /// </summary>
        [DataMember]
        public DateTime? LastSuccessfulCommunication { get; set; }

        /// <summary>
        /// Transient information about the last verification of this instrument. 
        /// This is not persistent and is used for Version Verification between InstrumentTaskManager and InstrumentVerifier
        /// </summary>
        public Version MinOrMaxInstrumentVerifierVersion { get; set; }

        /// <summary>
        /// Determine equality
        /// </summary>
        
        public bool Equals(InstrumentInfo other)
        {
            return !ReferenceEquals(other, null) &&
                   HostAddress.Equals(other.HostAddress) &&
                   InstrumentType == other.InstrumentType &&
                   Status == other.Status &&
                   Version == other.Version &&
                   SerialNumber == other.SerialNumber &&
                   Description == other.Description;
        }
    }
}
