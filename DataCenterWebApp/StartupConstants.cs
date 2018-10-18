using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCDataCenterClientHost
{
    public static class StartupConstants
    {
        // Adjust the following constants to set simulation and run mode 
        private const RunMode _RunMode = RunMode.WindowsApp;

        /// <summary>
        /// RunMode
        /// </summary>
        public static RunMode RunMode = _RunMode;
    }

    /// <summary>
    /// Run modes
    /// </summary>
    public enum RunMode
    {
        WindowsApp,
        Service,
    }
}
