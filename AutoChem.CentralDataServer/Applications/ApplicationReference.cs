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
using System.Runtime.Serialization;
using System.Text;
#if !SILVERLIGHT
using AutoChem.Core.CentralDataServer.DataAccess;
#endif
using AutoChem.Core.CentralDataServer.Experiments;
using AutoChem.Core.Generics;

namespace AutoChem.Core.CentralDataServer.Applications
{
    /// <summary>
    /// A reference to a particular application.
    /// </summary>
    // Note this class name is also used in AppInerop, but hopefully it will not interfere to often
    // because it is consistent with the rest of the central data server classes.
    [Serializable]
    [DataContract(Namespace=ServicesHelper.TypeNameSpace)]
    public class ApplicationReference : ExperimentSourceReference
    {
        /// <summary>
        /// Creates an empty ApplicationReference
        /// </summary>
        public ApplicationReference()
        {
        }

#if !SILVERLIGHT
        /// <summary>
        /// Creates an ApplicationReference with the specified values from the record
        /// </summary>
        internal ApplicationReference(ApplicationRecord record)
        {
            Name = record.Name;
            HostAddress = record.HostAddress;
            Version = record.Version;
        }
#endif

        /// <summary>
        /// The name of the application.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The address of the computer on which the application is running.
        /// </summary>
        [DataMember]
        public string HostAddress { get; set; }
        
        /// <summary>
        /// The version of the software on which the application is running.
        /// </summary>
        [DataMember]
        public string Version { get; set; }

        /// <summary>
        /// Returns a hash code that takes into account the name and host.
        /// </summary>
        public override int GetHashCode()
        {
            // Note that we need to Lower since we have it in equals because any thing for which
            // we equals returns true the Hash code should be the same.
            return HostAddress.ValueOrDefault(address => address.ToLower().GetHashCode()) ^ 
                Name.ValueOrDefault(name => name.ToLower().GetHashCode());
        }

        private int GetHashCode(string str)
        {
            return str.ToLower().GetHashCode();
        }

        /// <summary>
        /// Returns true if the other application reference is equal to this one.
        /// </summary>
        public override bool Equals(object obj)
        {
            return Equals(obj as ApplicationReference);
        }

        /// <summary>
        /// Returns true if the other application reference is equal to this one.
        /// </summary>
        public override bool Equals(ExperimentSourceReference other)
        {
            ApplicationReference otherReference = other as ApplicationReference;
            if (ReferenceEquals(otherReference, null))
            {
                return false;
            }
            else
            {
                return (HostAddress ?? string.Empty).ToLower() == (otherReference.HostAddress ?? string.Empty).ToLower() &&
                    (Name ?? string.Empty).ToLower() == (otherReference.Name ?? string.Empty).ToLower();
            }
        }

        /// <summary>
        /// Returns text that represents the reference.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} on {1}", Name, HostAddress);
        }
    }
}
