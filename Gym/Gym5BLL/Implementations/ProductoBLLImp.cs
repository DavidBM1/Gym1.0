using Gym5DAL;
using Gym5DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Implementations
{
    public class ProductoBLLImp: IProductoBLL
    {
        private UnitOfWork unitOfWork;

        public ProductoBLLImp()
        {

        }

        public List<Producto> BuscarTodos(string empresa)
        {
            try
            {
                List<Producto> resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    using (GymContext Context = new GymContext())
                    {
                        resultado = Context.Productoes.Select(n => n).Where(l => l.idNombreEmpresa == empresa).ToList();
                    }
                }
                return resultado;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Producto BuscarPorId(string idpro)
        {
            try
            {
                Producto resultado;
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    resultado = unitOfWork.productoDAL.Get(idpro);
                }

                return resultado;
            }
            catch (Exception)
            {

                return null;
            }
        }
        public bool Agregar(Producto producto)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.productoDAL.Add(producto);
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
                        Cierre per = new Cierre();
                        List<Producto> p = Context.Productoes.Select(n => n).Where(l => l.idProducto == id).ToList();
                        foreach (Producto pe in p)
                        {
                            Context.Productoes.Remove(pe);
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

        public bool Modificar(Producto producto)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.productoDAL.Update(producto);
                    unitOfWork.Complete();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
