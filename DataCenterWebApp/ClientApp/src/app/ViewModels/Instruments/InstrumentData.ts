import { IInstrumentVM } from '../Instruments/IInstrumentVM';

export class InstrumentData {
  instrumentArray: IInstrumentVM[];
  instrumentCount: number;
  instrumentOverview: string;

  constructor(
    instrumentArray: IInstrumentVM[],
    instrumentCount: number,
    instrumentOverview: string,
  ) {
    this.instrumentArray = instrumentArray;
    this.instrumentCount = instrumentCount;
    this.instrumentOverview = instrumentOverview;
  }
}
