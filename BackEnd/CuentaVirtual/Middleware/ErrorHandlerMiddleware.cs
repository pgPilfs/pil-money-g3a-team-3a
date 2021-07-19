using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CuentaVirtual.Helpers;
using Microsoft.AspNetCore.Http;

namespace CuentaVirtual.Middleware
{
    /* El controlador de errores global se utiliza para detectar tods los 
     * errores y eliminar la necesidad de un código de manejo de errores 
     * duplicado en toda la API de .NET. 
     * Está configurado como middleware en el method Configure del archivo de 
     * inicio del proyecto.
     * 
     * Los errores de tipo AppException se tratan como errores personalizados 
     * (específicos de la aplicación) que devuelven una respuesta 400 Bad Request,
     * la clase KeyNotFoundException incorporada de .NET se usa para devolver 
     * respuestas 404 Not Found, todas las demás excepciones no se controlan 
     * y devuelven una respuesta 500 Internal Server Error.
     */
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
