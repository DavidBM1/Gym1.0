using Gym5BLL.Implementations;
using Gym5DAL;
using Gym5DAL.Implementations;
using Gymm1._0.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gymm1._0.Controllers
{
    public class CierresController : Controller
    {
        ICierreBLL cierreBLL;
        ICierreProductoBLL productoCierreBLL;
        IProductoBLL productoBLL;
        FuncionesBasicas fb = new FuncionesBasicas();

        public ActionResult TablaCierres()
        {
            if (Session["empresa"] == null || Session["usuario"] == null || Session["permiso"] == null) return RedirectToAction("Login1", "Home");
            if (Session["permiso"].ToString() == "Instructor") return RedirectToAction("TablaMedidas", "Medidas");
            return View();
        }

        public ActionResult Tabla()
        {
            cierreBLL = new CierreBLLImp();
            List<Cierre> resultado = cierreBLL.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = String.Empty;
            foreach (Cierre per in resultado)
            {
                htmlCode += "<tr>" +
                                "<td>" + per.idCierre + "</td>" +
                                "<td>" + per.fecha.Value.ToString("MM/dd/yyyy") + "</td>" +
                                "<td>" + per.usuario + "</td>" +
                                "<td class=\"text - right\" > " +
                                    "<a href=\"#\" class=\"btn btn-simple btn-info btn-icon\" data-toggle=\"modal\" data-target=\"#myModal\" onclick=\"cargarTabla('"+per.idCierre+"')\"><i class=\"fa fa-shopping-basket\" ></i></a>" +
                                "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tabla2(string id)
        {
            productoCierreBLL = new CierreProductoBLLImp();
            productoBLL = new ProductoBLLImp();
            List<CierreProducto> resultado = productoCierreBLL.BuscarCierre(id);
            String htmlCode = String.Empty;
            foreach (CierreProducto per in resultado)
            {
                Producto pro = productoBLL.BuscarPorId(per.idProducto);
                htmlCode += "<tr>" +
                                "<td>" + per.idCierreProducto + "</td>" +
                                "<td>" + per.idProducto + "</td>" +
                                "<td>" + pro.nombre + "</td>" +
                                "<td>" + per.cantidadCierre + "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Crear()
        {
            cierreBLL = new CierreBLLImp();
            productoCierreBLL = new CierreProductoBLLImp();
            string idMedidasString = fb.ConsecutivoModifica("CIR");
            Cierre cierre = new Cierre()
            {
                idCierre = idMedidasString,
                fecha = DateTime.Now,
                usuario = Session["usuario"].ToString(),
                idNombreEmpresa = Session["empresa"].ToString()
            };
            bool a = cierreBLL.Agregar(cierre);
            bool b = productoCierreBLL.Crear(Session["empresa"].ToString(), idMedidasString);
            return Json(new { result =  a, result2 = b}, JsonRequestBehavior.AllowGet);
        }
    }
}
