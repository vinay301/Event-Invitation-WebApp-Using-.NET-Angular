import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Events } from '../../../core/models/event.model';
import { AuthService } from '../../auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  baseApiUrl:string = environment.baseApiUrl
constructor(private http:HttpClient, private authService:AuthService) { }

  getAllEvents(): Observable<Events[]>{
    return this.http.get<Events[]>(this.baseApiUrl + '/api/events/GetAllEvents');
  }

  addEvent(eventObj:Events): Observable<Events> {
    let headers = new HttpHeaders().set('Authorization','Bearer '+localStorage.getItem('event_token'));
    eventObj.creatorId = this.authService.getUsrIdFromToken();
    console.log(eventObj.creatorId);
    return this.http.post<Events>(this.baseApiUrl + '/api/events/CreateEvent', eventObj,{headers
    });
  }
}
