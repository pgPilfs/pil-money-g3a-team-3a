using System;
using System.Linq;
using System.Threading.Tasks;
using CuentaVirtual.Helpers;
using CuentaVirtual.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CuentaVirtual.Authorization
{
    /* El middleware JWT personalizado extrae el token JWT del encabezado
     * de autorizacion de la solicitud (si hay uno) y lo valida con el 
     * method jwtUtils.ValidateToken().
     * 
     * Si la validacion fue exitosa, se devuelve el ID del usuario del token
     * y el objeto de usuario autenticado se adjunta a la coleccion 
     * HttpContext.Items para que sea accesible dentro del alcance de la
     * solicitud actual.
     * 
     * Si la validacion del token falla, la solicitud solo puede acceder a 
     * rutas publicas (anonimas) porque no hay objeto de usuario autenticado
     * adjunto al contexto HTTP. 
     * 
     * La logica de autorizacion que verifica que el objeto de usuario este 
     * adjunto, se encuentra en el atributo de autorizacion personalizado, y 
     * si la autorizacion falla devuelve una respuesta 401 no autorizada.
     */
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            _next = next;
            _appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // Adjuntar usuario al contexto en la validación jwt exitosa
                context.Items["User"] = userService.GetById(userId.Value);
            }

            await _next(context);
        }
    }
}
