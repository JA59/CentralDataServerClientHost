import { INewInstrumentViewModel } from '../Instruments/inewinstrumentviewmodel';

export class NewInstrumentViewModel implements INewInstrumentViewModel {
  Address: string;
  Description: string;

  constructor(address: string, description: string) {
    this.Address = address;
    this.Description = description;
  }
}
