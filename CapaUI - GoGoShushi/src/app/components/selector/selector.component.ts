import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-selector',
  templateUrl: './selector.component.html',
  styleUrls: ['./selector.component.scss'],
})
export class SelectorComponent implements OnInit {

  @Input() value: number = 0;
  @Output() valueAdd: EventEmitter<boolean> = new EventEmitter();
  @Output() valueRm:  EventEmitter<boolean> = new EventEmitter();
  
  constructor() { }

  ngOnInit() {}


  add(){
    this.valueAdd.next(true);
  }

  rm(){
    this.valueRm.next(false);
  }

}
