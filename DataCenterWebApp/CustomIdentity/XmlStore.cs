using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.CustomIdentity
{
    public class XmlStore
    {
        private static XmlStore instance = null;
        private Dictionary<int, MyUser> m_UserDb = new Dictionary<int, MyUser>();
        private Dictionary<string, MyRole> m_RoleDb = new Dictionary<string, MyRole>();

        public static XmlStore Instance
        {
            get
            {
                if (instance == null)
                    instance = new XmlStore();
                return instance;
            }
        }

        public XmlStore()
        {
            // TODO = Read this from an XML file
            m_UserDb = new Dictionary<int, MyUser>()
            {
                {1, new MyUser(){ Id = 1, UserName = "SomeUser", PasswordHash="USER1", Roles= new List<string>(){"user" }} },
                {2, new MyUser(){ Id = 2, UserName = "SomeAdmin", PasswordHash="ADMIN1", Roles= new List<string>(){"user", "admin" }} },
                {3, new MyUser(){ Id = 3, UserName = "Joe", PasswordHash="JOE", Roles= new List<string>(){"user", "admin" }} },
                {4, new MyUser(){ Id = 4, UserName = "Ed", PasswordHash="ED", Roles= new List<string>(){"user" }} }
            };

            m_RoleDb = new Dictionary<string, MyRole>()
            {
                {"user", new MyRole(){ Id = "user", Name = "User"} },
                {"admin", new MyRole(){ Id = "admin", Name = "Administrator"} }
            };
    }

        public Dictionary<int, MyUser> UserDb
        {
            get { return m_UserDb; }
        }

        public Dictionary<string, MyRole> RoleDb
        {
            get { return m_RoleDb; }
        }
    }
}
