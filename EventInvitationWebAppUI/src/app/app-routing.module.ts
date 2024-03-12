import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './modules/home/components/home/home.component';
import { LoginComponent } from './modules/auth/login/login.component';
import { RegisterComponent } from './modules/auth/register/register.component';
import { AddEventComponent } from './modules/home/components/add-event/add-event.component';
import { ViewEventComponent } from './modules/home/components/view-event/view-event.component';

const routes: Routes = [
  {path:'',redirectTo: 'home', pathMatch:"full"},
  {path:'home',component:HomeComponent},
  {path:'login',component:LoginComponent},
  {path:'register',component:RegisterComponent},
  {path:'add-event',component:AddEventComponent},
  {path:'view-event/:id', component:ViewEventComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
