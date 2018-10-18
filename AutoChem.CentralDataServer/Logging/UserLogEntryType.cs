using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoChem.Core.CentralDataServer.Logging
{
    /// <summary>
    /// The types of user log entries.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    public enum UserLogEntryType
    {
        /// <summary>
        /// An informational message
        /// </summary>
        [EnumMember]
        Info,
        /// <summary>
        /// A warning
        /// </summary>
        [EnumMember]
        Warning,

        /// <summary>
        /// An error
        /// </summary>
        [EnumMember]
        Error
    }
}
