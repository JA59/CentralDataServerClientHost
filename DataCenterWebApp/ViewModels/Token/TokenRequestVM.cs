using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.ViewModels.Token
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenRequestVM
    {
        #region Constructor
        public TokenRequestVM()
        {

        }
        #endregion

        #region Properties
        public string vm_grant_type { get; set; }
        public string vm_client_id { get; set; }
        public string vm_client_secret { get; set; }
        public string vm_username { get; set; }
        public string vm_password { get; set; }
        #endregion
    }
}
