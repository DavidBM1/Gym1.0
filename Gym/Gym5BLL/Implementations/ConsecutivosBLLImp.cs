using Gym5DAL;
using Gym5DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Implementations
{
    public class ConsecutivosBLLImp : IConsecutivosBLL
    {
        private UnitOfWork unitOfWork;

        public ConsecutivosBLLImp()
        {

        }

        public bool Agregar(Consecutivo consecutivo)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.consecutivosDAL.Add(consecutivo);
                    unitOfWork.Complete();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Eliminar(Consecutivo consecutivo)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.consecutivosDAL.Remove(consecutivo);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Modificar(Consecutivo consecutivo)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.consecutivosDAL.Update(consecutivo);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public Consecutivo BuscarPorId(string idConsecutivo)
        {
            try
            {
                Consecutivo resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    resultado = unitOfWork.consecutivosDAL.Get(idConsecutivo);
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
