import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DashboardComponent } from './components/views/dashboard/dashboard.component';
import { LoginComponent } from './components/views/login/login.component';
import { RegistroComponent } from './components/views/registro/registro.component';
import { VerificacionComponent } from './components/views/registro/verificacion/verificacion.component';
import { AuthGuard } from './helpers/auth.guard';

const routes: Routes = [
  {path:"login",component: LoginComponent, pathMatch:"full"},
  {path:"registro",component: RegistroComponent, pathMatch:"full"},
  {path:"verificacion",component: VerificacionComponent, pathMatch:"full"},
  {path:"dashboard",component: DashboardComponent, canActivate: [AuthGuard] },
  {path:"**",redirectTo:"/login", pathMatch:"full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
