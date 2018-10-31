using Newtonsoft.Json;
using System;

namespace iCDataCenterClientHost.ViewModels
{
    [JsonObject(MemberSerialization.OptOut)]
    public interface INewInstrumentViewModel
    {
        string Address { get; set; }
        string Description { get; set; }
    }
}