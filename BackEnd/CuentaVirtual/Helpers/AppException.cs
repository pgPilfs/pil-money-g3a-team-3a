using System;
using System.Globalization;

namespace CuentaVirtual.Helpers
{
    /* La excepción de la aplicación es una clase de excepción personalizada 
     * que se utiliza para diferenciar entre excepciones controladas y 
     * no controladas en la API de .NET. 
     * Las excepciones manejadas son generadas por el código de la aplicación y 
     * se utilizan para devolver mensajes de error amigables, por ejemplo, 
     * lógica de negocios o excepciones de validación causadas por parámetros 
     * de solicitud no válidos, mientras que las excepciones no manejadas son 
     * generadas por .NET Framework o causadas por errores en el código de la 
     * aplicación
     */
    public class AppException : Exception
    {
        //Clase de excepción personalizada para lanzar excepciones específicas
        //de la aplicación (por ejemplo, para la validación) que se pueden
        //capturar y manejar dentro de la aplicación
        public AppException(): base(){}
        public AppException(string mensaje): base(mensaje){}
        public AppException(string mensaje, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, mensaje, args))
        {
        }
    }
}
