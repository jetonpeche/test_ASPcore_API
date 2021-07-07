import { Component, OnInit } from '@angular/core';
import { PainService } from 'src/app/services/pain.service';
import { Pain } from 'src/app/type/Pain';

@Component({
  selector: 'app-ajouter-commande',
  templateUrl: './ajouter-commande.component.html',
  styleUrls: ['./ajouter-commande.component.css']
})
export class AjouterCommandeComponent implements OnInit {

  estAjouter: boolean = false;

  listePain: Pain[] = [];

  constructor(private painService: PainService) { }

  ngOnInit(): void 
  {
    this.painService.Lister().subscribe(
      (liste: Pain[]) =>
      {
        this.listePain = liste;
      }
    );
  }

}
