import { Component, Input, OnInit } from '@angular/core';
import { EventsService } from '../../../modules/home/services/events.service';
import { Events } from '../../../core/models/event.model';
import { ITabComponent } from '../../../core/models/tab';
import { AuthService } from '../../../modules/auth/services/auth.service';

@Component({
  selector: 'app-user-invites-list',
  templateUrl: './user-invites-list.component.html',
  styleUrls: ['./user-invites-list.component.css']
})
export class UserInvitesListComponent implements OnInit, ITabComponent {

  @Input() IsActive : boolean = true;
  events: Events[] = [];
  constructor(private eventService:EventsService, private authService:AuthService) { }

  ngOnInit() {
    let userId = this.authService.getUsrIdFromToken();
    this.eventService.getInvitedEventsOfUser(userId)
    .subscribe({
      next: (events) => {
       
        this.events = events
        console.log(events)
      },
      error: (res) => {
        console.log(res);
      }
    });
  }

}
