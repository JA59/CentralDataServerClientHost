using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoChem.Core.CentralDataServer.Config;
using iCDataCenterClientHost.CustomIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace iCDataCenterClientHost.Controllers
{
    [Route("api/[controller]")]
    public class BaseApiController : Controller
    {
        #region Constructor
        public BaseApiController(
            RoleManager<DataCenterRole> roleManager,
            UserManager<DataCenterUser> userManager,
            IConfiguration configuration
            )
        {
            // Instantiate the required classes through DI
            RoleManager = roleManager;
            UserManager = userManager;
            Configuration = configuration;

            // Instantiate a single JsonSerializerSettings object
            // that can be reused multiple times.
            JsonSettings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented
            };

        }
        #endregion

        #region Shared Properties
        protected RoleManager<DataCenterRole> RoleManager { get; private set; }
        protected UserManager<DataCenterUser> UserManager { get; private set; }
        protected IConfiguration Configuration { get; private set; }
        protected JsonSerializerSettings JsonSettings { get; private set; }
        #endregion

        protected static Uri GetServerURI()
        {
            Uri hostURI = new Uri("http://localhost");
            Uri endPointUri = new Uri(hostURI + ProductHelper.UrlServiceRoot);
            return endPointUri;
        }

        /// <summary>
        /// Is the caller an administrator
        /// </summary>
        /// <returns></returns>
        protected bool IsAdmin()
        {
            return HasRole(DataCenterIdentities.AdminRole);
        }

        /// <summary>
        /// Is the caller a logged on user
        /// </summary>
        /// <returns></returns>
        protected bool IsUser()
        {
            return HasRole(DataCenterIdentities.UserRole);
        }

        /// <summary>
        /// Is the caller a guest (not logged on)
        /// </summary>
        /// <returns></returns>
        protected bool IsGuest()
        {
            return (!IsUser());
        }

        private bool HasRole(string role)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                DataCenterUser myUser = UserManager.FindByIdAsync(userId).Result;
                return myUser.Roles.Contains(role);
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// Get a string that contains a comma separated set of roles for the current user (based on the ClaimsPrincipal)
        /// </summary>
        /// <returns></returns>
        private string GetRoles()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                DataCenterUser myUser = UserManager.FindByIdAsync(userId).Result;
                string roles = String.Empty;
                foreach (string role in myUser.Roles)
                    roles = roles + role + ", ";
                return roles.Substring(0, roles.Length - 2);
            }
            catch (Exception)
            {
                return "<none>";
            }
        }

        /// <summary>
        /// Get a string that represents the currently logged on user (based on the ClaimsPrincipal)
        /// </summary>
        /// <returns></returns>
        private string GetUser()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                DataCenterUser myUser = UserManager.FindByIdAsync(userId).Result;
                return myUser.UserName;
            }
            catch (Exception)
            {
                return "Guest";
            }
        }


    }
}
