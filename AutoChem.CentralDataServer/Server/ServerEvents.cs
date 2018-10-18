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

using AutoChem.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Server
{
    /// <summary>
    /// A collection of events that have occurred on the server.
    /// </summary>
    [DataContract]
    public class ServerEvents : EventList<ServerEventType, IServerObserver>
    {
        /// <summary>
        /// Creates the client side actions for the events.
        /// </summary>
        protected override void CreateEventActions()
        {
            m_EventActions[ServerEventType.ConfigChanged] =
                (Action<IServerObserver>)((client) => client.ConfigChanged());
        }

        /// <summary>
        /// Registers a notification that the configuration has changed.
        /// </summary>
        public void NotifyConfigChanged()
        {
            AddEventEntry(ServerEventType.ConfigChanged);
        }
    }
}
