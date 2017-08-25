using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Gym5BLL.Implementations;
using Gym5DAL;
using System.Web.Security;
using Gymm1._0.Clases;

namespace Gymm1._0.Controllers
{
    public class InstructoresController : Controller
    {

        private IInstructorBLL instructorBLL;
        private ILogUsuarioBLL usuarioBLL;

        FuncionesBasicas fb = new FuncionesBasicas();

        public ActionResult TablaInts()
        {
            if (Session["empresa"] == null || Session["usuario"] == null || Session["permiso"] == null) return RedirectToAction("Login1", "Home");
            if (Session["permiso"].ToString() == "Instructor") return RedirectToAction("TablaMedidas", "Medidas");
            if (Session["permiso"].ToString() == "ventas") return RedirectToAction("TablaPro", "Producto");
            return View();
        }
        public ActionResult Ingreso(string nombreing, string primerapeing, string segundoapeing, string telefonoing, string usuarioing,
                                    string passing)
        {
            string idInstructorString;
            instructorBLL = new InstructorBLLImp();
            usuarioBLL = new LogUsuarioBLLImp();

            idInstructorString = fb.ConsecutivoModifica("INS");

            LogUsuario lu = new LogUsuario()
            {
                usuario = usuarioing,
                pass = fb.Encripta(passing),
                idNombreEmpresa = (string)Session["empresa"],
                permiso = "Instructor"
            };
            InstructorGym ins = new InstructorGym()
            {
                idInstructor = idInstructorString,
                nombre = nombreing,
                apellido1 = primerapeing,
                apellido2 = segundoapeing,
                telefono = telefonoing,
                usuario = usuarioing,
                idNombreEmpresa = (string)Session["empresa"]
            };
            usuarioBLL.Agregar(lu);
            instructorBLL.Agregar(ins);
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(string idInstructor)
        {
            instructorBLL = new InstructorBLLImp();
            InstructorGym ins = instructorBLL.BuscarPorId(idInstructor);
            return Json(new
            {
                nombre = ins.nombre,
                apellido1 = ins.apellido1,
                apellido2 = ins.apellido2,
                telefono = ins.telefono,
                usuario = ins.usuario,
                idIns = ins.idInstructor
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarEdicion(string nombreing, string primerapeing, string segundoapeing, string telefonoing, string usuarioing,
                                           string passing, string idInstructor)
        {
            instructorBLL = new InstructorBLLImp();
            usuarioBLL = new LogUsuarioBLLImp();
            LogUsuario lu = new LogUsuario()
            {
                usuario = usuarioing,
                pass = passing,
                idNombreEmpresa = (string)Session["empresa"],
                permiso = "Instructor"
            };

            InstructorGym ins = new InstructorGym
            {
                idInstructor = idInstructor,
                nombre = nombreing,
                apellido1 = primerapeing,
                apellido2 = segundoapeing,
                telefono = telefonoing,
                idNombreEmpresa = (string)Session["empresa"],
                usuario = usuarioing
            };
            usuarioBLL.Modificar(lu);
            instructorBLL.Modificar(ins);
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarEdicion2(string nombreing, string primerapeing, string segundoapeing, string telefonoing, string idInstructor)
        {
            instructorBLL = new InstructorBLLImp();
            usuarioBLL = new LogUsuarioBLLImp();
            InstructorGym InsViejo = instructorBLL.BuscarPorId(idInstructor);
            InstructorGym ins = new InstructorGym
            {
                idInstructor = idInstructor,
                nombre = nombreing,
                apellido1 = primerapeing,
                apellido2 = segundoapeing,
                telefono = telefonoing,
                usuario = InsViejo.usuario,
                idNombreEmpresa = (string)Session["empresa"]
            };
            instructorBLL.Modificar(ins);
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar(string idpersona)
        {
            instructorBLL = new InstructorBLLImp();
            bool a = instructorBLL.Eliminar(idpersona);
            return Json(new { result = a }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tabla()
        {
            instructorBLL = new InstructorBLLImp();
            List<InstructorGym> resultado = instructorBLL.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = String.Empty;
            foreach (InstructorGym ins in resultado)
            {
                htmlCode += "<tr>" +
                                "<td>" + ins.idInstructor + "</td>" +
                                "<td>" + string.Format("{0} {1} {2}", ins.nombre, ins.apellido1, ins.apellido2) + "</td>" +
                                "<td>" + ins.telefono + "</td>" +
                                "<td>" + ins.usuario + "</td>" +
                                "<td class=\"text - right\" > " +
                                    "<a href = \"#\" class=\"btn btn-simple btn-warning btn-icon \" data-toggle=\"modal\" data-target=\"#myModal1\" onclick=\"EditarAjax('" + ins.idInstructor + "')\"><i class=\"fa fa-edit\"></i></a>" +
                                    "<a href = \"#\" onclick=\"warninMessage(); EliminarCargar('" + ins.idInstructor + "')\" class=\"btn btn-simple btn-danger btn-icon\"><i class=\"fa fa-times\" ></i></a>" +
                                "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }
    }
}
