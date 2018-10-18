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
    /// Manages information about the instruments the system is interacting with.
    /// </summary>
    public interface IInstrumentManager
    {
        /// <summary>
        /// Gets the instruments registered on the system.
        /// </summary>
        IEnumerable<LiveInstrumentInfo> GetRegisteredInstruments();

        /// <summary>
        /// Adds an instrument to the system.
        /// </summary>
        InstrumentInfo AddInstrument(string instrumentAddress, string hostDescription);

        /// <summary>
        /// Remove an instrument to the system.
        /// </summary>
        void RemoveInstrument(string instrumentAddress);

        /// <summary>
        /// Update the last successful communication time for an instrument
        /// </summary>
        /// <param name="instrumentAddress"></param>
        /// <param name="lastSuccessfulCommunication"></param>
        void UpdateInstrumentLastSuccessfulCommunication(string instrumentAddress, DateTime lastSuccessfulCommunication);
    }
}
