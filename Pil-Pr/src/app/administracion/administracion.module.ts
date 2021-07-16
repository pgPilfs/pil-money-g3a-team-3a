import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './login/login.component';
import { DniValidationComponent } from './dni-validation/dni-validation.component';
import { RegistroComponent } from './registro/registro.component';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [
    LoginComponent,
    RegistroComponent,
    DashboardComponent,
    DniValidationComponent
  ],
  imports: [
    CommonModule
  ]
})
export class AdministracionModule { }
