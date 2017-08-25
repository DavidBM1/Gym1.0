using Gym5BLL.Interfaces;
using Gym5DAL;
using Gym5DAL.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym5BLL.Implementations
{
    public class CompraProductoBLLImp : ICompraProductoBLL
    {
        private UnitOfWork unitOfWork;
        IConsecutivosBLL consecutivosBLL;
        IProductoBLL productoBLL;

        public CompraProductoBLLImp()
        {

        }

        public bool Agregar(CompraProducto cierreProducto)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.compraProductoDAL.Add(cierreProducto);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CompraProducto> BuscarCierre(string id)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    using (GymContext Context = new GymContext())
                    {
                        List<CompraProducto> p = Context.CompraProductoes.Select(n => n).Where(l => l.idCompra == id).ToList();
                        return p;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Eliminar(CompraProducto cierreProducto)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.compraProductoDAL.Remove(cierreProducto);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Modificar(CompraProducto cierreProducto)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.compraProductoDAL.Update(cierreProducto);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Crear(string idCompra, string[,] productos)
        {
            productoBLL = new ProductoBLLImp();
            Producto pr;
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    using (GymContext Context = new GymContext())
                    {
                        for (int i = 0; i < productos.Length; i++)
                        {
                            if (productos[i, 0] == null)
                            {
                                return true;
                            }
                            else
                            {
                                string id = ConsecutivoModifica("CRM");
                                CompraProducto cp = new CompraProducto()
                                {
                                    idCompraProducto = id,
                                    idCompra = idCompra,
                                    idProducto = productos[i, 0],
                                    cantidad = int.Parse(productos[i, 1]),
                                    total = productos[i, 2]
                                };
                                pr = productoBLL.BuscarPorId(productos[i, 0]);
                                pr.cantidad = pr.cantidad - int.Parse(productos[i, 1]);
                                unitOfWork.compraProductoDAL.Add(cp);
                                productoBLL.Modificar(pr);
                                unitOfWork.Complete();
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception a)
            {
                string e = a.Message;
                return false;
            }
        }

        private string ConsecutivoModifica(string Prefijo)
        {
            consecutivosBLL = new ConsecutivosBLLImp();

            Consecutivo consecutivos = consecutivosBLL.BuscarPorId(Prefijo);
            consecutivos.valor++;
            int valorr = consecutivos.valor;
            Consecutivo consecutivos2 = new Consecutivo()
            {
                prefijo = consecutivos.prefijo,
                valor = valorr
            };
            consecutivosBLL.Modificar(consecutivos2);
            return string.Format("{0}-{1}", consecutivos.prefijo, valorr);
        }
    }
}
