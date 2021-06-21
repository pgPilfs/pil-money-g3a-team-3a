import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { RegistroComponent } from './registro/registro.component';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    LoginComponent,
    RegistroComponent,
    DashboardComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AdministracionModule { }
