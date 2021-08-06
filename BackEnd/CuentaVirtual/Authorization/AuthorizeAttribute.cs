using System;
using System.Linq;
using CuentaVirtual.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CuentaVirtual.Authorization
{
    /* El atributo [Authorize] es usado para restringir el acceso a 
     * controladores o acciones de methods especificos. 
     * Solo peticiones autorizadas pueden acceder a las acciones de los methods 
     * que tienen el atributo [Authorize].
     * 
     * Cuando un controlador tiene el atributo [Authorize] tods los methods de
     * accion estan restringidos a solicitudes autorizadas, excepto los methods
     * que tienen el atributo [AllowAnonymous].
     * 
     * La autorizacion se realiza mediante el method OnAuthorization que 
     * comprueba si hay un usuario autenticado adjunto a la solicitud 
     * (context.HttpContext.Items ["User"]). El middleware JWT personalizado 
     * adjunta un usuario autenticado si la solicitud contiene un token de 
     * acceso JWT valido
     * 
     * En la autorizacion exitosa no se toma ninguna accion y la solicitud se 
     * pasa al method de accion del controlador. 
     * Si la autorizacion falla, se devuelve una respuesta 401 no autorizada.
     */

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public AuthorizeAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Ignora la autorizacion si la accion tiene el atributo [AllowAnonymous]
            var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous) { return; }

            //Autorizacion
            var user = (User)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
