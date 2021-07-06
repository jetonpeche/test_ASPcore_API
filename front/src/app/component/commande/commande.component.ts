import { Component, OnInit } from '@angular/core';
import {animate, state, style, transition, trigger} from '@angular/animations';
import { MatTableDataSource } from '@angular/material/table';
import { Commande } from 'src/app/type/Commande';
import { CommandeService } from 'src/app/services/commande.service';

@Component({
  selector: 'app-commande',
  templateUrl: './commande.component.html',
  styleUrls: ['./commande.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
],
})
export class CommandeComponent implements OnInit 
{
  listeCommande: MatTableDataSource<Commande> = new MatTableDataSource();
  columnsToDisplay = ['dateLivraisonCommande', 'adresseUtilisateur', 'supprimer'];
  expandedElement: Commande | null = null;

  constructor(private commandeService: CommandeService) { }

  ngOnInit(): void 
  {
    this.commandeService.ListeGeneral().subscribe(
      (_liste: Commande[]) =>
      {
        this.listeCommande.data = _liste;
      } 
    );
  }

  SupprimerCommande(_id: number): void
  {
    if(confirm("Comfirmation de la suppression de la commande ?"))
    {
      this.commandeService.Supprimer(_id).subscribe(
        (ok: boolean) =>
        {
          if(ok)
          {
            const INDEX = this.listeCommande.data.findIndex(commande => +commande.idCommande == _id);
            this.listeCommande.data.splice(INDEX, 1);

            this.listeCommande.data = this.listeCommande.data;
          }
          
        }
      );
    }
    
  }

}
