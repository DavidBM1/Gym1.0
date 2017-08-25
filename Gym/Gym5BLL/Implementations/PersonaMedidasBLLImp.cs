using Gym5DAL;
using Gym5DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Implementations
{
    public class PersonaMedidasBLLImp : IPersonaMedidasBLL
    {
        private UnitOfWork unitOfWork;

        public PersonaMedidasBLLImp()
        {

        }

        public string getMeses(string id)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    string html = string.Empty;
                    using (GymContext Context = new GymContext())
                    {
                        List<DateTime> p = Context.PersonaMedidasGyms.Where(v => v.idPersona == id).Select(v => v.fecha).ToList();
                        foreach(DateTime d in p)
                        {
                            html += "'" + d.ToString("MM/dd/yyyy") + "', ";
                        }
                        return html.Remove(html.Length - 2);
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public PersonaMedidasGym BuscarPorId(string id)
        {
            try
            {
                PersonaMedidasGym resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    resultado = unitOfWork.personaMedidasDAL.Get(id);
                }

                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public List<PersonaMedidasGym> BuscarTodos()
        {
            try
            {
                List<PersonaMedidasGym> resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    resultado = unitOfWork.personaMedidasDAL.GetAll().ToList();
                }
                return resultado;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Agregar(PersonaMedidasGym personaMedidas)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.personaMedidasDAL.Add(personaMedidas);
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
                        List<PersonaMedidasGym> p = Context.PersonaMedidasGyms.Select(n => n).Where(l => l.idMedida == id).ToList();
                        foreach (PersonaMedidasGym pe in p)
                        {
                            Context.PersonaMedidasGyms.Remove(pe);
                            Context.SaveChanges();
                        }
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Modificar(PersonaMedidasGym personaMedidas)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.personaMedidasDAL.Update(personaMedidas);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        List<PersonaMedidasGym> IPersonaMedidasBLL.BuscarPorPersona(string id)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    using (GymContext Context = new GymContext())
                    {
                        List<PersonaMedidasGym> p = Context.PersonaMedidasGyms.Select(n => n).Where(l => l.idPersona == id).ToList();
                        return p;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
