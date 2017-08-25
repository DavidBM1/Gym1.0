using Gym5DAL;
using System.Collections.Generic;

namespace Gym5BLL.Implementations
{
    public interface ICierreBLL
    {
        bool Agregar(Cierre cierre);
        bool Modificar(Cierre cierre);
        bool Eliminar(Cierre cierre);
        List<Cierre> BuscarTodos(string empresa);
    }
}
