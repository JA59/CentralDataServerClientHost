import { Injectable, Inject } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { IInstrumentVM } from '../ViewModels/Instruments/IInstrumentVM';
import { InstrumentData } from '../ViewModels/Instruments/InstrumentData';
import { HttpClient } from '@angular/common/http';


//
// InstrumentService
//
// The instrument service stores the latest array of instruments, and a count for the number of instruments.
//
// A component can subscribe to this service to be notified when he list of instruments, or the instrument count, has changed.
//
// The method getLatest() returns the most recent IInstrumentVM[].
//
// A component can also subscribe to the instrumentSubject to be notified of changes to the most recent
// IInstrumentVM[].
// A component can also subscribe to the instrumentCountSubject to be notified of changes to the count of instruments
// 

@Injectable()
export class InstrumentService {

  private lastInstrumentData: InstrumentData;                                         // cache of the last instrument data

  private instrumentSubject = new Subject<InstrumentData>();                            // instrument array subject for observation
  public instrumentObservable = this.instrumentSubject.asObservable();                  // Observable IInstrumentVM[] stream

  //
  // Constructor
  //
  constructor(  @Inject('BASE_URL') private baseUrl: string,
                private http: HttpClient)
  {
    this.lastInstrumentData = new InstrumentData(undefined, -2, 'Initializing ...');
  }

  public doFetch(isGuest: boolean) {
    // If we are an Admin or  user, we can see the list of instruments.
    // If we are a guest, then we can only fetch the number of instruments
    if (isGuest) {
      this.doFetchCount();
    } else {
      this.doFetchInstruments();
    }
  }

  private doFetchInstruments() {
    // Get the count from the controller
    this.http.get<IInstrumentVM[]>('api/Instrument/RegisteredInstruments').subscribe(result => {
      console.log("got instruments");
      this.lastInstrumentData = new InstrumentData(result, result.length, 'There are ' + result.length + ' instruments.');
      this.Notify();
    }, () => {
      console.log("failed to get instruments");
      this.lastInstrumentData = new InstrumentData(undefined, -1, 'Unable to retrieve Instrument count from the server.');
      this.Notify();
    });
  }

  private doFetchCount() {
    // Get the latest from the controller
    this.http.get<number>('api/Instrument/Count').subscribe(result => {
      console.log("got instrument count");
      this.lastInstrumentData = new InstrumentData(undefined, result, 'There are ' + result + ' instruments.');
      this.Notify();
    }, () => {
      console.log("failed to get instruments");
      this.lastInstrumentData = new InstrumentData(undefined, -1, 'Unable to retrieve Instrument count from the server.');
      this.Notify();
    });
  }

  //
  // getInstruments()
  // Returns the most recently obtained instrument array
  //
  public getInstrumentData(): InstrumentData {
    //console.log("InstrumentService getInstruments()")
    return this.lastInstrumentData;
  }

   private Notify() {
    this.instrumentSubject.next(this.lastInstrumentData);
  }

}


