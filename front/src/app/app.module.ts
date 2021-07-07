import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// components
import { AppComponent } from './app.component';
import { PainComponent } from './component/pain/pain.component';
import { CommandeComponent } from './component/commande/commande.component';
import { UtilisateurComponent } from './component/utilisateur/utilisateur.component';

// modal
import { AjouterPainComponent } from './modal/ajouter-pain/ajouter-pain.component';
import { ModifierPainComponent } from './modal/modifier-pain/modifier-pain.component';
import { AjouterCommandeComponent } from './modal/ajouter-commande/ajouter-commande.component';
import { AjouterUtilisateurComponent } from './modal/ajouter-utilisateur/ajouter-utilisateur.component';
import { ConnexionUtilisateurComponent } from './modal/connexion-utilisateur/connexion-utilisateur.component';

// services
import { PainService } from './services/pain.service';
import { CommandeService } from './services/commande.service';

// mat
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';

@NgModule({
  declarations: [
    AppComponent,
    PainComponent,
    AjouterPainComponent,
    ModifierPainComponent,
    CommandeComponent,
    AjouterCommandeComponent,
    UtilisateurComponent,
    AjouterUtilisateurComponent,
    ConnexionUtilisateurComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCardModule,
    MatInputModule,
    MatFormFieldModule,
    MatTableModule,
    MatDialogModule,
    MatIconModule
  ],
  entryComponents: [AjouterPainComponent, ModifierPainComponent, AjouterCommandeComponent, AjouterUtilisateurComponent, ConnexionUtilisateurComponent],
  providers: [PainService, CommandeService],
  bootstrap: [AppComponent]
})
export class AppModule { }
