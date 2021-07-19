using System;
using CuentaVirtual.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CuentaVirtual.Helpers
{
    /* El contexto de datos es usado para acceder a los datos de la aplicacion
     * a traves de Entity Framework Core y esta configurado para conectarse a 
     * una base de datos de SQL Server. Se deriva de la clase EF Core DbContext
     * y tiene una propiedad de Users publica para acceder y administrar los
     * datos del usuario.
     * El servicio de usuario utiliza el contexto de datos para manejar todas 
     * las operaciones de datos de bajo nivel.
     */
    public class DataContext: DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            // Conexion a la base de datos SQL Server
            options.UseSqlServer(Configuration.GetConnectionString("WebApiDatabase"));
        }

        public DbSet<User> Users { get; set; }
    }
}
