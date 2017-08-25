using Gym5DAL;

namespace Gym5BLL.Implementations
{
    public interface ILogEmpresaBLL
    {
        bool Agregar(LogEmpresa log);
        bool Modificar(LogEmpresa log);
        bool Eliminar(LogEmpresa log);
        bool getUser(string user, string pass);
    }
}
