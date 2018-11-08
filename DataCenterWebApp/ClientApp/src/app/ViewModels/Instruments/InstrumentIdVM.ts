import { IInstrumentIdVM } from '../Instruments/IInstrumentIdVM';

// ViewModel passed to the InstrumentController when adding or deleting an instrument.
export class InstrumentIdVM implements IInstrumentIdVM {
  vm_address: string;
  vm_description: string;

  constructor(address: string, description: string) {
    this.vm_address = address;
    this.vm_description = description;
  }
}
