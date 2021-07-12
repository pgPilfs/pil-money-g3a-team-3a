import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Archivo } from '../models/Archivo';
import {HttpClient} from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ImagenService {
  constructor(private http: HttpClient) { }

  //Servicio con metodos para enviar la imagen al backend
  uploadFiles(file: Archivo): Observable<any>{
    var peticion = "api/subir";
    var file_json = JSON.stringify(file); //Pasamos a formato JSON el archivo
    return this.http.post(`${environment.apiUrl} + peticion`, file_json)
  }
}
