/*
El interceptor JWT intercepta las solicitudes http de la aplicación para agregar un token de autenticación JWT
al encabezado de autorización si el usuario ha iniciado sesión y la solicitud se dirige a la URL de la api
de la aplicación (environment.apiUrl).
*/

import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { AccountService } from "../services/account.service";

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    
    constructor(private accountService: AccountService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // agrega el token al encabezado de autenticación con jwt si el usuario está conectado y
        // la solicitud se envia a la URL de la API

        const user = this.accountService.userValue;
        const isLoggedIn = user && user.jwtToken;
        const isApiUrl = request.url.startsWith(environment.apiUrl);
        if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${user.jwtToken}`
                }
            });
        }

        return next.handle(request);
    }
}