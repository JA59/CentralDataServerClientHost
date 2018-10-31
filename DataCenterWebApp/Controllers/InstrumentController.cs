using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using iCDataCenterClientHost.CustomIdentity;
using iCDataCenterClientHost.ViewModels;
using System.Collections.Generic;
using AutoChem.Core.CentralDataServer.Instruments;
using AutoChem.Core.CentralDataServer;
using System.ServiceModel;
using AutoChem.Core.CentralDataServer.Config;
using System.Linq;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.Controllers
{
    /// <summary>
    /// Controller for handling the Instrument route
    /// </summary>
    [Route("api/[controller]")]
    public class InstrumentController : BaseApiController
    {
        private readonly ILogger<InstrumentController> _logger;
        #region Constructor
        public InstrumentController(
            RoleManager<MyRole> roleManager,            // role manager - ued to obtain roles for the currently logged on user
            UserManager<MyUser> userManager,            // user manager - used to obtain the currently logged on user 
            IConfiguration configuration,               // configuration - not used (needed to construct base class)
            ILogger<InstrumentController> logger        // logger (not in base class because it is for type InstrumentController)

            ) : base(roleManager, userManager, configuration)
        {
            _logger = logger;
        }
        #endregion

        /// <summary>
        /// GET: api/SystemOverview/Summary}
        /// Retrieves the specified page of planned experiments
        /// </summary>
        /// <param name="num">the number of planned experiments to retrieve</param>
        /// <returns>{num} Planned Experiments sorted by User</returns>
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("RegisteredInstruments")]
        public async Task<IActionResult> RegisteredInstruments()
        {
            try
            {
                // Log that we were called.
                _logger.LogInformation("RegisteredInstruments");

                IEnumerable<LiveInstrumentInfo> enumerable;
                InstrumentManagementClientAsync m_Client;

                string serverUriString = string.Format("{0}{1}", GetServerURI(), ProductHelper.UrlInstrumentManagement);
                m_Client = new InstrumentManagementClientAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(serverUriString));

                enumerable = await m_Client.GetRegisteredInstrumentsAsync();
                LiveInstrumentInfo[] array = enumerable.Cast<LiveInstrumentInfo>().ToArray();

                List<IInstrumentViewModel> list = new List<IInstrumentViewModel>();
                foreach(var liveInstrumentInfo in array)
                {
                    var instrumentViewModel = new InstrumentViewModel();
                    instrumentViewModel.Address = liveInstrumentInfo.HostAddress;
                    instrumentViewModel.Description = liveInstrumentInfo.HostAddress;
                    instrumentViewModel.Instrument = liveInstrumentInfo.HostAddress;
                    instrumentViewModel.Status = liveInstrumentInfo.Status.ToString();
                    instrumentViewModel.Reactor1 = liveInstrumentInfo.Reactor1Value;
                    instrumentViewModel.Reactor2 = liveInstrumentInfo.Reactor2Value;
                    instrumentViewModel.Version = liveInstrumentInfo.InstrumentInfo.Version;
                    instrumentViewModel.SerialNumber = liveInstrumentInfo.InstrumentInfo.SerialNumber;
                    instrumentViewModel.TimeDifference = liveInstrumentInfo.TimeDifference.HasValue ? liveInstrumentInfo.TimeDifference.Value.ToString() : "";
                    instrumentViewModel.LastUpdate = liveInstrumentInfo.LastSuccessfulCommunication.HasValue ? liveInstrumentInfo.LastSuccessfulCommunication.Value : DateTime.MinValue;
                    list.Add(instrumentViewModel);
                }

                // Return the result in JSON format
                return new JsonResult(
                    list.ToArray(),
                    new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented
                    });
            }
            catch (Exception exc)
            {
                // Log the exception and return it to the caller
                _logger.LogError(exc.ToString());
                throw;
            }
        }

        /// <summary>
        /// GET: api/SystemOverview/Summary}
        /// Retrieves the specified page of planned experiments
        /// </summary>
        /// <param name="num">the number of planned experiments to retrieve</param>
        /// <returns>{num} Planned Experiments sorted by User</returns>
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("Count")]
        public async Task<IActionResult> Count()
        {
            try
            {
                // Log that we were called.
                _logger.LogInformation("Count");

                IEnumerable<LiveInstrumentInfo> enumerable;
                InstrumentManagementClientAsync m_Client;

                string serverUriString = string.Format("{0}{1}", GetServerURI(), ProductHelper.UrlInstrumentManagement);
                m_Client = new InstrumentManagementClientAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(serverUriString));

                enumerable = await m_Client.GetRegisteredInstrumentsAsync();
                LiveInstrumentInfo[] array = enumerable.Cast<LiveInstrumentInfo>().ToArray();
                int count = array.Length;

                // Return the result in JSON format
                return new JsonResult(
                    count,
                    new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented
                    });
            }
            catch (Exception exc)
            {
                // Log the exception and return it to the caller
                _logger.LogError(exc.ToString());
                throw;
            }
        }

        /// <summary>
        /// Deletes the Planned Experiment with the given {id} from the Database
        /// DELETE: api/plannedexperiment/{id}
        /// </summary>
        /// <param name="id">The ID of an existing planned experiment</param>
        [HttpDelete("{address}")]
        public async Task<IActionResult> Delete(string address)
        {
            try
            {
                // Log that we were called.
                _logger.LogInformation("Delete");
                if (!HasRole("admin"))
                {
                    return new UnauthorizedResult();
                }

                if (address == null)
                    return new StatusCodeResult(500);

                InstrumentManagementAdminClientAsync m_Client;

                string serverUriString = string.Format("{0}{1}", GetServerURI(), ProductHelper.UrlInstrumentManagementAdmin);
                m_Client = new InstrumentManagementAdminClientAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(serverUriString));

                await m_Client.RemoveInstrumentAsync(address);

                // Return the result in JSON format
                return new JsonResult(
                    address,
                    new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented
                    });
            }
            catch (Exception exc)
            {
                // Log the exception and return it to the caller
                _logger.LogError(exc.ToString());
                throw;
            }
        }

        /// <summary>
        /// Add an instrument 
        /// POST: api/instrument/{object}
        /// </summary>
        /// <param name="newInstrument"></param>
        [HttpPost("Add")]
        public async Task<IActionResult> Add([FromBody]NewInstrumentViewModel newInstrument)
        {
            try
            {
                // Log that we were called.
                _logger.LogInformation("AddInstrument");

                if (newInstrument == null)
                    return new StatusCodeResult(500);

                InstrumentInfo instrumentInfo;
                InstrumentManagementAdminClientAsync m_Client;

                string serverUriString = string.Format("{0}{1}", GetServerURI(), ProductHelper.UrlInstrumentManagementAdmin);
                m_Client = new InstrumentManagementAdminClientAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(serverUriString));

                instrumentInfo = await m_Client.AddInstrumentAsync(newInstrument.Address, newInstrument.Description);

                // Return the result in JSON format
                return new JsonResult(
                    instrumentInfo.HostAddress,
                    new JsonSerializerSettings()
                    {
                        Formatting = Formatting.Indented
                    });
            }
            catch (Exception exc)
            {
                // Log the exception and return it to the caller
                _logger.LogError(exc.ToString());
                throw;
            }
        }

        private bool HasRole(string role)
        {
            //return true;
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                MyUser myUser = UserManager.FindByIdAsync(userId).Result;
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
                MyUser myUser = UserManager.FindByIdAsync(userId).Result;
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
                MyUser myUser = UserManager.FindByIdAsync(userId).Result;
                return myUser.UserName;
            }
            catch (Exception)
            {
                return "Guest";
            }
        }

        public static Uri GetServerURI()
        {
            Uri hostURI = new Uri("http://localhost");
            Uri endPointUri = new Uri(hostURI + ProductHelper.UrlServiceRoot);
            return endPointUri;
        }
    }
}

