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
**    Copyright © 2006 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AutoChem.Core.Threading
{
    /// <summary>
    /// Used to create threads with exception handling.
    /// </summary>
    public class ThreadCreator
    {
        /// <summary>
        /// Creates a new thread with the provided name and method to execute.  This does not
        /// start the thread it just creates the thread with exception handling wrapping the
        /// method to execute.
        /// </summary>
        /// <param name="name">The name of the thread.</param>
        /// <param name="threadStart">The for the thread to execute.</param>
        public static Thread CreateThread(string name, ThreadStart threadStart)
        {
            ThreadWrapper wrapper = new ThreadWrapper(name, threadStart);
            Thread thread = new Thread(wrapper.Start);
            thread.Name = name;

            return thread;
        }
    }
}
