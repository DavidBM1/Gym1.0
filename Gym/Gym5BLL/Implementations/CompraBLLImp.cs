using System;
using Gym5BLL.Implementations;
using System.Collections.Generic;
using System.Linq;
using Gym5DAL.Implementations;
using Gym5DAL;
using Gym5BLL.Interfaces;

namespace Gym5BLL.Implementations
{
    public class CompraBLLImp : ICompraBLL
    {
        private UnitOfWork unitOfWork;

        public CompraBLLImp()
        {

        }

        public bool Agregar(Compra compra)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.compraDAL.Add(compra);
                    unitOfWork.Complete();
                }

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Eliminar(Compra cierre)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.compraDAL.Remove(cierre);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public bool Modificar(Compra cierre)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.compraDAL.Update(cierre);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public List<Compra> BuscarTodos(string empresa)
        {
            try
            {
                List<Compra> resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    using (GymContext Context = new GymContext())
                    {
                        resultado = Context.Compras.Select(n => n).Where(l => l.idNombreEmpresa == empresa).ToList();
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
