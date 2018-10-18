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
**    Copyright © 2009 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace AutoChem.Core.Events
{
    /// <summary>
    /// An event entry for an event of type T.  T is often an Enum that indicates the event.
    /// In general this class is only used by the base infrastructure, but needs to be public for Silverlight DataContract use.
    /// </summary>
    [DataContract]
    public class EventEntry<T>
    {
        /// <summary>
        /// The event type and the arguments for the event.
        /// </summary>
        public EventEntry(T eventType, object[] args)
        {
            EventType = eventType;
            Args = args;
        }

        /// <summary>
        /// The type of the event.  This is often an enum.
        /// </summary>
        [DataMember]
        public T EventType { get; set; }
        /// <summary>
        /// The arguments for the event.
        /// </summary>
        [DataMember]
        public object[] Args { get; set; }
    }
}
