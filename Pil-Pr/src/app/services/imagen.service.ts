import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Archivo } from '../models/Archivo';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ImagenService {

  url: string;

  constructor(private http: HttpClient) { 
    this.url = 'URL BASE DE DATOS'
  }

  //Servicio con metodos para enviar la imagen al backend
  uploadFiles(file: Archivo): Observable<any>{
    var peticion = "api/subir";
    var file_json = JSON.stringify(file); //Pasamos a formato JSON el archivo
    return this.http.post(this.url + peticion, file_json)
  }
}
