using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
namespace Gym5DAL.Interfaces
{
    public interface ILogEmpresaDAL : IGenericRepository<LogEmpresa>
    {
        bool getUser(string userName, string password);
    }
}
