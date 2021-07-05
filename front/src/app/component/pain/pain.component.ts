import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { AjouterPainComponent } from 'src/app/modal/ajouter-pain/ajouter-pain.component';
import { ModifierPainComponent } from 'src/app/modal/modifier-pain/modifier-pain.component';
import { PainService } from 'src/app/services/pain.service';
import { Pain } from 'src/app/type/Pain';

@Component({
  selector: 'app-pain',
  templateUrl: './pain.component.html',
  styleUrls: ['./pain.component.css']
})
export class PainComponent implements OnInit 
{

  listePain: MatTableDataSource<Pain> = new MatTableDataSource();
  displayedColumns: string[] = ['nomPain', 'supp', 'modif'];

  applyFilter(event: Event): void 
  {
    const filterValue = (event.target as HTMLInputElement).value;
    this.listePain.filter = filterValue.trim().toLowerCase();
  }

  constructor(private painService: PainService, private dialog: MatDialog) { }

  ngOnInit(): void 
  {
    this.ListerPain();
  }

  modalAjoutPain(): void
  {
    const DIALOG_REF = this.dialog.open(AjouterPainComponent);
    DIALOG_REF.beforeClosed().subscribe(
      () =>
      {
        if(DIALOG_REF.componentInstance.estAjouter)
          this.ListerPain();
      }
    )
  }

  ModalModifPain(_pain: Pain): void
  {
    const DIALOG_REF = this.dialog.open(ModifierPainComponent, { disableClose: true, data: { _pain }});
  }

  SupprimerPain(_id: number, _nom: string): void
  {
    if(confirm("Confirmation, suppression du pain: " + _nom))
    {
      this.painService.Supprimer(_id).subscribe(
        () =>
        {
          const INDEX = this.listePain.data.findIndex(pain => pain.idPain != undefined && +pain.idPain == _id);
          
          this.listePain.data.splice(INDEX, 1);
          this.listePain.data = this.listePain.data;
        });
    }

  }

  private ListerPain(): void
  {
    this.painService.Lister().subscribe(
      (_pain) =>
      {
        this.listePain.data = _pain;
      },
      () =>
      {
        console.log("erreur réseau");
      }
    );
  }
}
