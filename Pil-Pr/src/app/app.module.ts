import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule} from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegistroComponent } from './components/views/registro/registro.component';
import { LoginComponent } from './components/views/login/login.component';
import { DashboardComponent } from './components/views/dashboard/dashboard.component';
import { MenuComponent } from './components/Template/menu/menu.component';
import { MenuLateralComponent } from './components/Template/menu-lateral/menu-lateral.component';
import { PiePaginaComponent } from './components/Template/pie-pagina/pie-pagina.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { JwtInterceptor } from './helpers/jwt.interceptor';
import { ErrorInterceptor } from './helpers/error.interceptor';
import { VerificacionComponent } from './components/views/registro/verificacion/verificacion.component';


@NgModule({
  declarations: [
    AppComponent,
    RegistroComponent,
    LoginComponent,
    DashboardComponent,
    MenuComponent,
    MenuLateralComponent,
    PiePaginaComponent,
    VerificacionComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
