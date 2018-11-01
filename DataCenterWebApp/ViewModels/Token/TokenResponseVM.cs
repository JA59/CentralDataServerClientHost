using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.ViewModels.Token
{
    [JsonObject(MemberSerialization.OptOut)]
    public class TokenResponseVM
    {
        #region Constructor
        public TokenResponseVM()
        {

        }
        #endregion

        #region Properties
        public string vm_token { get; set; }
        public int vm_expiration { get; set; }
        public string vm_username { get; set; }
        public bool vm_isadmin { get; set; }
        #endregion
    }
}
