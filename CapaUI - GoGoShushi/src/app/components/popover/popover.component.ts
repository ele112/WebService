import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AuthenticationService } from 'src/app/services/Authentication.service';
import { PopoverController } from '@ionic/angular';

@Component({
  selector: 'app-popover',
  templateUrl: './popover.component.html',
  styleUrls: ['./popover.component.scss'],
})
export class PopoverComponent implements OnInit {

  @Input() value: number = 0;
  @Output() valueAdd: EventEmitter<boolean> = new EventEmitter();
  @Output() valueRm:  EventEmitter<boolean> = new EventEmitter();
  
  constructor(private authService: AuthenticationService, private popover: PopoverController) { }

  ngOnInit() {}


  add(){
    this.valueAdd.next(true);
  }

  rm(){
    this.valueRm.next(false);
  }

  logout(){
    this.popover.dismiss();
    this.authService.logout();

  }

}
