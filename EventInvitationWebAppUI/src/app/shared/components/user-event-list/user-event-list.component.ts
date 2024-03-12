import { Component, Input, OnInit } from '@angular/core';
import { EventsService } from '../../../modules/home/services/events.service';
import { Events } from '../../../core/models/event.model';
import { ITabComponent } from '../../../core/models/tab';
import { AuthService } from '../../../modules/auth/services/auth.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-user-event-list',
  templateUrl: './user-event-list.component.html',
  styleUrls: ['./user-event-list.component.css']
})
export class UserEventListComponent implements OnInit, ITabComponent {

  @Input() IsActive : boolean = true;
  events: Events[] = [];
  constructor(private eventService:EventsService, private authService:AuthService, private toast:NgToastService) { }

  ngOnInit() {
    let userId = this.authService.getUsrIdFromToken();
    this.eventService.getCreatedEventsByUserId(userId)
    .subscribe({
      next: (events) => {
        this.events = events
        console.log(events)
      },
      error: (res) => {
        console.log(res);
        this.toast.error({detail:"ERROR",summary:res?.error,sticky:true})
      }
    });
  }

 

}
