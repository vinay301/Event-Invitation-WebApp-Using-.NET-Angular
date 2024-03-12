import { Component, Input, OnInit } from '@angular/core';
import { Events } from '../../../core/models/event.model';
import { EventsService } from '../../../modules/home/services/events.service';
import { ITabComponent } from '../../../core/models/tab';

@Component({
  selector: 'app-event-list',
  templateUrl: './event-list.component.html',
  styleUrls: ['./event-list.component.css']
})
export class EventListComponent implements OnInit, ITabComponent {
  @Input() IsActive : boolean = true;
  events: Events[] = [];
  constructor(private eventService:EventsService) { }

  ngOnInit() {
    this.eventService.getAllEvents()
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
