import { Component, OnDestroy, OnInit, Type, ViewChild, ViewContainerRef, viewChild } from '@angular/core';
import { ITab } from '../../../core/models/tab';
import { Subscription } from 'rxjs';
import { TabService } from '../../../core/services/tab.service';
import { EventListComponent } from '../event-list/event-list.component';
import { UserEventListComponent } from '../user-event-list/user-event-list.component';
import { UserInvitesListComponent } from '../user-invites-list/user-invites-list.component';

@Component({
  selector: 'app-TabControl',
  templateUrl: './TabControl.component.html',
  styleUrls: ['./TabControl.component.css']
})
export class TabControlComponent implements OnInit, OnDestroy {
  tabItemSubscription: Subscription;
  index:number = 0;
  tabs:ITab[] = [];
  @ViewChild('containerRef', {read: ViewContainerRef, static: true}) containerRef!:ViewContainerRef;
  constructor(private tabService: TabService) { 
    this.tabItemSubscription = tabService.tabItemObservable.subscribe({
      next:(res:string) => {this.addNewTab(res)},
      error: (err:any) => {
        console.error('Error: ', err)
      },
    })
  }


  ngOnInit() {
  }

  addNewTab(code:string){
    var uniqueCode = code + '-' + this.index;
    this.index++;

    this.containerRef.detach();
    var component = this.containerRef.createComponent(this.getComponentType(code))
    component.instance.IsActive = true;
    
    for(let tab of this.tabs){
      tab.content.instance.IsActive = false;
    }

    this.tabs.unshift({
      header : code,
      uniqueCode : uniqueCode,
      content: component,
      view: this.containerRef.get(0)!
    })
  }

  getComponentType(code:string) : Type<any> {
    var type : Type<any> = EventListComponent;
    switch(code){
      case "All Events" :
        type = EventListComponent;
        break;
      case "Your Events" :
        type = UserEventListComponent;
        break;
      case "Your Invites" :
        type = UserInvitesListComponent;
        break;
    }
    return type;
  }

  selectTab(uniqueCode:string){
    for(let tab of this.tabs){
      if(tab.uniqueCode == uniqueCode){
        tab.content.instance.IsActive = true;
        this.containerRef.detach();
        this.containerRef.insert(tab.view)
      }
      else{
        tab.content.instance.IsActive = false;
      }
    }
  }

  ngOnDestroy(): void {
    this.tabItemSubscription.unsubscribe();
  }

}
