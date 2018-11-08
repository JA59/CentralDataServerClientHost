import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject } from 'rxjs/Subject';
import { InstrumentData } from '../ViewModels/Instruments/InstrumentData';
import { IInstrumentVM } from '../ViewModels/Instruments/IInstrumentVM';

@Injectable()
export class InstrumentService {

  private lastInstrumentData: InstrumentData;                                           // cache of the last instrument data
  private instrumentSubject = new Subject<InstrumentData>();                            // subject for observation

  public instrumentObservable = this.instrumentSubject.asObservable();                  // Observable IInstrumentVM[] stream

  // constructor
  constructor(  @Inject('BASE_URL') private baseUrl: string,
                private http: HttpClient)
  {
    this.lastInstrumentData = new InstrumentData(undefined, -2, 'Initializing ...');
  }

  // doFetch
  // Fetch the number of instruments if we are a guest
  // Fetch the set of instruments if we are logged in (guests may not see the set of instruments)
  public doFetch(isGuest: boolean) {
    if (isGuest) {
      // We are a guest, only fetch the instrument count
      this.doFetchCount();
    } else {
      // We are logged in as a valid user or admin, so obtain the list of instruments
      this.doFetchInstruments();
    }
  }

  // getInstrumentData()
  // Returns the most recently obtained InstrumentData
  public getInstrumentData(): InstrumentData {
    return this.lastInstrumentData;
  }

  // private methods

  private doFetchCount() {
    // Get the latest from the controller
    this.http.get<number>('api/Instrument/Count').subscribe(result => {
      console.log("got instrument count");
      this.lastInstrumentData = new InstrumentData(undefined, result, 'There are ' + result + ' instruments.');
      this.Notify();
    }, () => {
      console.log("failed to get instruments");
      this.setAsError();
      this.Notify();
    });
  }

  private doFetchInstruments() {
    // Get the count from the controller
    this.http.get<IInstrumentVM[]>('api/Instrument/RegisteredInstruments').subscribe(result => {
      console.log("got instruments");
      this.lastInstrumentData = new InstrumentData(result, result.length, 'There are ' + result.length + ' instruments.');
      this.Notify();
    }, () => {
      console.log("failed to get instruments");
      this.setAsError();
      this.Notify();
    });
  }

  private setAsError() {
    this.lastInstrumentData = new InstrumentData(undefined, -1, 'Unable to retrieve Instrument count from the server.');
  }
  private Notify() {
    this.instrumentSubject.next(this.lastInstrumentData);
  }

}


