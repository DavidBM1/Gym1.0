using Gym5DAL;
using Gym5DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Implementations
{
    public class PersonaBLLImp : IPersonaBLL
    {
        private UnitOfWork unitOfWork;

        public PersonaBLLImp()
        {

        }

        public List<PersonaGym> BuscarTodos(string empresa)
        {
            try
            {
                List<PersonaGym> resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    using (GymContext Context = new GymContext())
                    {
                        resultado = Context.PersonaGyms.Select(n => n).Where(l => l.idNombreEmpresa == empresa).ToList();
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Agregar(PersonaGym persona)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.personaDAL.Add(persona);
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
                        List<PersonaMedidasGym> p = Context.PersonaMedidasGyms.Select(n => n).Where(l => l.idPersona == id).ToList();
                        foreach (PersonaMedidasGym pe in p)
                        {
                            Context.PersonaMedidasGyms.Remove(pe);
                            Context.SaveChanges();
                        }
                        PersonaGym per = new PersonaGym();
                        List<PersonaGym> pee = Context.PersonaGyms.Select(n => n).Where(l => l.idPersona == id).ToList();
                        foreach (PersonaGym pe in pee)
                        {
                            Context.PersonaGyms.Remove(pe);
                            Context.SaveChanges();
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

        public bool Modificar(PersonaGym persona)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.personaDAL.Update(persona);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public PersonaGym BuscarPorId(string idInstructor)
        {
            try
            {
                PersonaGym resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    resultado = unitOfWork.personaDAL.Get(idInstructor);
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
