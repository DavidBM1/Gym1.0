using System;
using Gym5BLL.Implementations;
using System.Collections.Generic;
using System.Linq;

namespace Gym5DAL.Implementations
{
    public class CierreBLLImp : ICierreBLL
    {
        private UnitOfWork unitOfWork;

        public CierreBLLImp()
        {

        }

        public bool Agregar(Cierre cierre)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.cierreDAL.Add(cierre);
                    unitOfWork.Complete();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Eliminar(Cierre cierre)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.cierreDAL.Remove(cierre);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Modificar(Cierre cierre)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.cierreDAL.Update(cierre);
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Cierre> BuscarTodos(string empresa)
        {
            try
            {
                List<Cierre> resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    using (GymContext Context = new GymContext())
                    {
                        resultado = Context.Cierres.Select(n => n).Where(l => l.idNombreEmpresa == empresa).ToList();
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
