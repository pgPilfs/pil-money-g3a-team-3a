import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Transacciones } from '../models/Transacciones';

@Injectable({
  providedIn: 'root'
})
export class TransaccionService {

  private URLapi:string="https://localhost:44382/api/";
  private router:string = "";
  constructor(private http:HttpClient) { }

  IngresarDinero(trans:Transacciones):Observable<any>{
    this.router = "IngresoDinero";
    return this.http.post(this.URLapi + this.router, trans);
  }

  ListadoDeTransacciones(id:any): Observable<any>{
    this.router = "ListadoTransacciones";
    return this.http.post(this.URLapi + this.router, id);
  }
}
