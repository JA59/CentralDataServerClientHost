import { Component, Inject, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { IInstrumentViewModel } from '../interfaces/iinstrumentviewmodel';

@Component({
    selector: "instruments-monitor",
  templateUrl: "./instruments-monitor.component.html",
  styleUrls: ['./instruments-monitor.component.css']
})

export class InstrumentsMonitorComponent implements OnInit {
  title = "Monitor Instruments";
  instrumentCount: number = 0;
  instruments: IInstrumentViewModel[];

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
    this.http.get<IInstrumentViewModel[]>('api/Instrument/RegisteredInstruments').subscribe(result => {
      this.instruments = result;
      this.instrumentCount = this.instruments.length;
    }, error => {
      this.instruments = new Array(0);
      this.instrumentCount = -1;
    });
  }


}