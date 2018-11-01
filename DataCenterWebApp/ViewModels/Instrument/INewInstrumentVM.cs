using Newtonsoft.Json;
using System;

namespace iCDataCenterClientHost.ViewModels.Instrument
{
    [JsonObject(MemberSerialization.OptOut)]
    public interface IInstrumentIdVM
    {
        string Address { get; set; }
        string Description { get; set; }
    }
}