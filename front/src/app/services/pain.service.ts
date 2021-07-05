import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Pain } from '../type/Pain';

@Injectable({
  providedIn: 'root'
})
export class PainService 
{
  constructor(private http: HttpClient) { }

  Lister(): Observable<Pain[]>
  {
    return this.http.get<Pain[]>(environment.url + "Pain/lister");
  }

  Ajouter(_info: Pain[]): Observable<Pain[]>
  {
    return this.http.post<Pain[]>(environment.url + "pain/ajouter", _info);
  }

  Supprimer(_id: number): Observable<any>
  {
    return this.http.delete(environment.url + "pain/supprimer/" + _id);
  }

  Modifier(_info: Pain): Observable<any>
  {
    return this.http.put(environment.url + "pain/modifier", _info);
  }
}
