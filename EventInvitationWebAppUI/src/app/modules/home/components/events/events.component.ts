import { Component, OnInit } from '@angular/core';
import { Events } from '../../../../core/models/event.model';
import { EventsService } from '../../services/events.service';
import { TabService } from '../../../../core/services/tab.service';

@Component({
  selector: 'app-events',
  templateUrl: './events.component.html',
  styleUrls: ['./events.component.css']
})
export class EventsComponent implements OnInit {

  events: Events[] = [];
  constructor(private eventService:EventsService, private tabService:TabService) { }

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

  insertComponent(code:string)
  {
    this.tabService.tabItemObservable.next(code);
  }

}
