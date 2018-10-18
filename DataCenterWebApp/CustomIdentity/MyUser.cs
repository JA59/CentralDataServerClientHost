using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace iCDataCenterClientHost.CustomIdentity
{
    public class MyUser : IdentityUser<int>
    {
        public MyUser()
        {
            this.Roles = new List<string>();
        }
        public IList<string> Roles { get; set; }
    }

}
