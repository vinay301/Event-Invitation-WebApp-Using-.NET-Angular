import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment.development';
import { Observable } from 'rxjs';
import { Events } from '../../../core/models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventsService {

  baseApiUrl:string = environment.baseApiUrl
constructor(private http:HttpClient) { }

  getAllEvents(): Observable<Events[]>{
    return this.http.get<Events[]>(this.baseApiUrl + '/api/events/GetAllEvents');
  }
}
