import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Commande } from '../type/Commande';

@Injectable({
  providedIn: 'root'
})
export class CommandeService {

  constructor(private http: HttpClient) { }

  ListeGeneral(): Observable<Commande[]>
  {
    return this.http.get<Commande[]>(environment.url + "commande/listeGeneral");
  }

  Supprimer(_id: number): Observable<boolean>
  {
    return this.http.delete<boolean>(environment.url + "commande/supprimer/" + _id);
  }
}
