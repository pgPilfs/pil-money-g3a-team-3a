/*
El Interceptor de errores intercepta las respuestas http de la API para verificar si hubo algún error. 
Si hay una respuesta 401 Unauthorized o 403 Forbidden(prohibida), el usuario se desconecta automáticamente de la aplicación
todos los demás errores se devuelven al servicio de llamada para que se muestre una alerta con el error en la pantalla.

Se implementa utilizando la interfaz Angular HttpInterceptor incluida en HttpClientModule, 
al implementar la interfaz HttpInterceptor puede crear un interceptor personalizado para capturar todas 
las respuestas de error del servidor en una sola ubicación.

Los interceptores HTTP se agregan al pipeline de solicitudes en la sección de proveedores del archivo app.module.ts.
*/

import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";
import { AccountService } from "../services/account.service";

@Injectable()
export class ErrorInterceptor implements HttpInterceptor{
    
    constructor(private accountService: AccountService){}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            if ([401, 403].includes(err.status) && this.accountService.userValue) {
                // Se desconecta al usuario si la respuesta de la API fue 401 o 403
                this.accountService.logout();
            }

            const error = err.error?.message || err.statusText;
            console.error(err);
            return throwError(error);
        }))
    }
}