/*
**  COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**  Copyright © 2007 by Mettler Toledo AutoChem.  All rights reserved.
**/

namespace AutoChem.Core.Versions
{
    /// <summary>
    /// Has the version for the AutoChem core library assemblies.
    /// </summary>
    public class SoftwareVersion
    {
		/// <summary>
		/// Mettler Toledo legal name 
		/// </summary>
		public const string Company = "Mettler-Toledo AutoChem, Inc.";

		/// <summary>
		/// Copyright info
		/// </summary>
		public const string Copyright = "© 2018 Mettler-Toledo AutoChem, Inc. All rights reserved";

        /// <summary>
        /// The Version number is made up of the MajorVersion, MinorVersion, Build, and Patch
        /// </summary>
        public const string AssemblyVersion = "1.40.14.0";

        /// <summary>
        /// The Version number is made up of the MajorVersion, MinorVersion, Build, and Patch
        /// </summary>
        public const string FileVersion = AssemblyVersion;
    }
}
