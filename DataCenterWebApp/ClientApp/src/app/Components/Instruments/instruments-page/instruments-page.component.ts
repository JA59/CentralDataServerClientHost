import { Component, Inject, OnInit, OnDestroy } from "@angular/core";
import { InstrumentService } from '../../../Services/instrument.service';
import { AuthService } from '../../../Services/auth.service';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import { HttpClient } from '@angular/common/http';
import { IInstrumentVM } from '../../../ViewModels/Instruments/IInstrumentVM';



@Component({
    selector: "instruments-page",
  templateUrl: "./instruments-page.component.html",
  styleUrls: ['./instruments-page.component.css']
})

export class InstrumentsPageComponent implements OnInit, OnDestroy {
  private timer;
  private sub: Subscription;
  authSubscription: Subscription | null = null;


  title = "Instruments";

  constructor(private authService: AuthService,
    private instrumentService: InstrumentService,
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient) {
  }

  ngOnInit() {
    this.authSubscription = this.authService.authObservable.subscribe(() => {
      this.instrumentService.doFetch(this.authService.isLoggedInAsGuest());
    });
    this.timer = Observable.timer(2000, 1000); // after two seconds, fire every second
    // subscribing to a observable returns a subscription object
    this.sub = this.timer.subscribe(() => this.instrumentService.doFetch(this.authService.isLoggedInAsGuest()));

  }

  ngOnDestroy() {
    //stop subscribing to the timer object
    this.sub.unsubscribe();
  }
}
