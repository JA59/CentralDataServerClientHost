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

namespace AutoChem.Core.Events
{
    /// <summary>
    /// Defines the basic properties of an EventList.
    /// </summary>
    public interface IEventList
    {
        /// <summary>
        /// The time since the last time events were retrieved from the list.
        /// </summary>
        TimeSpan ElapsedTimeSinceLastGetEventsCall { get; }

        /// <summary>
        /// Gets the events that have not been retrieved yet.
        /// </summary>
        T GetEvents<T>() where T: class, new();
    }
}
