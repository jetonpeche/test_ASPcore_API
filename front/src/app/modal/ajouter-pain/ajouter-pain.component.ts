import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { PainService } from 'src/app/services/pain.service';
import { Pain } from 'src/app/type/Pain';

@Component({
  selector: 'app-ajouter-pain',
  templateUrl: './ajouter-pain.component.html',
  styleUrls: ['./ajouter-pain.component.css']
})
export class AjouterPainComponent 
{
  estAjouter: boolean = false;

  listeInput: any[] = [];

  constructor(private painService: PainService, private dialogRef: MatDialogRef<AjouterPainComponent>) { }

  AjouterInput(_nb: number): void
  {
    this.listeInput.length = 0;

    for (let i = 1; i <= _nb; i++) 
    {     
      this.listeInput.push({ name: `nomPain${i}` });
    }
  }

  Ajouter(_form: NgForm): void
  {
    let listePainEnvoie: Pain[] = [];

    for (let i = 0; i < this.listeInput.length; i++) 
    {
      const element = _form.value[`nomPain${i + 1}`]; 
      
      listePainEnvoie.push({ nomPain: element });
    }

    this.painService.Ajouter(listePainEnvoie).subscribe(
      (_ok) =>
      {
        this.estAjouter = true;
        this.dialogRef.close();
      },
      () =>
      {

      }
    );
  }

}
