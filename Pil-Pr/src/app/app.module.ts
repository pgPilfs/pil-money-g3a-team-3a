import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuLateralComponent } from './plantilla-pricipal/menu-lateral/menu-lateral.component';
import { BarraNavegacionComponent } from './plantilla-pricipal/barra-navegacion/barra-navegacion.component';
import { PiePaginaComponent } from './plantilla-pricipal/pie-pagina/pie-pagina.component';
import { InicioSesionComponent } from './seguridad/inicio-sesion/inicio-sesion.component';
import { RegistroComponent } from './seguridad/registro/registro.component';
import { DashboardComponent } from './vistas/dashboard/dashboard.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuLateralComponent,
    BarraNavegacionComponent,
    PiePaginaComponent,
    InicioSesionComponent,
    RegistroComponent,
    DashboardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
