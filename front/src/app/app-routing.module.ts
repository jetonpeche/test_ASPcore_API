import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PainComponent } from './component/pain/pain.component';

const routes: Routes = [
  { path: "", component: PainComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
