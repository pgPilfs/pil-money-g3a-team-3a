using System;

namespace CuentaVirtual.Authorization
{
    /* El atributo [AllowAnonymous] es usado para permitir el acceso anonimo
     * a methods especificos que estan decorados con el atributo [Authorize]
     * Es usado en el controlador Users para permitir el acceso anonimo a los
     * methods de register y login.
     * El atributo de autorización personalizado a continuación omite 
     * la autorización si el method de acción está decorado con [AllowAnonymous].
    */
    [AttributeUsage(AttributeTargets.Method)]
    public class AllowAnonymousAttribute: Attribute
    {
        public AllowAnonymousAttribute()
        {
        }
    }
}
