using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public class NewInstrumentViewModel : INewInstrumentViewModel
    {
        #region Constructor
        public NewInstrumentViewModel()
        {

        }
        #endregion

        #region Properties
        public string Address { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
