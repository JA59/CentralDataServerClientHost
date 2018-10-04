import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AuthService } from './services/auth.service';
import { AuthInterceptor } from './services/auth.interceptor';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { LoginComponent } from './login/login.component';
import { ConfigurationComponent } from './configuration/configuration.component';
import { DemoComponent } from './demo/demo.component';
import { HeaderBarComponent } from './headerbar/headerbar.component';
import { ExperimentComponent } from './experiments/experiment.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { PageNotFoundComponent } from './pagenotfound/pagenotfound.component';
import { InstrumentsComponent } from './instruments/instruments.component';
import { InstrumentItemComponent } from './instrument-item/instrument-item.component';
import { LogsEventsComponent } from './logs-events/logs-events.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    ConfigurationComponent,
    DemoComponent,
    InstrumentsComponent,
    InstrumentItemComponent,
    HeaderBarComponent,
    ExperimentComponent,
    DashboardComponent,
    LogsEventsComponent,
    PageNotFoundComponent

  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    CommonModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      { path: '', component: LoginComponent, pathMatch: 'full' },
      { path: 'login', component: LoginComponent },
      { path: 'demo', component: DemoComponent },
      { path: 'instruments', component: InstrumentsComponent },
      { path: 'experiments', component: ExperimentComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'logs-events', component: LogsEventsComponent },
      { path: 'configuration', component: ConfigurationComponent },
      { path: '**', component: PageNotFoundComponent }
    ])
  ],
  providers: [
    AuthService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
