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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Instruments
{
    /// <summary>
    /// The status of an instrument
    /// </summary>
    public enum InstrumentStatus
    {
        /// <summary>
        /// The system has not been able to connect to or determine the type of an instrument.
        /// </summary>
        NotVerified,
        /// <summary>
        /// The specific version of the instrument firmware is to old.
        /// </summary>
        VersionTooOld,
        /// <summary>
        /// The instrument is not supported.  That is the verify recognized the instrument at some level, but does not support the
        /// instrument and the reason is something other than just the version.
        /// </summary>
        Unsupported,
        /// <summary>
        /// The system has connected to and determined the type of the instrument.
        /// </summary>
        Verified,
        /// <summary>
        /// The specific version of the instrument firmware is to new.
        /// </summary>
        VersionTooNew
    }
}
