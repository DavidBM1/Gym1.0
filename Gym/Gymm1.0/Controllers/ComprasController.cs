using Gym5BLL.Implementations;
using Gym5BLL.Interfaces;
using Gym5DAL;
using Gym5DAL.Implementations;
using Gymm1._0.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Gymm1._0.Controllers
{
    public class ComprasController : Controller
    {
        FuncionesBasicas fb = new FuncionesBasicas();
        IProductoBLL productobll;
        ICompraBLL compraBLL;
        ICompraProductoBLL compraProductoBLL;
        static string[,] productos;
        static int cantidad;

        public ActionResult TablaCompras()
        {
            if (Session["empresa"] == null || Session["usuario"] == null || Session["permiso"] == null) return RedirectToAction("Login1", "Home");
            if (Session["permiso"].ToString() == "Instructor") return RedirectToAction("TablaMedidas", "Medidas");
            productos = new string[100, 3];
            cantidad = 0;
            return View();
        }

        public ActionResult Tabla()
        {
            compraBLL = new CompraBLLImp();
            List<Compra> resultado = compraBLL.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = String.Empty;
            foreach (Compra per in resultado)
            {
                htmlCode += "<tr>" +
                                "<td>" + per.idCompra + "</td>" +
                                "<td>" + per.correo + "</td>" +
                                "<td>" + per.fecha.Value.ToString("MM/dd/yyyy") + "</td>" +
                                "<td>" + per.total + "</td>" +
                                "<td class=\"text - right\" > " +
                                    "<a href=\"#\" class=\"btn btn-simple btn-info btn-icon\" data-toggle=\"modal\" data-target=\"#myModal2\" onclick=\"cargarTabla('" + per.idCompra + "')\"><i class=\"fa fa-shopping-basket\" ></i></a>" +
                                "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Segunda(string id)
        {
            compraProductoBLL = new CompraProductoBLLImp();
            productobll = new ProductoBLLImp();
            List<CompraProducto> resultado = compraProductoBLL.BuscarCierre(id);
            String htmlCode = String.Empty;
            foreach (CompraProducto per in resultado)
            {
                Producto pro = productobll.BuscarPorId(per.idProducto);
                htmlCode += "<tr>" +
                                "<td>" + pro.idProducto + "</td>" +
                                "<td>" + pro.nombre + "</td>" +
                                "<td>" + per.cantidad + "</td>" +
                                "<td>" + pro.precioVenta + "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Combo()
        {
            productobll = new ProductoBLLImp();
            List<Producto> productos;
            productos = productobll.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = "<select id = \"rol\" class=\"form-control\" > ";
            foreach (Producto pr in productos)
            {
                htmlCode += "<option value = \"" + pr.idProducto + "\"  > " + pr.nombre + " </option >";
            }
            htmlCode += "</select>";
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tabla2(string id, string can)
        {
            productobll = new ProductoBLLImp();
            string htmlCode = string.Empty;
            Producto PR = productobll.BuscarPorId(id);
            productos[cantidad,0] = id;
            productos[cantidad, 1] = can;
            int cann;
            if (can.Equals(string.Empty))
            {
                cann = 1;
            }
            else
            {
                cann = int.Parse(can);
            }
            int can2 = int.Parse(PR.precioVenta);
            can2 = can2 * cann;
            productos[cantidad, 2] = can2.ToString();
            cantidad++;
            htmlCode = "<tr id=\"" + PR.idProducto + "\">" +
                        "<td>"+PR.idProducto+"</td>" +
                        "<td>"+PR.nombre+"</td>" +
                        "<td>" + cann + "</td>" +
                        "<td>"+ can2+ "</td>" +
                        "<td class=\"text-right\">" +
                           "<a href = \"#\" class=\"btn btn-simple btn-danger btn-icon \" onclick=\"eliminar('"+PR.idProducto+"')\"><i class=\"fa fa-times\" ></i></a>" +
                        "</td>"+
                        "</tr>";
            return Json(new { result = htmlCode, total = can2 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EliminarProd(string idd)
        {
            bool acomodar = false;
            int total = 0;
            for(int i = 0; i<productos.Length; i++)
            {
                if (acomodar)
                {
                    if (productos[i, 0] == null)
                    {
                        return Json(new { result = total }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        productos[i - 1, 0] = productos[i, 0];
                        productos[i - 1, 1] = productos[i, 1];
                        productos[i - 1, 2] = productos[i, 2];
                        productos[i, 0] = null;
                        productos[i, 1] = null;
                        productos[i, 2] = null;
                    }
                }
                else
                {
                    if (productos[i, 0].Equals(idd))
                    {
                        acomodar = true;
                        productos[i, 0] = null;
                        productos[i, 1] = null;
                        total = int.Parse(productos[i, 2]);
                        productos[i, 2] = null;
                        cantidad--;
                    }
                }
            }
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Insertar(string correo, string total)
        {
            compraBLL = new CompraBLLImp();
            compraProductoBLL = new CompraProductoBLLImp();
            string idCompra = fb.ConsecutivoModifica("CMP");
            Compra compra = new Compra()
            {
                idCompra = idCompra,
                correo = correo,
                fecha = DateTime.Now,
                total = total,
                idNombreEmpresa = Session["empresa"].ToString()
            };
            bool a = compraBLL.Agregar(compra);
            bool b = compraProductoBLL.Crear(idCompra, productos);
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("maicoldv@gmail.com");
                    mail.To.Add(correo);
                    mail.Subject = "Usuario de Ragnarok";
                    mail.Body = Mensaje(total);
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("maicoldv@gmail.com", ""),
                        Timeout = 20000
                    };
                    smtp.Send(mail);
                }
            }
            catch (Exception e)
            {
                string aa = e.Message;
                return Json(new { bol = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { result = a, result2 = b }, JsonRequestBehavior.AllowGet);
        }

        private string Mensaje(string total)
        {
            productobll = new ProductoBLLImp();
            Producto pr;
            pr = productobll.BuscarPorId(productos[0, 0]);
            string msj = "<h3>Producto:" + pr.nombre + "</h3><br>" +
                         "<h3>Cantidad:" + productos[0, 1] + "</h3><br>" +
                         "<h3>Precio:" + pr.precioVenta + "</h3><br>***********************************************************";
            for (int i = 1; i < productos.Length; i++)
            {
                if (productos[i, 0] == null)
                {
                    msj += "<br><h2>Total:" + total + "</h2>";
                    return msj;
                }
                else
                {
                    pr = productobll.BuscarPorId(productos[i, 0]);
                    msj += "<h3>Producto:" + pr.nombre + "</h3><br>" +
                           "<h3>Cantidad:" + productos[i, 1] + "</h3><br>" +
                           "<h3>Precio:" + pr.precioVenta + "</h3><br>***********************************************************";
                }
            }
            msj += "<br><h2>Total:" + total + "</h2>";
            return msj;
        }
    }
}
