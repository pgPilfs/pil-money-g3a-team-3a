import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    LoginComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule,
  ]
})
export class AdministracionModule { }
