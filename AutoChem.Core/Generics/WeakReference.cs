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
**    Copyright © 2010 by Mettler Toledo AutoChem.  All rights reserved.
**
**ENDHEADER:
**/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoChem.Core.Generics
{
    /// <summary>
    /// A typed weak reference.
    /// </summary>
    public class WeakReference<T> where T : class
    {
        private WeakReference m_Reference;

        /// <summary>
        /// Creates a new WeakReference to the target.
        /// </summary>
        public WeakReference(T target)
        {
            m_Reference = new WeakReference(target);
        }

        /// <summary>
        /// True if the target is stil accessible.
        /// </summary>
        public bool IsAlive { get { return m_Reference.IsAlive; } }

        /// <summary>
        /// The target object or null if the target has been garbage collected.
        /// </summary>
        public T Target { get { return (T)m_Reference.Target; } }
    }
}
