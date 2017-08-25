using Gym5DAL;
using Gym5DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Implementations
{
    public class LogEmpresaBLLImp : ILogEmpresaBLL
    {
        private UnitOfWork unitOfWork;

        public LogEmpresaBLLImp()
        {

        }

        public bool Agregar(LogEmpresa log)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.logEmpresaDAL.Add(log);
                    unitOfWork.Complete();
                    unitOfWork.Dispose();
                }
                return true;
            }
            catch (Exception e)
            {
                string a = e.Message;
                return false;
            }
        }

        public bool Eliminar(LogEmpresa log)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.logEmpresaDAL.Remove(log);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Modificar(LogEmpresa log)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.logEmpresaDAL.Update(log);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool getUser(string user, string pass)
        {
            
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    return unitOfWork.logEmpresaDAL.getUser(user, pass);
                }
            }
            catch (Exception e)
            {
                
                string a = e.Message;
                return false;
            }
        }
    }
}
