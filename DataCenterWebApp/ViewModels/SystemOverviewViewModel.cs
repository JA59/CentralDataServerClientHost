using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCenterWebApp.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class SystemOverviewViewModel
    {
        #region Constructor
        public SystemOverviewViewModel()
        {

        }
        #endregion

        #region Properties
        public DateTime LastImportDate { get; set; }
        public int ExperimentCount { get; set; }
        public int HighestSequenceID { get; set; }
        public string ICDataCenterAddress { get; set; }
        public string ICDataCenterVersion { get; set; }
        public string ICDataCenterStatus { get; set; }
        public string DataCenterWebAppAddress { get; set; }
        public string DataCenterWebAppVersion { get; set; }
        public string DataCenterWebAppStatus { get; set; }
        public string LoggedOnUser { get; set; }
        public string LoggedOnRole { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion
    }
}
