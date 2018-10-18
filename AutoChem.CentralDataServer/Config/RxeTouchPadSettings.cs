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
using System.Runtime.Serialization;
using System.ComponentModel;

namespace AutoChem.Core.CentralDataServer.Config
{
    // Note ideally this class would not be in core, but since it would also be need in the silver application
    // We will put it here for now since there is not an easy way to inject assemblies into silverlight xap file.
    // Also, we don't want to load the components from the other domains into the main domain, so by keeping this 
    // in AutoChem we also avoid that.
    /// <summary>
    /// Settings specific or at least with different values for the RxeTouchpad based experiment collection.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class RxeTouchPadSettings : DeviceSettings
    {
        /// <summary>
        /// The file specifying the report content for experiments downloaded from Rxe touchpad based devices.
        /// </summary>
        [DataMember]
        public string ReportLayoutFile { get; set; }

        /// <summary>
        /// Overrides the default minimum version supported by the system.  Normally this should be null.
        /// This is stored as a string because there seemed to be some issues with serializing a version object between Silverlight and regular .net.
        /// </summary>
        [DataMember(Name = "MinimumVersionOverride")]
        [DefaultValue(null)]
        public string _MinimumVersionOverride { get; set; }

        /// <summary>
        /// Overrides the default maximum version supported by the system.  Normally this should be null.
        /// This is stored as a string because there seemed to be some issues with serializing a version object between Silverlight and regular .net.
        /// </summary>
        [DataMember(Name = "MaximumVersionOverride")]
        [DefaultValue(null)]
        public string _MaximumVersionOverride { get; set; }

        /// <summary>
        /// Gets the minimum version as a Version object.  Null is returned if the version is not set or if the version can not be parsed.
        /// </summary>
        public Version MinimumVersionOverride
        {
            get { return ProductHelper.GetVersion(_MinimumVersionOverride); }
        }

        /// <summary>
        /// Gets the maximum version as a Version object.  Null is returned if the version is not set or if the version can not be parsed.
        /// </summary>
        public Version MaximumVersionOverride
        {
            get { return ProductHelper.GetVersion(_MaximumVersionOverride); }
        }
    }
}
