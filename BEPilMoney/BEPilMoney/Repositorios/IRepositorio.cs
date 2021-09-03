using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEPilMoney.Repositorios
{
    public interface IRepositorio<T> where T : class
    {
        DataTable Listado();
        DataTable Detalle(int id);
        int Agregar(T obj);
        int Modificar(T obj);
        int Eliminar(int id);
    }
}
