import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AuthService } from './Services/auth.service';
import { InstrumentService } from './Services/instrument.service';
import { AuthInterceptor } from './Services/auth.interceptor';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './Components/nav-menu/nav-menu.component';
import { LoginComponent } from './Components/login/login.component';
import { ConfigurationComponent } from './Components/configuration/configuration.component';
import { DemoComponent } from './Components/demo/demo.component';
import { HeaderBarComponent } from './Components/headerbar/headerbar.component';
import { ExperimentComponent } from './Components/experiments/experiment.component';
import { DashboardComponent } from './Components/dashboard/dashboard.component';
import { PageNotFoundComponent } from './Components/pagenotfound/pagenotfound.component';
import { InstrumentsPageComponent } from './Components/Instruments/instruments-page/instruments-page.component';
import { InstrumentEditorComponent } from './Components/Instruments/instrument-editor/instrument-editor.component';
import { InstrumentsMonitorComponent } from './Components/Instruments/instruments-monitor/instruments-monitor.component';
import { InstrumentItemComponent } from './Components/Instruments/instrument-item/instrument-item.component';
import { LogsEventsComponent } from './Components/logs-events/logs-events.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    ConfigurationComponent,
    DemoComponent,
    InstrumentsPageComponent,
    InstrumentEditorComponent,
    InstrumentsMonitorComponent,
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
      { path: 'instruments-page', component: InstrumentsPageComponent },
      { path: 'experiments', component: ExperimentComponent },
      { path: 'dashboard', component: DashboardComponent },
      { path: 'logs-events', component: LogsEventsComponent },
      { path: 'configuration', component: ConfigurationComponent },
      { path: '**', component: PageNotFoundComponent }
    ])
  ],
  providers: [
    AuthService,
    InstrumentService,
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
