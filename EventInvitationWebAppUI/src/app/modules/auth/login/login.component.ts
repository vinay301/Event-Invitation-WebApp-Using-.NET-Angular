import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { UserStoreService } from '../services/user-store.service';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from '../../../core/helpers/ValidateForm';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm !: FormGroup;
  isModalVisible : boolean = false;
  resetPasswordEmail !: string;
  isValidEmail !: boolean;
  constructor(private fb : FormBuilder, 
    private authService : AuthService, 
    private router : Router, 
    private toast: NgToastService, 
    private userStore : UserStoreService) { }

  ngOnInit() {
    this.loginForm = this.fb.group({
      username : ['',Validators.required],
      password : ['',Validators.required]
    })
  }
onLogin(){
  if(this.loginForm.valid)
  {
    console.log(this.loginForm.value);
    //send obj to Db
    this.authService.login(this.loginForm.value).subscribe({
      next : (res => {
        this.loginForm.reset();
        this.authService.storeToken(res.accessToken);
        this.authService.storeRefreshToken(res.refreshToken);
        const tokenPayload = this.authService.decryptToken();
        this.userStore.setUsernameForStore(tokenPayload.unique_name);
        this.userStore.setRoleForStore(tokenPayload.role);
        //console.log(res.message);
        this.toast.success({detail:"SUCCESS",summary:res.msessage,duration:5000});
        this.router.navigate(['dashboard']);
      }),
      error : (err => {
        // console.log(err);
        // console.log(err?.error?.message);
        // alert(err?.error.message)
        this.toast.error({detail:"ERROR",summary:err?.error?.message,sticky:true,duration:5000});
      }),
    })
  }else{
    console.log("Form is not valid");
    //Throw error using toastr and with required fields
    ValidateForm.validateAllFormFields(this.loginForm);
    //alert("Your Form is invalid");
    this.toast.error({detail:"ERROR",summary:"Your Form is Invalid",sticky:true});
  }
}

OpenForgetPasswordModal(){
  this.isModalVisible = true;
}

CloseImageSelector(){
  this.isModalVisible = false;
}

checkValidEmail(event : string){
  const value = event;
  const emailRegExp = /\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,3}\b/
  this.isValidEmail = emailRegExp.test(value);
  return this.isValidEmail;
}


 
}
