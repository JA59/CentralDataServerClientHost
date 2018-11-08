import { IInstrumentVM } from '../Instruments/IInstrumentVM';

// ViewModel describing an instrument received from the InstrumentController when obtaining registered instruments.
export class InstrumentVM implements IInstrumentVM {
  vm_address: string;
  vm_description: string;
  vm_instrument: string;
  vm_last_update: Date;
  vm_reactor_1: string;
  vm_reactor_2: string;
  vm_serial_number: string;
  vm_status: string;
  vm_time_difference: string;
  vm_version: string;

  constructor(
    address: string,
    description: string,
    instrument: string,
    last_update: Date,
    reactor_1: string,
    reactor_2: string,
    serial_number: string,
    status: string,
    time_difference: string,
    version: string
  ) {
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
}
