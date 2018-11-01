using AutoChem.Core.CentralDataServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Xml;

namespace iCDataCenterClientHost.CustomIdentity
{
    /// <summary>
    /// DataCenterIdentities stores the set of users and roles.
    /// Roles are hardcoded ("user" and "admin")
    /// Users are loaded from a text file.
    /// 
    /// Users are available in the UserDictionary
    /// Roles are available in the RoleDictionary
    /// </summary>
    public class DataCenterIdentities
    {
        public const string UserRole = "user";
        public const string AdminRole = "admin";

        // Singleton instance
        private static DataCenterIdentities instance = null;

        /// <summary>
        /// Dictionary of Users
        /// </summary>
        public Dictionary<int, DataCenterUser> UserDictionary { get; private set; }

        /// <summary>
        /// Dictionary of Roles
        /// </summary>
        public Dictionary<string, DataCenterRole> RoleDictionary { get; private set; }

        /// <summary>
        /// Singleton Instance
        /// </summary>
        public static DataCenterIdentities Instance
        {
            get
            {
                if (instance == null)
                    instance = new DataCenterIdentities();
                return instance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DataCenterIdentities()
        {
            UserDictionary = new Dictionary<int, DataCenterUser>();
            RoleDictionary = new Dictionary<string, DataCenterRole>();
            AddRoles();
            AddUsers();
        }


        /// <summary>
        /// Add roles to the RoleDictionary
        /// </summary>
        private void AddRoles()
        {
            // Roles are not user defined; there are always two roles; "user" and "admin".
            RoleDictionary = new Dictionary<string, DataCenterRole>()
            {
                {UserRole, new DataCenterRole(){ Id = UserRole, Name = "User"} },
                {AdminRole, new DataCenterRole(){ Id = AdminRole, Name = "Administrator"} }
            }; 
        }

        /// <summary>
        /// Add users to the UserDictionary
        /// </summary>
        private void AddUsers()
        {
            // Try to load the users from the user editable text file.
            if (!LoadUsersFromTextFile())
            {
                // Load from text file failed, so create a default set of users
                UserDictionary = new Dictionary<int, DataCenterUser>()
                {
                    {1, new DataCenterUser(){ Id = 1, UserName = "SomeUser", PasswordHash="USER1", Roles= new List<string>(){UserRole }} },
                    {2, new DataCenterUser(){ Id = 2, UserName = "SomeAdmin", PasswordHash="ADMIN1", Roles= new List<string>(){ UserRole, AdminRole }} },
                    {3, new DataCenterUser(){ Id = 3, UserName = "Joe", PasswordHash="JOE", Roles= new List<string>(){ UserRole, AdminRole }} },
                    {4, new DataCenterUser(){ Id = 4, UserName = "Ed", PasswordHash="ED", Roles= new List<string>(){UserRole }} }
                };
            }
        }

        /// <summary>
        /// Try to parse the text file containing users
        /// </summary>
        /// <returns>True if at least one user was added (UserDictionary is valid.)</returns>
        private bool LoadUsersFromTextFile()
        {
            try
            {
                var path = "users.txt";
                UserDictionary = new Dictionary<int, DataCenterUser>();
                int index = 1;
                using (StreamReader reader = File.OpenText(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // skip comment lines
                        if (line.StartsWith(';'))
                            continue;

                        // remove everything after the comment character (;)
                        if (line.IndexOf(';') > 0)
                        {
                            line = line.Substring(0, line.IndexOf(';'));
                        }

                        // parse line role: name,password
                        var pieces = line.Split(':');
                        if (pieces.Length != 2) continue;

                        // get the user name and password
                        var subPieces = pieces[1].Split(',');
                        if (subPieces.Length != 2) continue;
                        
                        // determine the roles
                        var roles = new List<string>() { UserRole };
                        if (pieces[0] == AdminRole) roles.Add(AdminRole);

                        // Create the user and add it to the dictionary
                        var myUser = new DataCenterUser()
                        {
                            Id = index,
                            UserName = subPieces[0].Trim(),
                            PasswordHash = subPieces[1].Trim(),
                            Roles = roles
                        };
                        UserDictionary.Add(myUser.Id, myUser);
                        index++;
                    }
                }

                // If at least one user exists in the dictionary, then we consider the load to be successful.
                return (UserDictionary.Count > 0);
            }
            catch(Exception)
            {
                return false;
            }

        }
    }
}
