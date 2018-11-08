import { IInstrumentVM } from '../Instruments/IInstrumentVM';

// Object provided by InstrumentService subscription
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
