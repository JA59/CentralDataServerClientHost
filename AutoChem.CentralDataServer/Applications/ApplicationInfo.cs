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

namespace AutoChem.Core.CentralDataServer.Applications
{
    /// <summary>
    /// Contains information about an application that has connected to the system.
    /// </summary>
    [DataContract(Namespace=ServicesHelper.TypeNameSpace)]
    [Serializable] // Because of the app domain intra process communication.
    public class ApplicationInfo : IEquatable<ApplicationInfo>
    {
        /// <summary>
        /// Creates an empty ApplicationInfo
        /// </summary>
        public ApplicationInfo()
        {
        }

        /// <summary>
        /// The name of the application.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The version of the application.
        /// </summary>
        [DataMember]
        public Version Version { get; set; }

        /// <summary>
        /// The address of the computer on which the application is running.
        /// </summary>
        [DataMember]
        public string HostAddress { get; set; }

        /// <summary>
        /// A reference to the application.
        /// </summary>
        public ApplicationReference Reference
        {
            get { return new ApplicationReference { HostAddress = HostAddress, Name = Name, Version=Version.ToString() }; }
        }

        /// <summary>
        /// Returns true if the value of the fields are equal to the ones in other.
        /// </summary>
        public bool Equals(ApplicationInfo other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }
            else
            {
                return Name == other.Name && Version == other.Version && HostAddress == other.HostAddress;
            }
        }

        /// <summary>
        /// Returns information about the application as a string.
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} version {1} on {2}", Name, Version, HostAddress);
        }
    }
}
