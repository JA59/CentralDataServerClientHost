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
        public string token { get; set; }
        public int expiration { get; set; }
        public string username { get; set; }
        public bool isadmin { get; set; }
        #endregion
    }
}
