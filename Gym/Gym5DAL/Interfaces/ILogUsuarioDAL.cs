using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5DAL.Interfaces
{
    public interface ILogUsuarioDAL : IGenericRepository<LogUsuario>
    {
        string[] getUser(string userName, string password);
    }
}
