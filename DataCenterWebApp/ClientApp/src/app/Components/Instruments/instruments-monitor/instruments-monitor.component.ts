import { Component, Inject, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from '../../../Services/auth.service';
import { HttpClient } from '@angular/common/http';
import { IInstrumentVM } from '../../../ViewModels/Instruments/IInstrumentVM';

@Component({
    selector: "instruments-monitor",
  templateUrl: "./instruments-monitor.component.html",
  styleUrls: ['./instruments-monitor.component.css']
})

export class InstrumentsMonitorComponent implements OnInit {
  title = "Monitor Instruments";
  instrumentCount: number = 0;
  instruments: IInstrumentVM[];

    constructor(private router: Router,
        private authService: AuthService,
      @Inject('BASE_URL') private baseUrl: string,
      private http: HttpClient) {
  }

  ngOnInit() {
    // subscribe to changes from the SystemOverviewService
    this.doFetch();
  }

  private doFetch() {
    // Get the latest experiment count from the SystemOverview controller
    this.http.get<IInstrumentVM[]>('api/Instrument/RegisteredInstruments').subscribe(result => {
      this.instruments = result;
      this.instrumentCount = this.instruments.length;
    }, error => {
      this.instruments = new Array(0);
      this.instrumentCount = -1;
    });
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
