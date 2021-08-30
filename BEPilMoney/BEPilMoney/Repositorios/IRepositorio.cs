using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEPilMoney.Repositorios
{
    public interface IRepositorio
    {
        DataTable Listado();
        DataTable Detalle(int id);
        int Agregar(object obj);
        int Modificar(object obj);
        int Eliminar(int id);
    }
}
