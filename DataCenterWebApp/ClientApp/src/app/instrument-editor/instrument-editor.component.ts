import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NewInstrumentViewModel } from '../interfaces/newinstrumentviewmodel';
import { INewInstrumentViewModel } from '../interfaces/inewinstrumentviewmodel';
import { Component, Inject, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from '../services/auth.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from "rxjs";
import 'rxjs/Rx';

@Component({
  selector: 'instrument-editor',
  templateUrl: './instrument-editor.component.html',
  styleUrls: ['./instrument-editor.component.css']
})
export class InstrumentEditorComponent implements OnInit {
  title = "Configure Instruments";
  instrumentForm: FormGroup;
  instrument: NewInstrumentViewModel = {
    Address: 'localhost',
    Description: 'simulated instrument'
  };
  constructor
    (private formBuilder: FormBuilder,
    private router: Router,
    private authService: AuthService,
    @Inject('BASE_URL') private baseUrl: string,
    private http: HttpClient) {

  }
  ngOnInit() {
    this.instrumentForm = this.formBuilder.group({
      'address': [this.instrument.Address, [Validators.required]],
      'description': [this.instrument.Description, [Validators.maxLength(50)]]
    });
  }

  logFormValue() {
    var tempInstrument = <NewInstrumentViewModel>{};
    tempInstrument.Address = this.instrumentForm.value.address;
    tempInstrument.Description = this.instrumentForm.value.description;
    console.log("address is " + tempInstrument.Address);
    console.log("description is " + tempInstrument.Description);
    var url = this.baseUrl + "api/Instrument/Add";
    this.http.post<NewInstrumentViewModel>(url, tempInstrument)
      .subscribe(this.extractData), error => console.log(error);
  }

  extractData(res: any) {
    console.log("extract data");
    //let body = res.json();
    //return body || {};
  }
  handleErrorObservable(error: Response | any) {
    console.log("error");
    //console.error(error.message || error);
    return Observable.throw(error.message || error);
  } 

 

}
