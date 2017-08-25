using Gym5DAL;
using System.Collections.Generic;

namespace Gym5BLL.Implementations
{
    public interface IProductoBLL
    {
        bool Agregar(Producto producto);
        bool Modificar(Producto producto);
        bool Eliminar(string id);
        List<Producto> BuscarTodos(string empresa);
        Producto BuscarPorId(string idpro);
    }
}