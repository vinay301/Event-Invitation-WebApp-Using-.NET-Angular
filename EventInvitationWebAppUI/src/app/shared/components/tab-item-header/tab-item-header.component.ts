import { Component, EventEmitter, HostBinding, HostListener, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';

@Component({
  selector: 'app-tab-item-header',
  templateUrl: './tab-item-header.component.html',
  styleUrls: ['./tab-item-header.component.css']
})
export class TabItemHeaderComponent implements OnInit, OnChanges {
@Input() Header:string = ''
@Input() UniqueCode:string = ''
@Input() IsActive: boolean = true;
@Output() Close : EventEmitter<string> = new EventEmitter();
@HostBinding( 'class' ) hostClass: string = '';
@HostListener('click') onHostSelected(){
  this.Selected.emit(this.UniqueCode)
}
@Output() Selected: EventEmitter<string> = new EventEmitter();
  constructor() { }

  ngOnInit() {
  }

  ngOnChanges(changes: SimpleChanges): void {
    if(changes['IsActive'] != undefined){
      if(changes['IsActive'].currentValue == true){
        this.hostClass = 'active'
      }
      else{
        this.hostClass = '';
      }
    }
  }

  closeTab(){
    this.Close.emit(this.UniqueCode);
  }

}
