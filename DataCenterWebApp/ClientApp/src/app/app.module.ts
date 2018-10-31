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
import { InstrumentsPageComponent } from './Instruments/instruments-page/instruments-page.component';
import { InstrumentsConfigureComponent } from './Instruments/instruments-configure/instruments-configure.component';
import { InstrumentEditorComponent } from './Instruments/instrument-editor/instrument-editor.component';
import { InstrumentsMonitorComponent } from './Instruments/instruments-monitor/instruments-monitor.component';
import { InstrumentItemComponent } from './Instruments/instrument-item/instrument-item.component';
import { LogsEventsComponent } from './logs-events/logs-events.component';


@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    LoginComponent,
    ConfigurationComponent,
    DemoComponent,
    InstrumentsPageComponent,
    InstrumentsConfigureComponent,
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
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
