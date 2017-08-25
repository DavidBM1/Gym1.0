using Gym5DAL;
using System.Collections.Generic;

namespace Gym5BLL.Implementations
{
    public interface ICierreProductoBLL
    {
        bool Agregar(CierreProducto cierreProducto);
        bool Modificar(CierreProducto cierreProducto);
        bool Eliminar(CierreProducto cierreProducto);
        bool Crear(string empresa, string cierre);
        List<CierreProducto> BuscarCierre(string id);
    }
}
