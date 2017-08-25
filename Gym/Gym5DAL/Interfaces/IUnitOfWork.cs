using System;

namespace Gym5DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICierreDAL cierre { get; }
        ICierreProductoDAL cierreProducto { get; }
        IConsecutivosDAL consecutivos { get; }
        IInstructorDAL instructor { get; }
        ILogEmpresaDAL logEmpresa { get; }
        ILogUsuarioDAL logUsuario { get; }
        IPersonaDAL persona { get; }
        IPersonaMedidasDAL personaMedidas { get; }
        IProductoDAL producto { get; }
        ICompraDAL compra { get; }
        ICompraProductoDAL compraProducto { get; }
    }
}
