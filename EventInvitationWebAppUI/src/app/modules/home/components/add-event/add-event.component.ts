import { Component, OnInit } from '@angular/core';
import { Events } from '../../../../core/models/event.model';
import { EventsService } from '../../services/events.service';
import { NgToastService } from 'ng-angular-popup';
import { User } from '../../../../core/models/user.model';
import { AuthService } from '../../../auth/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-event',
  templateUrl: './add-event.component.html',
  styleUrls: ['./add-event.component.css']
})
export class AddEventComponent implements OnInit {

  addEventRequest: Events = {
    id: '',
    name: '',
    startDate: new Date(),
    endDate: new Date(),
    creatorId:'',
    creator: {
      id: '',
      email: '',
      userName: '',
      password: ''
    }
  }
  constructor(private eventsService:EventsService, private toast:NgToastService, private authService:AuthService, private router :Router) { }
  userId:string = ''
  ngOnInit() {
  }

  addEvent(){
    this.eventsService.addEvent(this.addEventRequest)
    .subscribe({
      next: (events) => {
        console.log(events)
        this.toast.success({detail:"SUCCESS",summary:'Event Listed Successfully!',duration:5000})
        this.router.navigate(['']);
      }, 
      error: (errorMessage)=>{
        console.log(errorMessage)
        this.toast.error({detail:"ERROR",summary:errorMessage.error,duration:5000})
      }
    })
    
  }

}
