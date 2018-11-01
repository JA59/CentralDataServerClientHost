using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

using iCDataCenterClientHost.CustomIdentity;
using iCDataCenterClientHost.ViewModels.Instrument;
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="roleManager">Role Manager - used to obtain roles for the currently logged on user</param>
        /// <param name="userManager">User Manager - used to obtain the currently logged on user </param>
        /// <param name="configuration">Configuration - not used (needed to construct base class)</param>
        /// <param name="logger">logger (not in base class because it is for type InstrumentController)</param>
        public InstrumentController(
            RoleManager<DataCenterRole> roleManager,             
            UserManager<DataCenterUser> userManager,             
            IConfiguration configuration,                       
            ILogger<InstrumentController> logger) : base(roleManager, userManager, configuration)
        {
            _logger = logger;
        }


        /// <summary>
        /// GET: api/Instrument/RegisteredInstruments}
        /// Retrieves the list of registered instrument
        /// </summary>
        /// <returns>List of registered instruments as IInstrumentViewModel[]</returns>
        [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
        [HttpGet("RegisteredInstruments")]
        public async Task<IActionResult> RegisteredInstruments()
        {
            try
            {
                // Log that we were called.
                _logger.LogInformation("RegisteredInstruments");

                // Caller must be a logged on user in order to request the list of registered instruments
                if (!IsUser())
                {
                    return new UnauthorizedResult();
                }

                IEnumerable<LiveInstrumentInfo> enumerable;
                InstrumentManagementClientAsync m_Client;

                string serverUriString = string.Format("{0}{1}", GetServerURI(), ProductHelper.UrlInstrumentManagement);
                m_Client = new InstrumentManagementClientAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(serverUriString));

                enumerable = await m_Client.GetRegisteredInstrumentsAsync();
                LiveInstrumentInfo[] array = enumerable.Cast<LiveInstrumentInfo>().ToArray();

                List<IInstrumentVM> list = new List<IInstrumentVM>();
                foreach(var liveInstrumentInfo in array)
                {
                    var instrumentViewModel = new InstrumentVM();
                    instrumentViewModel.vm_address = liveInstrumentInfo.HostAddress;
                    instrumentViewModel.vm_description = liveInstrumentInfo.HostAddress;
                    instrumentViewModel.vm_instrument = liveInstrumentInfo.HostAddress;
                    instrumentViewModel.vm_status = liveInstrumentInfo.Status.ToString();
                    instrumentViewModel.vm_reactor_1 = liveInstrumentInfo.Reactor1Value;
                    instrumentViewModel.vm_reactor_2 = liveInstrumentInfo.Reactor2Value;
                    instrumentViewModel.vm_version = liveInstrumentInfo.InstrumentInfo.Version;
                    instrumentViewModel.vm_serial_number = liveInstrumentInfo.InstrumentInfo.SerialNumber;
                    instrumentViewModel.vm_time_difference = liveInstrumentInfo.TimeDifference.HasValue ? liveInstrumentInfo.TimeDifference.Value.ToString() : "";
                    instrumentViewModel.vm_last_update = liveInstrumentInfo.LastSuccessfulCommunication.HasValue ? liveInstrumentInfo.LastSuccessfulCommunication.Value : DateTime.MinValue;
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
                if (!IsAdmin())
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
        public async Task<IActionResult> Add([FromBody]InstrumentIdVM newInstrument)
        {
            try
            {
                // Log that we were called.
                _logger.LogInformation("Add");
                if (!IsAdmin())
                {
                    return new UnauthorizedResult();
                }

                if (newInstrument == null)
                    return new StatusCodeResult(500);

                InstrumentInfo instrumentInfo;
                InstrumentManagementAdminClientAsync m_Client;

                string serverUriString = string.Format("{0}{1}", GetServerURI(), ProductHelper.UrlInstrumentManagementAdmin);
                m_Client = new InstrumentManagementAdminClientAsync(ServicesHelper.GetDefaultBinding(),
                    new EndpointAddress(serverUriString));

                instrumentInfo = await m_Client.AddInstrumentAsync(newInstrument.vm_address, newInstrument.vm_description);

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
    }
}

