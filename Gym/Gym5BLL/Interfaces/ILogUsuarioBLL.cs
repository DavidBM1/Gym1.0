using Gym5DAL;
using System.Collections.Generic;

namespace Gym5BLL.Implementations
{
    public interface ILogUsuarioBLL
    {
        bool Agregar(LogUsuario log);
        bool Modificar(LogUsuario log);
        string[] getUser(string user, string pass);
        bool Eliminar(string id);
        List<LogUsuario> BuscarTodos(string empresa);
        LogUsuario BuscarPorId(string id);
    }
}