using Gym5BLL.Implementations;
using Gym5DAL;
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
    public class UsuariosController : Controller
    {
        ILogUsuarioBLL usuariosBLL;
        FuncionesBasicas fb = new FuncionesBasicas();

        public ActionResult TablaUsu()
        {
            if (Session["empresa"] == null || Session["usuario"] == null || Session["permiso"] == null) return RedirectToAction("Login1", "Home");
            if (Session["permiso"].ToString() == "Instructor") return RedirectToAction("TablaMedidas", "Medidas");
            if (Session["permiso"].ToString() == "ventas") return RedirectToAction("TablaPro", "Producto");
            return View();
        }

        public ActionResult Tabla()
        {
            usuariosBLL = new LogUsuarioBLLImp();
            List<LogUsuario> resultado = usuariosBLL.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = String.Empty;
            foreach (LogUsuario per in resultado)
            {
                htmlCode += "<tr>" +
                                "<td>" + per.usuario + "</td>" +
                                "<td>" + per.permiso + "</td>" +
                                "<td class=\"text - right\" > " +                                    
                                    "<a href = \"#\" class=\"btn btn-simple btn-warning btn-icon \" data-toggle=\"modal\" data-target=\"#myModal1\" onclick=\"EditarAjax('" + per.usuario + "')\"><i class=\"fa fa-edit\"></i></a>" +
                                    "<a href = \"#\" onclick=\"warninMessage(); EliminarCargar('" + per.usuario + "')\" class=\"btn btn-simple btn-danger btn-icon\"><i class=\"fa fa-times\" ></i></a>" +
                                "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ingreso(string usuarioing, string contrasenaing, string rol, string emailIn)
        {

            usuariosBLL = new LogUsuarioBLLImp();

            LogUsuario per = new LogUsuario
            {
                usuario = usuarioing,
                pass = fb.Encripta(contrasenaing),
                idNombreEmpresa = (string)Session["empresa"],
                permiso = rol
            };
            bool a = usuariosBLL.Agregar(per);
            if (a)
            {
                try
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress("maicoldv@gmail.com");
                        mail.To.Add(emailIn);
                        mail.Subject = "Usuario de Ragnarok";
                        mail.Body = string.Format("{0}:{1}", usuarioing, contrasenaing);
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
                        return Json(new { bol = true }, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception e)
                {
                    string aa = e.Message;
                        return Json(new { bol = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else return Json(new { bol = a }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar(string idpersona)
        {
            usuariosBLL = new LogUsuarioBLLImp();
            bool a = usuariosBLL.Eliminar(idpersona);
            return Json(new { result = a }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(string usuario)
        {
            usuariosBLL = new LogUsuarioBLLImp();
            LogUsuario ins = usuariosBLL.BuscarPorId(usuario);
            Session["RolTemporal"] = ins.permiso;
            return Json(new
            {
                usuario = ins.usuario,
                pass = fb.Desencripta(ins.pass),
                rol = ins.permiso,
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarEdicion(string usuario, string contrasena, string rol)
        {
            usuariosBLL = new LogUsuarioBLLImp();

            LogUsuario lu = new LogUsuario()
            {
                usuario = usuario,
                pass = fb.Encripta(contrasena),
                idNombreEmpresa = (string)Session["empresa"],
                permiso = rol
            };
            usuariosBLL.Modificar(lu);
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);
        }
    }
}