using Gym5DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Interfaces
{
    public interface ICompraBLL
    {
        bool Agregar(Compra compra);
        bool Modificar(Compra compra);
        bool Eliminar(Compra compra);
        List<Compra> BuscarTodos(string empresa);
    }
}
