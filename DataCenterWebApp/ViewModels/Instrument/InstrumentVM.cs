using Newtonsoft.Json;
using System;

namespace iCDataCenterClientHost.ViewModels.Instrument
{
    [JsonObject(MemberSerialization.OptOut)]
    public class InstrumentVM : IInstrumentVM
    {
        #region Constructor
        public InstrumentVM()
        {

        }
        #endregion

        #region Properties
        public string Address { get; set; }
        public string Description { get; set; }
        public string Instrument { get; set; }
        public string Status { get; set; }
        public string Reactor1 { get; set; }
        public string Reactor2 { get; set; }
        public string Version { get; set; }
        public string SerialNumber { get; set; }
        public string TimeDifference { get; set; }
        public DateTime LastUpdate { get; set; }
        #endregion
    }
}

