using System;
using System.Reflection;

namespace AutoChem.Core.CentralDataServer.Versions
{
    /// <summary>
    /// Has the version for the Central Data Service.
    /// </summary>
    public class SoftwareVersion
    {
        /// <summary>
        /// The Version number is made up of the MajorVersion, MinotVersion, Build, and Patch
        /// Note please do not use this in code as when it changes it causes unnecessary test impact results.
        /// Please use AssemblyVersionObject instead.
        /// However this needs to be used for the AssemblyVersion attribute as it needs a constant expression.
        /// </summary>
        public const string AssemblyVersion = "6.1.150.0";

        /// <summary>
        /// File version
        /// </summary>
        public const string FileVersion = AssemblyVersion;

		/// <summary>
		/// Mettler Toledo legal name 
		/// </summary>
		public const string Company = "Mettler-Toledo AutoChem, Inc.";

		/// <summary>
		/// Copyright info
		/// </summary>
		public const string Copyright = "© 2018 Mettler-Toledo AutoChem, Inc. All rights reserved";

        /// <summary>
        /// The ReleaseType string indicates Alpha or Beta. Leave blank for normal releases.
        /// This field is read by ConfigFileCreator and added to the exe.config file as an application setting
        /// Release build for blank
        /// Beta build for "Beta"
        /// </summary>
        public const string AssemblyReleaseCandidate = "";

        /// <summary>
        /// The AssemblyVersion as a Version object.
        /// </summary>
        public static readonly Version AssemblyVersionObject = new AssemblyName(Assembly.GetExecutingAssembly().FullName).Version;
        /// <summary>
        /// The major version
        /// MajorVersion.x.x.x
        /// </summary>
        public static readonly int MajorVersion = AssemblyVersionObject.Major;

        /// <summary>
        /// The minor version
        /// x.MinorVersion.x.x
        /// </summary>
        public static readonly int MinorVersion = AssemblyVersionObject.Minor;

        /// <summary>
        /// The build number
        /// x.x.Build.x
        /// </summary>
        public static readonly int Build = AssemblyVersionObject.Build;

        /// <summary>
        /// The patch number
        /// x.x.x.Patch`
        /// </summary>
        public static readonly int Patch = AssemblyVersionObject.Revision;

        /// <summary>
        /// Subfolder name based upon version number
        /// SubfolderName.x.x
        /// </summary>
        public static readonly string SubfolderName = MajorVersion + "." + MinorVersion;
    }
}
