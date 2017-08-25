using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gym5BLL.Implementations;
using Gym5DAL;
using System.Web.Security;
using Gymm1._0.Clases;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace Gymm1._0.Controllers
{
    public class ProductoController : Controller
    {
       
         private IProductoBLL  productoBLL;
      
        FuncionesBasicas fb = new FuncionesBasicas();

        public ActionResult TablaPro()
        {
            if (Session["empresa"] == null || Session["usuario"] == null || Session["permiso"] == null) return RedirectToAction("Login1", "Home");
            if (Session["permiso"].ToString() == "Instructor") return RedirectToAction("TablaMedidas", "Medidas");
            return View();
        }

        public ActionResult Tabla()
        {
            productoBLL = new ProductoBLLImp();
            List<Producto> resultado = productoBLL.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = String.Empty;
            foreach (Producto per in resultado)
            {
               
                htmlCode += "<tr>" +
                                "<td>" + per.idProducto + "</td>" +
                                "<td>" + per.nombre + "</td>" +
                                "<td>" + per.cantidad + "</td>" +
                                "<td>" + per.precioCompra + "</td>" +
                                "<td>" + per.precioVenta + "</td>" +
                                "<td class=\"text - right\" > " +
                                    "<a href = \"#\" class=\"btn btn-simple btn-info btn-icon \" data-toggle=\"modal\" data-target=\"#myModal2\" ><i class=\"fa fa-user\"></i></a>" +
                                    "<a href = \"#\" class=\"btn btn-simple btn-warning btn-icon \" data-toggle=\"modal\" data-target=\"#myModal1\" onclick=\"EditarAjax('" + per.idProducto + "')\"><i class=\"fa fa-edit\"></i></a>" +
                                    "<a href = \"#\" onclick=\"warninMessage(); EliminarCargar('" + per.idProducto + "')\" class=\"btn btn-simple btn-danger btn-icon\" ><i class=\"fa fa-times\" ></i></a>" +
                                "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }
       

        public ActionResult Ingreso(string nombre, int cantidad, string precioCompra, string precioVenta)
        {

            productoBLL = new ProductoBLLImp();
            string idproString;

            idproString = fb.ConsecutivoModifica("Pro");
            

            Producto per = new Producto
            {
                nombre = nombre,
                cantidad = cantidad,
                precioCompra = precioCompra,
                precioVenta = precioVenta,                
                idNombreEmpresa = (string)Session["empresa"],
                idProducto = idproString
            };
            bool a = productoBLL.Agregar(per);
            return Json(new { bol = a }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(string idpersona)
        {
            productoBLL = new ProductoBLLImp();
            Producto per = productoBLL.BuscarPorId(idpersona);
            
            return Json(new
            {
                nombre = per.nombre,
                cantidad = per.cantidad,
                precioCompra = per.precioCompra,
                precioVenta = per.precioVenta,               
                idProducto = per.idProducto
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarEdicion(string nombre, int cantidad, string precioCompra, string precioVenta,string idpro)
        {
            productoBLL = new ProductoBLLImp();
            Producto per = new Producto
            {
                nombre = nombre,
                cantidad = cantidad,
                precioCompra = precioCompra,
                precioVenta = precioVenta,
                idNombreEmpresa = (string)Session["empresa"],
                idProducto = idpro
            };
            productoBLL.Modificar(per);
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar(string idpersona)
        {
            productoBLL = new ProductoBLLImp();
            bool a = productoBLL.Eliminar(idpersona);
            return Json(new { result = a }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Clases"), "Productos.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<Producto> cm = new List<Producto>();
            using (GymContext dc = new GymContext())
            {
                cm = dc.Productoes.ToList();
            }
            ReportDataSource rd = new ReportDataSource("DataSet1", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;
            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);


            return File(renderedBytes, mimeType);
        }
    }
}