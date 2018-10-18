using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace AutoChem.Core.CentralDataServer.General
{
    /// <summary>
    /// Defines the operations available to get server settings.
    /// </summary>
    [ServiceContract(Namespace = ServicesHelper.ServiceNameSpace)]
    public interface IGeneralAuthorizationService
    {
        /// <summary>
        /// Call that a client can make to determine if the server is configured for anonymous access
        /// </summary>
        /// <returns>True if the server is configured for anonymous access</returns>
        [OperationContract]
        bool IsAnonymousAccessAllowed();

        /// <summary>
        /// Call that a client can make to determine if it is executing on the same PC as the server
        /// </summary>
        /// <returns>True if client and server are executing on the same PC</returns>
        [OperationContract]
        bool IsClientComputerSameAsServer();
    }
}
