import { Component, OnInit } from '@angular/core';
import { Events } from '../../../../core/models/event.model';
import { ActivatedRoute } from '@angular/router';
import { EventsService } from '../../services/events.service';

@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.css']
})
export class ViewEventComponent implements OnInit {
  eventDetail!: Events 
  constructor(private activeRoute:ActivatedRoute, private eventService:EventsService) { }

  ngOnInit() {
    let eventId = this.activeRoute.snapshot.paramMap.get('id');
    console.warn(eventId);
    eventId && this.eventService.getEventById(eventId).subscribe((result) => {
      console.warn(result);
      this.eventDetail = result;
    })

  }

}
