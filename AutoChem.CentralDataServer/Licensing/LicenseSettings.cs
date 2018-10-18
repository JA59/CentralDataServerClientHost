#region Header

/*
**COPYRIGHT:
**    This software program is furnished to the user under license
**  by METTLER TOLEDO AutoChem, and use thereof is subject to applicable 
**  U.S. and international law. This software program may not be 
**  reproduced, transmitted, or disclosed to third parties, in 
**  whole or in part, in any form or by any manner, electronic or
**  mechanical, without the express written consent of METTLER TOLEDO 
**  AutoChem, except to the extent provided for by applicable license.
**
**    Copyright © 2012 by Mettler Toledo AutoChemAutoChem.  All rights reserved.
**
**ENDHEADER:
*/

#endregion

using System;
using System.Runtime.Serialization;
using AutoChem.Core.CentralDataServer.Config;
using System.Diagnostics;

namespace AutoChem.Core.CentralDataServer.Licensing
{
    /// <summary>
    /// Contains the e-mail related settings.
    /// </summary>
    [DataContract(Namespace = ServicesHelper.TypeNameSpace)]
    [Serializable]
    public class LicenseSettings
    {
        /// <summary>
        /// The current state of the license
        /// </summary>
        public enum LicenseState
        {
            /// <summary>
            /// License not loaded
            /// </summary>
            NotInitialized,
            /// <summary>
            /// Everything is perfect
            /// </summary>
            Valid,
            /// <summary>
            /// Trial
            /// </summary>
            Trial,
            /// <summary>
            /// Trial Expires in 5 days or less
            /// </summary>
            TrialExpiring,
            /// <summary>
            /// Grace period for site license in effect
            /// </summary>
            GracePeriod,
            /// <summary>
            /// License expired or not found
            /// </summary>
            Expired
        }

        /// <summary>
        /// The current state of the license
        /// </summary>
        public LicenseState State { get; set; }
        
        /// <summary>
        /// License status for iC Data Center
        /// </summary>
        [DataMember]
        public bool IsICDataCenterLicensed { get; set; }

        /// <summary>
        /// License status for EasyMax and Optimax
        /// </summary>
        [DataMember]
        public bool IsRXELicensed { get; set; }

        /// <summary>
        /// License status for FBRM
        /// </summary>
        [DataMember]
        public bool IsFBRMLicensed { get; set; }

        /// <summary>
        /// License status for IR
        /// </summary>
        [DataMember]
        public bool IsIRLicensed { get; set; }

        /// <summary>
        /// License status for RC1e
        /// </summary>
        [DataMember]
        public bool IsRC1eLicensed { get; set; }

        /// <summary>
        /// License status for LabMax
        /// </summary>
        [DataMember]
        public bool IsLabMaxLicensed { get; set; }

        /// <summary>
        /// License status for PVM
        /// </summary>
        [DataMember]
        public bool IsPVMLicensed { get; set; }
        
        /// <summary>
        /// License status for Kaiser Raman
        /// </summary>
        [DataMember]
        public bool IsKaiserRamanLicensed { get; set; }

	    /// <summary>
	    /// License status for ReactRaman
	    /// </summary>
	    [DataMember]
	    public bool IsReactRamanLicensed { get; set; }

		/// <summary>
		/// True if trial
		/// </summary>
		[DataMember]
        public bool IsTrialLicense { get; set; }

        /// <summary>
        /// True if site
        /// </summary>
        [DataMember]
        public bool IsSiteLicense { get; set; }

        /// <summary>
        /// True if site
        /// </summary>
        [DataMember]
        public bool IsGracePeriod { get; set; }

        /// <summary>
        /// Days remaining in trial or grace period. 
        /// </summary>
        [DataMember]
        public int DaysRemaining { get; set; }

        /// <summary>
        /// Indicates status of current license. 
        /// String is empty when there are no issues
        /// </summary>
        [DataMember]
        public string Status { get; set; }

        /// <summary>
        /// Abbreviated version of the status string 
        /// String is empty when there are no issues
        /// </summary>
        [DataMember]
        public string ShortStatus { get; set; }

        /// <summary>
        /// Reminder message used for Email
        /// </summary>
        [DataMember]
        public string Reminder { get; set; }

	}
}
