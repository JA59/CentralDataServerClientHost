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
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace AutoChem.Core.Serialization
{
    /// <summary>
    /// Provides static serlization utility methods.
    /// </summary>
    public class SerializationUtility
    {
        /// <summary>
        /// Get the gets a serialization prefix for the member names based on 
        /// the type of object being serialzied and the type of the class
        /// that is calling this method.
        /// </summary>
        /// <param name="serializationTarget">The instance of the object being serialized</param>
        /// <param name="objectType">The type of the class that is calling this method</param>
        /// <returns>The prefix string</returns>
        public static string GetSerializationPrefix(object serializationTarget, Type objectType)
        {
            string prefix = "";
            if (serializationTarget.GetType() != objectType)
            {
                prefix = objectType.Name + "+";
            }

            return prefix;
        }
    }
}
