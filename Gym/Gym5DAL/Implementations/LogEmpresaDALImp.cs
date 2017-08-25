using Gym5DAL.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Data.Entity;

namespace Gym5DAL.Implementations
{
    public class LogEmpresaDALImp : GenericRepository<LogEmpresa>, ILogEmpresaDAL
    {
        public LogEmpresaDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }

        bool ILogEmpresaDAL.getUser(string userName, string password)
        {
            using (GymContext con = new GymContext())
            {
                try
                {
                    var count = con.LogEmpresas.Where(x => x.idNombreEmpresa.Equals(userName) && x.pass.Equals(password)).Count();
                    if (count == 1) return true;
                    else return false;
                }
                catch (Exception)
                {
                    return true;
                }
            }
        }
    }
}
