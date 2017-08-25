using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym5DAL;
using Gym5DAL.Implementations;

namespace Gym5BLL.Implementations
{
    public class CierreProductoBLLImp : ICierreProductoBLL
    {
        private UnitOfWork unitOfWork;
        IConsecutivosBLL consecutivosBLL;

        public CierreProductoBLLImp()
        {

        }

        public bool Agregar(CierreProducto cierreProducto)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    unitOfWork.cierreProductoDAL.Add(cierreProducto);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<CierreProducto> BuscarCierre(string id)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    using (GymContext Context = new GymContext())
                    {
                        List<CierreProducto> p = Context.CierreProductoes.Select(n => n).Where(l => l.idCierre == id).ToList();
                        return p;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Eliminar(CierreProducto cierreProducto)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.cierreProductoDAL.Remove(cierreProducto);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Modificar(CierreProducto cierreProducto)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {

                    unitOfWork.cierreProductoDAL.Update(cierreProducto);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Crear(string empresa, string cierre)
        {
            try
            {
                using (unitOfWork = new UnitOfWork(new GymContext()))
                {
                    using (GymContext Context = new GymContext())
                    {
                        List<Producto> p = Context.Productoes.Select(n => n).Where(l => l.idNombreEmpresa == empresa).ToList();
                        foreach (Producto pro in p)
                        {
                            string id = ConsecutivoModifica("CRM");
                            CierreProducto cp = new CierreProducto()
                            {
                                idProducto = pro.idProducto,
                                idCierre = cierre,
                                idNombreEmpresa = empresa,
                                cantidadCierre = pro.cantidad,
                                idCierreProducto = id
                            };
                            unitOfWork.cierreProductoDAL.Add(cp);
                            unitOfWork.Complete();
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
