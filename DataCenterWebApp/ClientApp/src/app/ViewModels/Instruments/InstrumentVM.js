"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
// ViewModel describing an instrument received from the InstrumentController when obtaining registered instruments.
var InstrumentVM = /** @class */ (function () {
    function InstrumentVM(address, description, instrument, last_update, reactor_1, reactor_2, serial_number, status, time_difference, version) {
        this.vm_address = address;
        this.vm_description = description;
        this.vm_instrument = instrument;
        this.vm_last_update = last_update;
        this.vm_reactor_1 = reactor_1;
        this.vm_reactor_2 = reactor_2;
        this.vm_serial_number = serial_number;
        this.vm_status = status;
        this.vm_time_difference = time_difference;
        this.vm_version = version;
    }
    return InstrumentVM;
}());
exports.InstrumentVM = InstrumentVM;
//# sourceMappingURL=InstrumentVM.js.map