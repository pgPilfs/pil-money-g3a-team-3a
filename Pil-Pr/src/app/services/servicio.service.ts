import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ServicioService {

  private URLapi:string="https://localhost:44382/api/servicio/listado";

  constructor(private http:HttpClient) { }

  listadoTipoServicio(): Observable<any>{
    return this.http.get(this.URLapi);
  }
}
