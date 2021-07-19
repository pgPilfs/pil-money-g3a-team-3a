using System;
namespace CuentaVirtual.Helpers
{
    /* La clase de configuracion de la API contiene propiedades definidas en el
     * archivo appsetting.json y se utiliza para acceder a la configuracion
     * de la aplicacion a traves de objetos que se inyectan en clases mediante
     * el sistema de inyeccion de dependencias (DI) .NET 5.0.
     * Por ejemplo: el controlador de usuarios accede a la configuracion de la
     * aplicacion a traves de un objeto IOptions<AppSettings> appSettings que
     * se inyecta en su constructor.
     * 
     * La asignacion de las secciones de configuracion a las clases se realiza
     * en el method ConfigureServices del archivo de inicio del proyecto.
     */
    public class AppSettings
    {
        public string Secret { get; set; }

        public AppSettings()
        {
        }
    }
}
