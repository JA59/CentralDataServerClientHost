import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { InstrumentIdVM } from '../../ViewModels/Instruments/InstrumentIdVM';
import { Component, Inject, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { AuthService } from '../../Services/auth.service';
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
  instrument: InstrumentIdVM = {
    vm_address: 'localhost',
    vm_description: 'simulated instrument'
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
      'address': [this.instrument.vm_address, [Validators.required]],
      'description': [this.instrument.vm_description, [Validators.maxLength(50)]]
    });
  }

  logFormValue() {
    var tempInstrument = <InstrumentIdVM>{};
    tempInstrument.vm_address = this.instrumentForm.value.address;
    tempInstrument.vm_description = this.instrumentForm.value.description;
    console.log("address is " + tempInstrument.vm_address);
    console.log("description is " + tempInstrument.vm_description);
    var url = this.baseUrl + "api/Instrument/Add";
    this.http.post<InstrumentIdVM>(url, tempInstrument)
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
