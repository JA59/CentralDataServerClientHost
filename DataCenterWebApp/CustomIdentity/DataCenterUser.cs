using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace iCDataCenterClientHost.CustomIdentity
{
    public class DataCenterUser : IdentityUser<int>
    {
        public DataCenterUser()
        {
            this.Roles = new List<string>();
        }
        public IList<string> Roles { get; set; }
    }

}
