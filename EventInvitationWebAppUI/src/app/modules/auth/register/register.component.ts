import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { NgToastService } from 'ng-angular-popup';
import ValidateForm from '../../../core/helpers/ValidateForm';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registerForm !: FormGroup;
  constructor(private fb : FormBuilder, private authService : AuthService, private router : Router,  private toast: NgToastService) { }

  ngOnInit() {
    this.createRegistrationForm();
  }

  createRegistrationForm () {
    this.registerForm = this.fb.group({
      username : [null, Validators.required],
      email : [null,[Validators.required,Validators.email]],
      password : [null,[Validators.required,Validators.minLength(8)]],
      
      
    });
  }





  onRegister(){
   
    if(this.registerForm.valid){
      //perform registration logic
      this.authService.register(this.registerForm.value).subscribe({
        next : (res => {
          //alert(res.message)
          this.registerForm.reset();
          this.toast.success({detail:"SUCCESS",summary:res.message,duration:5000});
          this.router.navigate(['']);
        }),
        error : (err => {
          //alert(err?.error.message)
          this.toast.error({detail:"ERROR",summary:err?.error.message,duration:5000,sticky:true});
        })
      })
     
    }else{
      ValidateForm.validateAllFormFields(this.registerForm);
      //logic for throwing error
      //alert('Invalid form')
      this.toast.error({detail:"ERROR",summary:'Invalid Form',duration:5000,sticky:true});
    }
  }

 

}

