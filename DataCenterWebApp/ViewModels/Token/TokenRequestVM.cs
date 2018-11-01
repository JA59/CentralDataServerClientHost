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
        public string grant_type { get; set; }
        public string client_id { get; set; }
        public string client_secret { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        #endregion
    }
}
