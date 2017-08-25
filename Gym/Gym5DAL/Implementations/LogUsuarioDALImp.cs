using System;
using Gym5DAL.Interfaces;
using System.Linq;
using System.Data;

namespace Gym5DAL.Implementations
{
    public class LogUsuarioDALImp : GenericRepository<LogUsuario>, ILogUsuarioDAL
    {
        public LogUsuarioDALImp(GymContext context) : base(context)
        {
        }

        public GymContext GymContext
        {
            get { return Context as GymContext; }
        }

        public string[] getUser(string userName, string password)
        {
            using (GymContext con = new GymContext())
            {
                try
                {
                    var usu = con.LogUsuarios.Where(x => x.usuario.Equals(userName) && x.pass.Equals(password)).ToList();
                    var count = usu.Count();
                    if (count == 1)
                    {
                        string[] respuesta = new string[2];
                        foreach (var us in usu)
                        {
                            respuesta[0] = us.usuario;
                            respuesta[1] = us.permiso;
                        }
                        return respuesta;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    string a = e.Message;
                    return null;
                }
            }
        }
    }
}
