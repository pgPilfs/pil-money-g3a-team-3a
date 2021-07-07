import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BilleteraComponent } from './vistas/billetera/billetera.component';
import { DashboardComponent } from './vistas/dashboard/dashboard.component';


const routes: Routes = [
  {path:"",component:DashboardComponent, pathMatch:"full"},
  {path:"billetera",component:BilleteraComponent, pathMatch:"full"},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
