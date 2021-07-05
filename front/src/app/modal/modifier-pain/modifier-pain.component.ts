import { Component, Inject, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { PainService } from 'src/app/services/pain.service';
import { Pain } from 'src/app/type/Pain';

@Component({
  selector: 'app-modifier-pain',
  templateUrl: './modifier-pain.component.html',
  styleUrls: ['./modifier-pain.component.css']
})
export class ModifierPainComponent implements OnInit 
{
  private painOrigine: Pain = {
    idPain: undefined,
    nomPain: ""
  }

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, private painService: PainService, private dialogRef: MatDialogRef<ModifierPainComponent>) { }

  ngOnInit(): void 
  {
    this.painOrigine.idPain = this.data._pain.idPain;
    this.painOrigine.nomPain = this.data._pain.nomPain;
  }

  Modifier(_form: NgForm): void
  {
    this.painService.Modifier(_form.value).subscribe(
      () =>
      {
        this.dialogRef.close();
      }
    );
  }

  AnnulerModif(): void
  {
    this.data._pain.idPain = this.painOrigine.idPain;
    this.data._pain.nomPain = this.painOrigine.nomPain;
  }
}
