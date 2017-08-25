using Gym5DAL;
using Gym5DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Implementations
{
    public class LogUsuarioBLLImp : ILogUsuarioBLL
    {
        private UnitOfWork unitOfWork;

        public LogUsuarioBLLImp()
        {

        }

        public List<LogUsuario> BuscarTodos(string empresa)
        {
            try
            {
                List<LogUsuario> resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    using (GymContext Context = new GymContext())
                    {
                        resultado = Context.LogUsuarios.Select(n => n).Where(l => l.idNombreEmpresa == empresa).ToList();
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public LogUsuario BuscarPorId(string idInstructor)
        {
            try
            {
                LogUsuario resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    resultado = unitOfWork.logUsuarioDAL.Get(idInstructor);
                }

                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Agregar(LogUsuario log)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.logUsuarioDAL.Add(log);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Eliminar(string id)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    using (GymContext Context = new GymContext())
                    {
                        LogUsuario per = new LogUsuario();
                        List<LogUsuario> p = Context.LogUsuarios.Select(n => n).Where(l => l.usuario == id).ToList();
                        foreach (LogUsuario pe in p)
                        {
                            if (pe.permiso.Equals("Instructor"))
                            {
                                List<InstructorGym> ins = Context.InstructorGyms.Select(n => n).Where(l => l.usuario == pe.usuario).ToList();
                                foreach (InstructorGym inss in ins)
                                {
                                    Context.InstructorGyms.Remove(inss);
                                    Context.SaveChanges();
                                }
                                Context.LogUsuarios.Remove(pe);
                                Context.SaveChanges();
                            }else
                            {
                                Context.LogUsuarios.Remove(pe);
                                Context.SaveChanges();
                            }
                        }
                        return true;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Modificar(LogUsuario log)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.logUsuarioDAL.Update(log);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception e)
            {
                string a = e.Message;
                return false;
            }
        }

        public string[] getUser(string user, string pass)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    return unitOfWork.logUsuarioDAL.getUser(user, pass);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
