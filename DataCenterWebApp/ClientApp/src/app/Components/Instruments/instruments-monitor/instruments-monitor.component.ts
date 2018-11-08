import { Component, Inject, OnInit, OnDestroy } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from '../../../Services/auth.service';
import { InstrumentService } from '../../../Services/instrument.service';
import { HttpClient } from '@angular/common/http';
import { IInstrumentVM } from '../../../ViewModels/Instruments/IInstrumentVM';
import { InstrumentData } from '../../../ViewModels/Instruments/InstrumentData';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: "instruments-monitor",
  templateUrl: "./instruments-monitor.component.html",
  styleUrls: ['./instruments-monitor.component.css']
})

export class InstrumentsMonitorComponent implements OnInit, OnDestroy {
  title = "Monitor Instruments";
  instrumentData: InstrumentData;
  instrumentSubscription: Subscription | null = null;

  constructor(private router: Router,
      private authService: AuthService,
      private instrumentService: InstrumentService,
      @Inject('BASE_URL') private baseUrl: string,
      private http: HttpClient) {
  }

  ngOnInit() {
    // subscribe to changes from the InstrumentService
    this.instrumentData = this.instrumentService.getInstrumentData();
    this.instrumentSubscription = this.instrumentService.instrumentObservable.subscribe(data => {
      this.instrumentData = data;
    });
  }

  // ngOnDestroy()
  // Ask the service to stop collecting, and
  // unsubscribe from the service
  //
  ngOnDestroy() {
    if (this.instrumentSubscription != null) {
      this.instrumentSubscription.unsubscribe();
    }
  }

  setSelectedName(event: IInstrumentVM) {
    this.removeInstrument(event.vm_address);
  }

  private removeInstrument(address: string) {
    console.log("address is " + address);

    var url = this.baseUrl + "api/Instrument/"+address;
    this.http.delete<string>(url)
      .subscribe(this.extractData), error => console.log(error);
  }

  extractData(res: any) {
    console.log("extract data: " + res);
    //let body = res.json();
    //return body || {};
  }


}
