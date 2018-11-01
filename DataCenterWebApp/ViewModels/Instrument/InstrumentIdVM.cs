using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iCDataCenterClientHost.ViewModels.Instrument
{
    [JsonObject(MemberSerialization.OptOut)]
    public class InstrumentIdVM : IInstrumentIdVM
    {
        #region Constructor
        public InstrumentIdVM()
        {

        }
        #endregion

        #region Properties
        public string Address { get; set; }
        public string Description { get; set; }
        #endregion
    }
}
