using Newtonsoft.Json;
using System;

namespace iCDataCenterClientHost.ViewModels.Instrument
{
    [JsonObject(MemberSerialization.OptOut)]
    public interface IInstrumentIdVM
    {
        string vm_address { get; set; }
        string vm_description { get; set; }
    }
}