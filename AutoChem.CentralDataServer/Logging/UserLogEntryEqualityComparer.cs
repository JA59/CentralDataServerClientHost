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
using System.Text;
using AutoChem.Core.Generics;

namespace AutoChem.Core.CentralDataServer.Logging
{
    /// <summary>
    /// Determines if two entries are equal not including the resolved state.
    /// </summary>
    public class UserLogEntryEqualityComparer : IEqualityComparer<UserLogEntry>
    {
        /// <summary>
        /// Returns true if the x and y represent the same message.
        /// </summary>
        public bool Equals(UserLogEntry x, UserLogEntry y)
        {
            bool xNull = ReferenceEquals(x, null);
            bool yNull = ReferenceEquals(y, null);
            if (xNull != yNull)
            {
                return false;
            }
            else if (xNull)
            {
                // Note because of the previous check y is also null if we get here.
                return true;
            }
            else
            {
                return
                    x.Code == y.Code &&
                    x.Timestamp == y.Timestamp &&
                    x.InstrumentOrApplication.SafeEquals(y.InstrumentOrApplication) &&
                    x.Experiment.SafeEquals(y.Experiment) &&
                    x.Message == y.Message;
            }
        }

        /// <summary>
        /// Gets a hash code which takes into account the components used for determining equality.
        /// </summary>
        public int GetHashCode(UserLogEntry entry)
        {
            return
                entry.Code ^
                entry.Timestamp.GetHashCode() ^ 
                entry.InstrumentOrApplication.ValueOrDefault(source => source.GetHashCode()) ^
                entry.Experiment.ValueOrDefault(experiment => experiment.GetHashCode()) ^ 
                entry.Message.GetHashCode();
        }
    }
}
