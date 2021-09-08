import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Usuario } from '../models/Usuario';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {

  private myApiUrl:string ="https://localhost:44382/api/Usuario/";

  constructor(private http:HttpClient) { }

  agregarUsuario(usuario:Usuario):Observable<any>{
    return this.http.post(this.myApiUrl + "Registrar", usuario);
  }
}
