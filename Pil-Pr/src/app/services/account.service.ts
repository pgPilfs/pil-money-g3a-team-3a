import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';

import { User } from '../models/User';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  private userSubject: BehaviorSubject<User>;
  public user: Observable<User>;

  constructor(
    private router: Router,
    private http: HttpClient
  ) {
    /*
    Al iniciar sesion correctamente, el User devuelto se almacena en el almacenamiento local del navegador
    para permanecer conectado 
    */
    this.userSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('user')!));
      
    /*
    para que cualquier componente pueda suscribirse para recibir una notificacion cuando un usuario
     inicia sesion, cierra sesion o actualiza su perfil. 
    */
    this.user = this.userSubject.asObservable();
   }
  
  /*
  Permite que otros componentes obtengan facilmente el valor actual del usuario que
  ha iniciado sesion sin tener que suscribirse al observable.
  */
  public get userValue(): User {
    return this.userSubject.value;
  }

  /* FUNCIONES DE LA CUENTA DE USUARIO QUE PUEDE HACER AL BACKEND-API */
  login(username: string, password: string) {
    return this.http.post<User>(`${environment.apiUrl}/users/authenticate`, { username, password })
        .pipe(map(user => {
            // Almacena los datos del usuario y el token en el almacenamiento local para estar logeado mientras
            // la pagina se refresca.
            localStorage.setItem('user', JSON.stringify(user));
            this.userSubject.next(user);
            
            console.log("Usuario")
            console.log(user)
            
            return user;
        }));
  }

  logout() {
      // Remueve el usuario del almacenamiento local y establece el usuario actual como null
      localStorage.removeItem('user');
      this.userSubject.next(null!);
      this.router.navigate(['/login']);
  }

  register(user: User) {
      return this.http.post(`${environment.apiUrl}/users/register`, user);
  }

  getById(id: string) {
      return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
  }

  update(id : string, params : any) {
      return this.http.put(`${environment.apiUrl}/users/${id}`, params)
          .pipe(map(x => {
              // Actualiza el usuario almacenado si el usuario que inició sesión 
              // actualizó su propio registro
              if (id == this.userValue.id) {
                  // Actualiza el almacenamiento local
                  const user = { ...this.userValue, ...params };
                  localStorage.setItem('user', JSON.stringify(user));

                  // Publica usuario actualizado a los suscriptores
                  this.userSubject.next(user);
              }
              return x;
          }));
  }

}
