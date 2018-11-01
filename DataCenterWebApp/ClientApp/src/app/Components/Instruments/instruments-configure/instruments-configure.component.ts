import { Component, Inject, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from '../../../Services/auth.service';
import { HttpClient } from '@angular/common/http';
import { IInstrumentVM } from '../../../ViewModels/Instruments/IInstrumentVM';

@Component({
    selector: "instruments-configure",
  templateUrl: "./instruments-configure.component.html",
  styleUrls: ['./instruments-configure.component.css']
})

export class InstrumentsConfigureComponent implements OnInit {
  title = "Configure Instruments";
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


}
