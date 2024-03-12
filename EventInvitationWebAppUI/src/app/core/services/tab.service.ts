import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TabService {
tabItemObservable : Subject<string> = new Subject<string>();
constructor() { }

}
