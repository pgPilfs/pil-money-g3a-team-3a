import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from '../models/Login';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  private myApiUrl:string ="https://localhost:44382/api/Login";

  constructor(private http:HttpClient) { }

  login(inicioSesion:Login):Observable<any> {
    console.log(inicioSesion);
    return this.http.post(this.myApiUrl, inicioSesion);
  }
}
