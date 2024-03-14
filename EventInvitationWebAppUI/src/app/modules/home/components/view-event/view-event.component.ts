import { Component, OnInit } from '@angular/core';
import { Events } from '../../../../core/models/event.model';
import { ActivatedRoute, Router } from '@angular/router';
import { EventsService } from '../../services/events.service';
import { AuthService } from '../../../auth/services/auth.service';
import { Invite } from '../../../../core/models/invite.model';
import { NgToastService } from 'ng-angular-popup';
import { Invitations } from '../../../../core/models/invitations';

@Component({
  selector: 'app-view-event',
  templateUrl: './view-event.component.html',
  styleUrls: ['./view-event.component.css']
})
export class ViewEventComponent implements OnInit {
  eventDetail: Events | undefined;
  showInviteButton:boolean = false;
  InviteResponseChoice:boolean = false
  inviteUserRequest : Invite = {
    eventId : "",
    invitedUsername: ""
  }

  inviteResponseForm : Invitations = {
    eventId:'',
    userId:'',
    respondingUserName:'',
    status:''
  }

  isAccepted : boolean = false;

  constructor(private activeRoute:ActivatedRoute,
              private eventService:EventsService, 
              private authService:AuthService, 
              private toast:NgToastService,
              private router:Router) { }

  ngOnInit() {
    let eventId = this.activeRoute.snapshot.paramMap.get('id');
    this.inviteUserRequest.eventId= eventId!;
    let userId = this.authService.getUsrIdFromToken();
    // console.log(userId)
    console.warn(eventId);
    eventId && this.eventService.getEventById(eventId).subscribe((result:Events) => {
      console.warn(result);
      if(userId == result.creator.id)
      {
        this.showInviteButton=true
      }
      else{
        this.showInviteButton = false;
      }
      // console.log("current user",this.currentUser)
      console.log("isInvited",result.invitation)
      //if the user is a currentUser and has a invite of a event, then enable the response choices for the user
       if(!this.showInviteButton && result.invitation)
       {
        this.InviteResponseChoice = true;
       } 
        this.inviteResponseForm = result.invitation[0] as Invitations;
      this.eventDetail = result;
    })
  }

  sendInvite(){
    console.log(this.inviteUserRequest.invitedUsername);
    console.log(this.inviteUserRequest.eventId);
   
    this.eventService.inviteUsersForEvent(this.inviteUserRequest)
    .subscribe({
      next: (invite) => {
        console.log(invite)
        this.toast.success({detail:"SUCCESS",summary:'Invite Sent Successfully!',duration:5000})
        this.router.navigate(['']);
      }, 
      error: (errorMessage)=>{
        console.log(errorMessage)
        this.toast.error({detail:"ERROR",summary:errorMessage.error,duration:5000})
      }
    })

    this.inviteUserRequest.invitedUsername = "";
  }

  inviteResponse(status:string){
    this.inviteResponseForm.status = status;
    console.log(status)
    console.log("response:",this.inviteResponseForm)
    if(this.inviteResponseForm.status == "accept"){
      this.isAccepted = true;
    }
    else{
      this.isAccepted = false;
    }
    console.log("Accept Response", this.isAccepted)
    console.log(this.inviteResponseForm)
    this.eventService.inviteResponse(this.inviteResponseForm)
    .subscribe({
      next: (inviteRes) => {
        console.log(inviteRes)
        this.toast.success({detail:"SUCCESS",summary:`Invite ${inviteRes.status}ed Successfully!`,duration:5000})
        this.router.navigate(['']);
        
      }, 
      error: (errorMessage)=>{
        console.log(errorMessage)
        this.toast.error({detail:"ERROR",summary:errorMessage.error,duration:5000})
      }
    })
  }

}
