import { Component, OnInit } from '@angular/core';
import { Events } from '../../../../core/models/event.model';
import { EventsService } from '../../services/events.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

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
