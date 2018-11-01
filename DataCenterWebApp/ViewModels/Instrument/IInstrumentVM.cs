using System;

namespace iCDataCenterClientHost.ViewModels.Instrument
{
    public interface IInstrumentVM
    {
        string vm_address { get; set; }
        string vm_description { get; set; }
        string vm_instrument { get; set; }
        DateTime vm_last_update { get; set; }
        string vm_reactor_1 { get; set; }
        string vm_reactor_2 { get; set; }
        string vm_serial_number { get; set; }
        string vm_status { get; set; }
        string vm_time_difference { get; set; }
        string vm_version { get; set; }
    }
}