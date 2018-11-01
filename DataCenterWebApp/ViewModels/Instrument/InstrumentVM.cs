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
        public string vm_address { get; set; }
        public string vm_description { get; set; }
        public string vm_instrument { get; set; }
        public string vm_status { get; set; }
        public string vm_reactor_1 { get; set; }
        public string vm_reactor_2 { get; set; }
        public string vm_version { get; set; }
        public string vm_serial_number { get; set; }
        public string vm_time_difference { get; set; }
        public DateTime vm_last_update { get; set; }
        #endregion
    }
}

