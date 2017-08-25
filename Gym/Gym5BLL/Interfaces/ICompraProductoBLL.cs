using Gym5DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Interfaces
{
    public interface ICompraProductoBLL
    {
        bool Agregar(CompraProducto compraProducto);
        bool Modificar(CompraProducto compraProducto);
        bool Eliminar(CompraProducto compraProducto);
        bool Crear(string compra, string[,] productos);
        List<CompraProducto> BuscarCierre(string id);
    }
}
