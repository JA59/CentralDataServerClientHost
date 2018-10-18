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
using System.Diagnostics;
using System.Linq;
using System.Text;
#if !SILVERLIGHT
using AutoChem.Core.CentralDataServer.DataAccess;
#endif
using AutoChem.Core.CentralDataServer.Experiments;
using AutoChem.Core.Generics;
using System.Runtime.Serialization;

namespace AutoChem.Core.CentralDataServer.Instruments
{
    /// <summary>
    /// A reference to an instrument.
    /// At this point this is synonymous with the host address of the instrument, 
    /// but having a specific reference will parallel experiments, and applications.
    /// </summary>
    [DataContract(Namespace=ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class InstrumentReference : ExperimentSourceReference
    {
        /// <summary>
        /// Creates a new InstrumentReference
        /// </summary>
        public InstrumentReference()
        {
        }

#if !SILVERLIGHT
        /// <summary>
        /// Creates a new InstrumentReference with the specified host address.
        /// </summary>
        public InstrumentReference(InstrumentRecord instrumentRecord)
        {
            HostAddress = instrumentRecord.HostAddress;
            InstrumentType = instrumentRecord.InstrumentType;
            Description = instrumentRecord.Description;
            Version = instrumentRecord.Version;
            SerialNumber = instrumentRecord.SerialNumber;
            DateAdded = instrumentRecord.AddedOn;
        }
#endif

        /// <summary>
        /// Creates a new InstrumentReference with the specified host address.
        /// </summary>
        public InstrumentReference(string hostAddress) : this(hostAddress, String.Empty, String.Empty, String.Empty, String.Empty, DateTime.Now)
        {
        }

        /// <summary>
        /// Creates a new InstrumentReference with the specified host address.
        /// </summary>
        public InstrumentReference(string hostAddress, string instrumentType, string description, string version, string serialNumber, DateTime dateAdded)
        {
            HostAddress = hostAddress;
            InstrumentType = instrumentType;
            Description = description;
            Version = version;
            SerialNumber = serialNumber;
            DateAdded = dateAdded;
        }

        #if !SILVERLIGHT            
        /// <summary>
        /// Creates a new InstrumentReference with the specified host address.
        /// </summary>
        public static implicit operator InstrumentReference(InstrumentRecord instrumentRecord)
        {
            return new InstrumentReference(instrumentRecord);
        }
        #endif

        /// <summary>
        /// The address of an instrument. The inlcudes the host and optionally a port and /or path (e.g. IP or host name)
        /// </summary>
        [DataMember]
        public string HostAddress { get; set; }
        
        /// <summary>
        /// The address of an instrument. The inlcudes the host and optionally a port and /or path (e.g. IP or host name)
        /// </summary>
        [DataMember]
        public string InstrumentType { get; set; }

        /// <summary>
        /// The address of an instrument. The inlcudes the host and optionally a port and /or path (e.g. IP or host name)
        /// </summary>
        [DataMember]
        public string Description { get; set; }

        /// <summary>
        /// The firmware version of an instrument.
        /// </summary>
        [DataMember]
        public string Version { get; set; }

        /// <summary>
        /// The serial number of an instrument.
        /// </summary>
        [DataMember]
        public string SerialNumber { get; set; }
        
        /// <summary>
        /// The date the instrument was added.
        /// </summary>
        [DataMember]
        public DateTime DateAdded { get; set; }        

        /// <summary>
        /// Returns a hash code that takes into account the host address.
        /// </summary>
        public override int GetHashCode()
        {
            return HostAddress.ValueOrDefault(GetHashCode);
        }

        private int GetHashCode(string str)
        {
            return str.ToLower().GetHashCode();
        }

        /// <summary>
        /// Returns true if the other instrument reference is equal to this one.
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as InstrumentReference);
        }

        /// <summary>
        /// Returns true if the other instrument reference is equal to this one.
        /// </summary>
        public override bool Equals(ExperimentSourceReference other)
        {
            InstrumentReference otherReference = other as InstrumentReference;
            if (ReferenceEquals(otherReference, null))
            {
                return false;
            }
            else
            {
                return (HostAddress ?? string.Empty).ToLower() == (otherReference.HostAddress ?? string.Empty).ToLower();
            }
        }

        /// <summary>
        /// Returns text that represents the reference.
        /// </summary>
        public override string ToString()
        {
            //return HostAddress;
            return string.Format("{0} at {1}", InstrumentType, HostAddress);

        }
    }
}
