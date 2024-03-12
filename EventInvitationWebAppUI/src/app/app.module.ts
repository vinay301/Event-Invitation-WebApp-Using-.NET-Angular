import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { MatIconModule } from '@angular/material/icon';
import { AppComponent } from './app.component';
import { HomeComponent } from './modules/home/components/home/home.component';
import { HeaderComponent } from './shared/header/header.component';
import { EventsComponent } from './modules/home/components/events/events.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './modules/auth/login/login.component';
import { RegisterComponent } from './modules/auth/register/register.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgToastModule } from 'ng-angular-popup';
import { AddEventComponent } from './modules/home/components/add-event/add-event.component';
import { TabControlComponent } from './shared/components/TabControl/TabControl.component';
import { TabItemHeaderComponent } from './shared/components/tab-item-header/tab-item-header.component';
import { EventListComponent } from './shared/components/event-list/event-list.component';
import { UserEventListComponent } from './shared/components/user-event-list/user-event-list.component';
import { UserInvitesListComponent } from './shared/components/user-invites-list/user-invites-list.component';
import { ViewEventComponent } from './modules/home/components/view-event/view-event.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    EventsComponent,
    LoginComponent,
    RegisterComponent,
    AddEventComponent,
    TabControlComponent,
    TabItemHeaderComponent,
    EventListComponent,
    UserEventListComponent,
    UserInvitesListComponent,
    ViewEventComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatIconModule,
    ReactiveFormsModule,
    FormsModule,
    NgToastModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
