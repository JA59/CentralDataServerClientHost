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
using System.Threading;
using System.Diagnostics;

namespace AutoChem.Core.Threading
{
    internal class ThreadWrapper
    {
        private string m_Name;
        private ThreadStart m_ThreadStart;

        public ThreadWrapper(string name, ThreadStart threadStart)
        {
            m_Name = name;
            m_ThreadStart = threadStart;
        }

        public void Start()
        {
            try
            {
                m_ThreadStart();
            }
            catch (Exception exception)
            {
                Debug.WriteLine("An error occurred executing the thread " + m_Name + ".  " + exception);
            }
        }
    }
}
