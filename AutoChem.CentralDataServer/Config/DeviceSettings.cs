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
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using AutoChem.Core.Reflection;

namespace AutoChem.Core.CentralDataServer.Config
{
    /// <summary>
    /// A base class for device specific settings 
    /// </summary>
    [DataContract(Namespace=ServicesHelper.TypeNameSpace)]
    [KnownType("GetKnownDeviceSettings")]
    [Serializable]
    public abstract class DeviceSettings
    {
        private static Type[] m_KnownDeviceSettingTypes;

        /// <summary>
        /// Returns the types of the DeviceSettings that are part of this installation.
        /// </summary>
        public static IEnumerable<Type> GetKnownDeviceSettings()
        {
            if (m_KnownDeviceSettingTypes == null)
            {
                var currentAssembly = Assembly.GetExecutingAssembly();
                m_KnownDeviceSettingTypes = ReflectionUtility.GetTypesFromAssembly<DeviceSettings>(currentAssembly).ToArray();
            }

            return m_KnownDeviceSettingTypes;
        }
    }
}
