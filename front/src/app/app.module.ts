import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// components
import { AppComponent } from './app.component';
import { PainComponent } from './component/pain/pain.component';

// modal
import { AjouterPainComponent } from './modal/ajouter-pain/ajouter-pain.component';
import { ModifierPainComponent } from './modal/modifier-pain/modifier-pain.component';

// services
import { PainService } from './services/pain.service';

// mat
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatTableModule } from '@angular/material/table';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { CommandeComponent } from './component/commande/commande.component';



@NgModule({
  declarations: [
    AppComponent,
    PainComponent,
    AjouterPainComponent,
    ModifierPainComponent,
    CommandeComponent
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
  entryComponents: [AjouterPainComponent, ModifierPainComponent],
  providers: [PainService],
  bootstrap: [AppComponent]
})
export class AppModule { }
