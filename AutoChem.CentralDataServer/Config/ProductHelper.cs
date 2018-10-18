using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoChem.Core.CentralDataServer.Config
{
    /// <summary>
    /// Helper methods for the product as a whole.
    /// </summary>
    public class ProductHelper
    {
        /// <summary>
        /// The name of the product
        /// </summary>
        public static string ProductName
        {
            get
            {
                return "iC Data Center";
            }
        }

        /// <summary>
        /// Relative Url for services
        /// </summary>
        public const string UrlServiceRoot = "/Service";

        /// <summary>
        /// Releative Url for the Authorization service (relative to Service root).
        /// </summary>
        public const string UrlAuthorization = "/Authorization/UserAuthorization";

        /// <summary>
        /// Releative Url for the Configuration management service (relative to Service root).
        /// </summary>
        public const string UrlConfigurationManagement = "/ConfigurationManagement";
        /// <summary>
        /// Releative Url for the Configuration management service (relative to Service root).
        /// </summary>
        public const string UrlConfigurationManagementAdmin = "/ConfigurationManagementAdmin";

        /// <summary>
        /// Releative Url for the InstrumentManagement service (relative to Service root).
        /// </summary>
        public const string UrlInstrumentManagement = "/InstrumentManagement";

        /// <summary>
        /// Releative Url for the InstrumentManagement service (relative to Service root).
        /// </summary>
        public const string UrlInstrumentManagementAdmin = "/InstrumentManagementAdmin";

        /// <summary>
        /// Releative Url for the PlannedExperimentManagement service (relative to Service root).
        /// </summary>
        public const string UrlPlannedExperimentManagement = "/PlannedExperimentManagement";

        /// <summary>
        /// Releative Url for the PlannedExperimentManagementAdmin service (relative to Service root).
        /// </summary>
        public const string UrlPlannedExperimentManagementAdmin = "/PlannedExperimentManagementAdmin";

        /// <summary>
        /// Releative Url for the ExperimentManagement service (relative to Service root).
        /// </summary>
        public const string UrlExperimentManagement = "/ExperimentManagement";
        
        /// <summary>
        /// Releative Url for the ExperimentManagement service (relative to Service root).
        /// </summary>
        public const string UrlExperimentInformation = "/ExperimentInformation";

        /// <summary>
        /// Releative Url for the GeneralManagement service (relative to Service root).
        /// </summary>
        public const string UrlGeneralManagement = "/GeneralManagement";
        
        /// <summary>
        /// Releative Url for the GeneralManagement service (relative to Service root).
        /// </summary>
        public const string UrlGeneralManagementAdmin = "/GeneralManagementAdmin";
        
        /// <summary>
        /// Releative Url for the GeneralAuthorization service (relative to Service root).
        /// </summary>
        public const string UrlGeneralAuthorization = "/GeneralAuthorization";

        /// <summary>
        /// Releative Url for the ApplicationManagement service (relative to Service root).
        /// </summary>
        public const string UrlApplicationManagement = "/ApplicationManagement";

        /// <summary>
        /// Releative Url for the DashBoardManagement service (relative to Service root).
        /// </summary>
        public const string UrlDashBoardManagement = "/DashBoardManagement";

        /// <summary>
        /// Releative Url for the LicenseManagement service (relative to Service root).
        /// </summary>
        public const string UrlLicenseManagement = "/LicenseManagement";

        /// <summary>
        /// Releative Url for the FileServer service (relative to Service root).
        /// </summary>
        public const string UrlFileServer = "/FileServer";

        /// <summary>
        /// Relative Url for the Authorization service (relative for base URL).
        /// </summary>
        public const string UrlAuthorizationService = UrlServiceRoot + UrlAuthorization;

        /// <summary>
        /// Relative Url for the Configuration management service (relative for base URL).
        /// </summary>
        public const string UrlConfigurationManagementService = UrlServiceRoot + UrlConfigurationManagement;
        
        /// <summary>
        /// Relative Url for the Configuration management service (relative for base URL).
        /// </summary>
        public const string UrlConfigurationManagementServiceAdmin = UrlServiceRoot + UrlConfigurationManagementAdmin;

        /// <summary>
        /// Relative Url for the InstrumentManagement service (relative for base URL).
        /// </summary>
        public const string UrlInstrumentManagementService = UrlServiceRoot + UrlInstrumentManagement;

        /// <summary>
        /// Relative Url for the InstrumentManagement service (relative for base URL).
        /// </summary>
        public const string UrlInstrumentManagementServiceAdmin = UrlServiceRoot + UrlInstrumentManagementAdmin;

        /// <summary>
        /// Relative Url for the PlannedExperimentManagement service (relative for base URL).
        /// </summary>
        public const string UrlPlannedExperimentManagementService = UrlServiceRoot + UrlPlannedExperimentManagement;

        /// <summary>
        /// Relative Url for the PlannedExperimentManagement Admin service (relative for base URL).
        /// </summary>
        public const string UrlPlannedExperimentManagementServiceAdmin = UrlServiceRoot + UrlPlannedExperimentManagementAdmin;

        /// <summary>
        /// Relative Full Url for the ExperimentManagement service (relative for base URL).
        /// </summary>
        public const string UrlExperimentManagementService = UrlServiceRoot + UrlExperimentManagement;
        
        /// <summary>
        /// Relative Full Url for the ExperimentManagement service (relative for base URL).
        /// </summary>
        public const string UrlExperimentInformationService = UrlServiceRoot + UrlExperimentInformation;

        /// <summary>
        /// Releative Full Url for the ApplicationManagement service (relative to base URL).
        /// </summary>
        public const string UrlApplicationManagementService = UrlServiceRoot + UrlApplicationManagement;
        /// <summary>
        /// Relative Full Url for the DashboardManagement service (relative to base URL).
        /// </summary>
        public const string UrlDashboardManagementService = UrlServiceRoot + UrlDashBoardManagement;

        /// <summary>
        /// Relative Full Url for the FileServer service (relative for base URL).
        /// </summary>
        public const string UrlFileServerService = UrlServiceRoot + UrlFileServer;

        /// <summary>
        /// If the version string is set and is a valid version number then a Version object with this information is returned.
        /// Otherwise null is returned.
        /// </summary>
        internal static Version GetVersion(string versionString)
        {
            Version version = null;

            if (!string.IsNullOrWhiteSpace(versionString))
            {
                bool parsed = Version.TryParse(versionString, out version);
                if (!parsed)
                {
                    version = null;
                }
            }

            return version;
        }
    }
}
