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

  closeTab(uniqueCode:string){
    var tabToClose: ITab | null = null;
    var index = -1
    for(let i=0; i< this.tabs.length; i++)
      if(this.tabs[i].uniqueCode == uniqueCode){
        tabToClose = this.tabs[i];
        index = i;
      }

      this.removeTab(tabToClose!,index)
  }

  removeTab(tabToRemove: ITab, index: number){
    if(tabToRemove.content.instance.IsActive){
      tabToRemove.content.instance.IsActive = false;
      this.tabs.splice(index,1);
      this.containerRef.detach();

      //make other tabs active if present
      if(this.tabs.length > 0){
        //if this was last, then its next as active, otherwise make its previous as active
        if(index == this.tabs.length){
          this.tabs[index - 1].content.instance.IsActive = true;
          this.containerRef.insert(this.tabs[index-1].view)
        }
        else{
          this.tabs[index].content.instance.IsActive = true;
          this.containerRef.insert(this.tabs[index].view);
        }
      }
    }
    else{
      this.tabs.splice(index,1);
    }
  }

  ngOnDestroy(): void {
    this.tabItemSubscription.unsubscribe();
  }

}
