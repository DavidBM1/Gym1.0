using Gym5DAL;
using Gym5DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Implementations
{
    public class InstructorBLLImp : IInstructorBLL
    {
        private UnitOfWork unitOfWork;

        public InstructorBLLImp()
        {

        }

        public InstructorGym BuscarPorId(string idInstructor)
        {
            try
            {
                InstructorGym resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    resultado = unitOfWork.instructorDAL.Get(idInstructor);
                }

                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public bool Agregar(InstructorGym instructor)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.instructorDAL.Add(instructor);
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
                        InstructorGym per = new InstructorGym();
                        List<InstructorGym> p = Context.InstructorGyms.Select(n => n).Where(l => l.idInstructor == id).ToList();
                        foreach (InstructorGym pe in p)
                        {
                            List<LogUsuario> pp = Context.LogUsuarios.Select(n => n).Where(l => l.usuario == pe.usuario).ToList();
                            Context.InstructorGyms.Remove(pe);
                            Context.SaveChanges();
                            foreach (LogUsuario pee in pp)
                            {
                                Context.LogUsuarios.Remove(pee);
                                Context.SaveChanges();
                            }
                        }
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                string a = e.Message;
                return false;
            }
        }

        public bool Modificar(InstructorGym instructor)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.instructorDAL.Update(instructor);
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

        public List<InstructorGym> BuscarTodos(string empresa)
        {
            try
            {
                List<InstructorGym> resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    using (GymContext Context = new GymContext())
                    {
                        resultado = Context.InstructorGyms.Select(n => n).Where(l => l.idNombreEmpresa == empresa).ToList();
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
