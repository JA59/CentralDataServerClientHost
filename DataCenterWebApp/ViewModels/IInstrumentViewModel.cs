using System;

namespace DataCenterWebApp.ViewModels
{
    public interface IInstrumentViewModel
    {
        string Address { get; set; }
        string Description { get; set; }
        string Instrument { get; set; }
        DateTime LastUpdate { get; set; }
        string Reactor1 { get; set; }
        string Reactor2 { get; set; }
        string SerialNumber { get; set; }
        string Status { get; set; }
        string TimeDifference { get; set; }
        string Version { get; set; }
    }
}