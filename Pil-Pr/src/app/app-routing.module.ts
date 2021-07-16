import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './administracion/login/login.component';
import { RegistroComponent } from './administracion/registro/registro.component';
import { DniValidationComponent } from './administracion/dni-validation/dni-validation.component';

const routes: Routes = [
  {path:"",component:LoginComponent, pathMatch:"full"},
  {path:"registro",component:RegistroComponent, pathMatch:"full"},
  {path:"dni-validation",component:DniValidationComponent, pathMatch:"full"},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
