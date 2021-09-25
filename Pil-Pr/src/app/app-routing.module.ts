import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BilleterapesosComponent } from './components/views/billeterapesos/billeterapesos.component';
import { DashboardComponent } from './components/views/dashboard/dashboard.component';
import { LoginComponent } from './components/views/login/login.component';
import { RegistroComponent } from './components/views/registro/registro.component';
import { UsenGuardGuard } from './Security/usen-guard.guard';

const routes: Routes = [
  {
    path:"login",component: LoginComponent, 
    pathMatch:"full"
  },
  {
    path:"registro",component: RegistroComponent, 
    pathMatch:"full"
  },
  {
    path:"dashboard",component: DashboardComponent, 
    pathMatch:"full",
    //canActivate: [UsenGuardGuard]
  },
  {
    path:"billeterapesos",component: BilleterapesosComponent, 
    pathMatch:"full",
    // canActivate: [UsenGuardGuard]
  },
  {
    path:"**",redirectTo:"/login", pathMatch:"full"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
