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
    public class ClientesController : Controller
    {
        private IPersonaBLL personaBLL;
        private IInstructorBLL instructorBLL;
        private IPersonaMedidasBLL personaMedidasBLL;
        FuncionesBasicas fb = new FuncionesBasicas();

        public ActionResult TablaClient()
        {
                if (Session["empresa"] == null || Session["usuario"] == null || Session["permiso"] == null) return RedirectToAction("Login1", "Home");
                if (Session["permiso"].ToString() == "Instructor") return RedirectToAction("TablaMedidas", "Medidas");
                if (Session["permiso"].ToString() == "ventas") return RedirectToAction("TablaPro", "Producto");
            return View();
        }

        public ActionResult Tabla()
        {
            personaBLL = new PersonaBLLImp();
            instructorBLL = new InstructorBLLImp();
            InstructorGym instructor;
            List<PersonaGym> resultado = personaBLL.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = String.Empty;
            foreach (PersonaGym per in resultado)
            {
                instructor = instructorBLL.BuscarPorId(per.idInstructor);
                htmlCode += "<tr>" +
                                "<td>" + per.idPersona + "</td>" +
                                "<td>" + string.Format("{0} {1} {2}", per.nombre, per.apellido1, per.apellido2) + "</td>" +
                                "<td>" + per.telefono + "</td>" +
                                "<td>" + per.fechaPago.Value.ToString("MM/dd/yyyy") + "</td>" +
                                "<td>" + string.Format("{0} {1}", instructor.nombre, instructor.apellido1) + "</td>" +
                                "<td class=\"text - right\" > " +
                                    "<a href = \"#\" class=\"btn btn-simple btn-info btn-icon \" data-toggle=\"modal\" data-target=\"#myModal2\" ><i class=\"fa fa-user\"></i></a>" +
                                    "<a href = \"#\" class=\"btn btn-simple btn-warning btn-icon \" data-toggle=\"modal\" data-target=\"#myModal1\" onclick=\"EditarAjax('"+per.idPersona+"')\"><i class=\"fa fa-edit\"></i></a>" +
                                    "<a href = \"#\" onclick=\"warninMessage(); EliminarCargar('" + per.idPersona + "')\" class=\"btn btn-simple btn-danger btn-icon\"><i class=\"fa fa-times\" ></i></a>" +
                                "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Combo()
        {
            instructorBLL = new InstructorBLLImp();
            List<InstructorGym> instructoresList;
            instructoresList = instructorBLL.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = "<select name=\"Instructor\" class=\"form - control\" data-title=\"Seleccione Opcion\" data-style=\"btn-default btn- block\" data-menu-style=\"dropdown-blue\" id=\"InstructoresIng\">";
            foreach (InstructorGym ins in instructoresList)
            {
                htmlCode += "<option value = \"" + ins.idInstructor + "\"  > " + string.Format("{0} {1}", ins.nombre, ins.apellido1) + " </option >";
            }
            htmlCode += "</select>";
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Combo2()
        {
            instructorBLL = new InstructorBLLImp();
            List<InstructorGym> instructoresList;
            instructoresList = instructorBLL.BuscarTodos(Session["empresa"].ToString());
            String htmlCode = "<select name=\"Instructor\" class=\"form - control\" data-title=\"Seleccione Opcion\" data-style=\"btn-default btn- block\" data-menu-style=\"dropdown-blue\" id=\"InstructoresIng2\">";
            foreach (InstructorGym ins in instructoresList)
            {
                htmlCode += "<option value = \"" + ins.idInstructor + "\"  > " + string.Format("{0} {1}", ins.nombre, ins.apellido1) + " </option >";
            }
            htmlCode += "</select>";
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ingreso (string nombreing, string primerapeing, string segundoapeing, string telefonoing, string fechaingresoing,
                                        string instructoresing, string enfermedading, string observacionesing, string fechaing, string pechoing,
                                        string espaldaing, string cinturaing, string caderaing, string  piernaing, string pantorrillaing, string brazoing,
                                        string antebrazoing, string tricepsing, string abdominaling, string supralliacoing, string subscapularing,
                                        string pesoing, string estaturaing, string imging, string grasaing)
        {
            personaBLL = new PersonaBLLImp();
            personaMedidasBLL = new PersonaMedidasBLLImp();
            string idPersonaString, idMedidasString;

            idPersonaString = fb.ConsecutivoModifica("CLI");
            idMedidasString = fb.ConsecutivoModifica("MED");
            
            PersonaGym per = new PersonaGym
            {
                nombre = nombreing, apellido1 = primerapeing, apellido2 = segundoapeing, telefono = telefonoing,
                fechaPago = DateTime.Parse(fb.fechaConvertidor(fechaingresoing)), idInstructor = instructoresing, enfermedad = enfermedading, observaciones = observacionesing,
                idNombreEmpresa = (string)Session["empresa"], idPersona = idPersonaString
            };
            PersonaMedidasGym medidas = new PersonaMedidasGym()
            {
                idPersona = idPersonaString, fecha = DateTime.Parse(fb.fechaConvertidor(fechaing)), pecho = double.Parse(pechoing), espalda = double.Parse(espaldaing), cintura = double.Parse(cinturaing),
                cadera = double.Parse(caderaing), pierna = double.Parse(piernaing), pantorrilla = double.Parse(pantorrillaing), brazo = double.Parse(brazoing), antebrazo = double.Parse(antebrazoing),
                triceps = double.Parse(tricepsing), abdominal = double.Parse(abdominaling), supraIliaco = double.Parse(subscapularing), subscapular = double.Parse(subscapularing), peso = double.Parse(pesoing),
                estatura = double.Parse(estaturaing), IMC = double.Parse(imging), porcentajeGrasa = double.Parse(grasaing), idMedida = idMedidasString
            };
            bool resultadoBool;
            bool a = personaBLL.Agregar(per);
            bool b = personaMedidasBLL.Agregar(medidas);
            if (!a || !b)
                resultadoBool = false;
            else
                resultadoBool = true;
            return Json(new { result = string.Empty, bol = resultadoBool }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Editar(string idpersona)
        {
            personaBLL = new PersonaBLLImp();
            PersonaGym per = personaBLL.BuscarPorId(idpersona);
            string fechaa = fb.fechaConvertidorInv(per.fechaPago.ToString());
            return Json(new { nombre = per.nombre, apellido1 = per.apellido1, apellido2 = per.apellido2,
                              telefono = per.telefono, fechaP = fechaa, instructor = per.idInstructor,
                              enfermedad = per.enfermedad, observaciones = per.observaciones, idPersona = per.idPersona}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarEdicion(string nombreing, string primerapeing, string segundoapeing, string telefonoing, string fechaingresoing,
                                            string instructoresing, string enfermedading, string observacionesing, string idpersona)
        {
            personaBLL = new PersonaBLLImp();
            PersonaGym per = new PersonaGym
            {
                nombre = nombreing,
                apellido1 = primerapeing,
                apellido2 = segundoapeing,
                telefono = telefonoing,
                fechaPago = DateTime.Parse(fb.fechaConvertidor(fechaingresoing)),
                idInstructor = instructoresing,
                enfermedad = enfermedading,
                observaciones = observacionesing,
                idNombreEmpresa = (string)Session["empresa"],
                idPersona = idpersona
            };
            personaBLL.Modificar(per);
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar(string idpersona)
        {
            personaBLL = new PersonaBLLImp();
            bool a = personaBLL.Eliminar(idpersona);
            return Json(new { result = a }, JsonRequestBehavior.AllowGet);
        }
    }
}
