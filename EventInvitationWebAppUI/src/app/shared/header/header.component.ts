import { Component, OnInit } from '@angular/core';
import { UserStoreService } from '../../modules/auth/services/user-store.service';
import { AuthService } from '../../modules/auth/services/auth.service';
import { NgToastService } from 'ng-angular-popup';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isLoggedIn = false;
  username: string = ""
  userId:string = ""
  constructor(private authService : AuthService,  private toast:NgToastService) { }

  ngOnInit() {

    this.isLoggedIn = this.authService.isLoggedIn();
    this.username = this.authService.getUsernameFromToken();
    this.userId = this.authService.getUsrIdFromToken();

    console.log("userId", this.userId);
    console.log("Username", this.username)
    console.log("Is logged in? ",this.isLoggedIn);
    
  }
  logout(){
    this.authService.signOut();
    this.toast.success({detail:"SUCCESS",summary:'Logout successfull!',duration:5000})
    window.location.reload();
  }
  
}
