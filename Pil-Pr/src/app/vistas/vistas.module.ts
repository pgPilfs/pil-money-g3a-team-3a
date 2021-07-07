import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BilleteraComponent } from './billetera/billetera.component';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    DashboardComponent,
    BilleteraComponent
  ],
  imports: [
    CommonModule
  ]
})
export class VistasModule { }
