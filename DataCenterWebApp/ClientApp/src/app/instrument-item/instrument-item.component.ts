import { Component, Input, Output, EventEmitter } from '@angular/core';
import { IInstrumentViewModel } from '../interfaces/iinstrumentviewmodel';

@Component({
  selector: 'instrument-item',
  templateUrl: './instrument-item.component.html',
  styleUrls: ['./instrument-item.component.css']
})

export class InstrumentItemComponent {
  @Input() myInstrument: IInstrumentViewModel;
  @Output() selectEvent = new EventEmitter();

  constructor() {
  }


  itemSelected() {
    this.selectEvent.emit(this.myInstrument);
  }
}


