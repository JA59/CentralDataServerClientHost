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

#if !SILVERLIGHT
using AutoChem.Core.CentralDataServer.Logging;
using AutoChem.Core.Licensing.Client;
#endif

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

#if !SILVERLIGHT
        /// <summary>
        /// Update the license from the file
        /// </summary>
        /// <returns></returns>
        public static LicenseSettings LoadSettings(IUserLogManager userLog)
        {

            LicenseSettings settings = new LicenseSettings();

            try
            {
                settings.State = LicenseState.NotInitialized;

                License license = new License(FolderHelper.AppDataPath);
                string error;
                bool success = license.Initialize(out error);

                if(!success)
                {
                    settings.Status = string.Format("Error Creating License: {0}", error);
                    settings.ShortStatus = "Error Creating License";
                    return settings;
                }

                settings.IsICDataCenterLicensed = license.IsValidVersion;
                var product = license.Products[0];
                if (settings.IsICDataCenterLicensed)
                {
                    settings.State = LicenseState.Valid;
                    //Product is licensed - look for warnings
                    int daysLeft = LicenseFileManager.TrialDaysLeft(product.ExpirationDateLocalTime);
                    if (product.IsSiteKey)
                    {
                        settings.IsSiteLicense = true;

                        if(product.ExpirationDateLocalTime.Year == 9999)
                        {
                            //basically, an unlimited site licese
                            settings.Status = "Site License valid forever";
                        }
                        else if (daysLeft >= 0)
                        {
                            //site license is valid and not expired
                            settings.Status = string.Format("Site License valid through {0}. {1} days remaining.", product.ExpirationDateLocalTime.ToShortDateString(), daysLeft);
                        }
                        else if (daysLeft >= -30)
                        {
                            //30 day grace period after license expires
                            int daysRemaining = daysLeft + 30;
                            settings.Status = string.Format("Site license expired on {0} - {1} days remaining to renew your license", product.ExpirationDateLocalTime.ToShortDateString(), daysRemaining);
                            settings.ShortStatus = string.Format("Site license expired - {0} days remaining to renew your license", daysRemaining);
                            settings.Reminder = string.Format("Your site license expired on {0}. You have {1} days remaining to renew your license.", product.ExpirationDateLocalTime.ToShortDateString(), daysRemaining);
                            settings.IsGracePeriod = true;
                            settings.DaysRemaining = daysRemaining;
                            settings.State = LicenseState.GracePeriod;
                        }                   
                    }
                    else if (product.IsTrialKey)
                    {
                        settings.IsTrialLicense = true;
                        settings.Status = string.Format("Trial License expires on {0} - {1} days remaining to use iC Data Center", product.ExpirationDateLocalTime.ToShortDateString(), daysLeft);
                        settings.ShortStatus = string.Format("Trial License - {0} days remaining", daysLeft);
                        settings.Reminder = string.Format("This is a trial version of iC Data Center. There are {0} days remaining to try the software.", daysLeft);
                        settings.DaysRemaining = daysLeft;
                        settings.State = daysLeft <= 5 ? LicenseState.TrialExpiring : LicenseState.Trial;
                    }

	                // Note: RX-10 is covered with the EasyMax/OptiMax license as well, but we didn't bother updating the SiteLicense ProductName string 
					var em = license.Products.Find(item => item.ProductName == "iC Data Center for EasyMax/OptiMax");  
                    if (em != null)
                    {
                        settings.IsRXELicensed = em.IsValidKey;
                    }

                    var ir = license.Products.Find(item => item.ProductName == "iC Data Center for ReactIR/FlowIR");
                    if (ir != null)
                    {
                        settings.IsIRLicensed = ir.IsValidKey;
                    }

                    var fbrm = license.Products.Find(item => item.ProductName == "iC Data Center for FBRM/ParticleTrack");
                    if (fbrm != null)
                    {
                        settings.IsFBRMLicensed = fbrm.IsValidKey;
                    }

                    var rc1e = license.Products.Find(item => item.ProductName == "iC Data Center for RC1e");
                    if (rc1e != null)
                    {
                        settings.IsRC1eLicensed = rc1e.IsValidKey;
                    }

                    var labmax = license.Products.Find(item => item.ProductName == "iC Data Center for LabMax");
                    if (labmax != null)
                    {
                        settings.IsLabMaxLicensed = labmax.IsValidKey;
                    }

                    var pvm = license.Products.Find(item => item.ProductName == "iC Data Center for PVM/ParticleView");
                    if (pvm != null)
                    {
                        settings.IsPVMLicensed = pvm.IsValidKey;
                    }
	                
                    var kaiserRaman = license.Products.Find(item => item.ProductName == "iC Data Center for Kaiser Raman");
                    if (kaiserRaman != null)
                    {
                        settings.IsKaiserRamanLicensed = kaiserRaman.IsValidKey;
                    }

					var reactRaman = license.Products.Find(item => item.ProductName == "iC Data Center for ReactRaman");
                    if (reactRaman != null)
                    {
                        settings.IsReactRamanLicensed = reactRaman.IsValidKey;
                    }

					Trace.TraceInformation(settings.Status);

                    if (settings.State == LicenseState.GracePeriod || settings.State == LicenseState.TrialExpiring)
                    {
                        userLog.AddEntry(LogEntryCreators.LicenseWarning, settings.ShortStatus);
                    }
                    else
                    {
                        // Error and warning use the same code, so this will mark either as resolved if there is one outstanding.
                        userLog.MarkResolved(LogEntryCreators.LicenseError);
                    }
                }
                else
                {
                    settings.State = LicenseState.Expired;
                    //Product not licensed - why not
                    if(product.IsTrialKey)
                    {
                        settings.Status = "The iC Data Center server license has expired";
                        settings.ShortStatus = "Trial License Expired - Experiment Collection Disabled";
                    }
                    else if(product.IsSiteKey)
                    {
                        settings.Status = "The iC Data Center server license has expired";
                        settings.ShortStatus = "Site License Expired - Experiment Collection Disabled";
                    }
                    else
                    {
                        settings.ShortStatus = settings.Status = "The iC Data Center license was not found - Experiment Collection Disabled";
                    }

                    Trace.TraceInformation(settings.Status);

                    userLog.AddEntry(LogEntryCreators.LicenseError, settings.ShortStatus);
                }
            }
            catch (UnauthorizedAccessException ex)
            {
                Trace.TraceError(string.Format("Failed to initialize license: Unauthorized access: {0}", ex));
                throw;
            }
            catch (Exception ex)
            {
                Trace.TraceError(string.Format("Unexpected Error While Validating Software License: {0}", ex));
				throw;
            }
            return settings;
        }
#endif
	}
}
