import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServicioService {

  private URLapi:string="https://localhost:44382/api/";
  private router:string = "";
  constructor(private http:HttpClient) { }

  listadoTipoServicio(): Observable<any>{
    this.router = "ListadoDeServicios";
    return this.http.get(this.URLapi + this.router);
  }

  datosCuentaEnPesos(id:any): Observable<any>{
    this.router = "CuentaPesos";
    return this.http.post(this.URLapi + this.router, id);
  }

  ultimosMovimientos(id:any): Observable<any>{
    this.router = "UltimosMovimientos";
    return this.http.post(this.URLapi + this.router, id);
  }
}
