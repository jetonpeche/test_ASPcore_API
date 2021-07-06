import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CommandeComponent } from './component/commande/commande.component';
import { PainComponent } from './component/pain/pain.component';

const routes: Routes = [
  { path: "", component: PainComponent },
  { path: "commande", component: CommandeComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
