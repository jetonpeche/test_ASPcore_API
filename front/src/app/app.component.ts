import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ConnexionUtilisateurComponent } from './modal/connexion-utilisateur/connexion-utilisateur.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent
{
  constructor(private dialog: MatDialog) { }

  ModalConnexion(): void
  {
    const DIALOG_REF = this.dialog.open(ConnexionUtilisateurComponent);

  }
}
