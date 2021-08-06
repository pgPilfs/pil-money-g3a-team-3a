import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from "@angular/router";

import { AccountService } from "../services/account.service";

/*
El auth guard es un protector de ruta angular que se utiliza para evitar que los usuarios no autenticados 
accedan a rutas restringidas, lo hace implementando la interfaz CanActivate que permite al guardia decidir 
si una ruta se puede activar con el método canActivate (). 
    - Si el método devuelve true, la ruta se activa (se permite continuar); 
    - Si el método devuelve false, la ruta se bloquea.

El auth guard usa el servicio de "account" para verificar si el usuario está logeado: 
    - Si está logeado, devuelve true desde el método canActivate (); de lo contrario, 
    - Si no lo esta, devuelve false y redirige al usuario a la página de inicio de sesión 
        junto con returnUrl en los parametros de la consulta.
*/

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate{
    constructor(
        private router: Router,
        private accountService: AccountService
    ) {}

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot ) {
        const user = this.accountService.userValue;
        if (user) {
            // Usuario logeado, autoriza acceso a ruta retornando true
            return true;
        }

        // Usuario no logeado, no autoriza y redirige a la ruta de logeo con la URL
        this.router.navigate(['/login'], { queryParams: { returnUrl: state.url }});
        return false;
    }
}