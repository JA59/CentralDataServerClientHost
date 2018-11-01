import { Component, Input, Output, EventEmitter } from '@angular/core';
import { IInstrumentVM } from '../../ViewModels/Instruments/IInstrumentVM';

@Component({
  selector: 'instrument-item',
  templateUrl: './instrument-item.component.html',
  styleUrls: ['./instrument-item.component.css']
})

export class InstrumentItemComponent {
  @Input() myInstrument: IInstrumentVM;
  @Output() selectEvent = new EventEmitter();

  constructor() {
  }


  itemSelected() {
    this.selectEvent.emit(this.myInstrument);
  }
}


