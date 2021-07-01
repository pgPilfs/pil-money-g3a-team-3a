import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './administracion/dashboard/dashboard.component';
import { LoginComponent } from './components/login/login.component';
import { RegistroComponent } from './components/registro/registro.component';

const routes: Routes = [
  {path:"login",component: LoginComponent, pathMatch:"full"},
  {path:"registro",component:RegistroComponent, pathMatch:"full"},
  {path:"home",component: DashboardComponent, pathMatch:"full"},
  {path: "**", redirectTo: "login", pathMatch: "full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
