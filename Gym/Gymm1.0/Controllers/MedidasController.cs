using Gym5BLL.Implementations;
using Gym5DAL;
using Gymm1._0.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gymm1._0.Controllers
{
    public class MedidasController : Controller
    {
        IPersonaBLL personaBLL;
        IInstructorBLL instructorBLL;
        IPersonaMedidasBLL medidasBLL;
        FuncionesBasicas fb = new FuncionesBasicas();

        public static string grafica;

        public ActionResult TablaMedidas()
        {
            if (Session["empresa"] == null || Session["usuario"] == null || Session["permiso"] == null) return RedirectToAction("Login1", "Home");
            if (Session["permiso"].ToString() == "ventas") return RedirectToAction("TablaPro", "Producto");
            return View();
        }

        public ActionResult Grafica()
        {
            if (Session["empresa"] == null || Session["usuario"] == null || Session["permiso"] == null) return RedirectToAction("Login1", "Home");
            if (Session["permiso"].ToString() == "ventas") return RedirectToAction("TablaPro", "Producto");
            return View();
        }

        public ActionResult Mostrar(string idPersona)
        {

            return Json(new { result = grafica }, JsonRequestBehavior.AllowGet);
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
                                    "<a href=\"#\" class=\"btn btn-simple btn-info btn-icon\" data-toggle=\"modal\" data-target=\"#myModal\" ><i class=\"fa fa-bar-chart\" onclick=\"CargarTabla2('" + per.idPersona + "')\"></i></a>" +
                                "</td>" +
                            "<tr>";
            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Tabla2(string idPersona)
        {
            medidasBLL = new PersonaMedidasBLLImp();
            personaBLL = new PersonaBLLImp();
            List<PersonaMedidasGym> resultado = medidasBLL.BuscarPorPersona(idPersona);
            String htmlCode = String.Empty;
            foreach (PersonaMedidasGym per in resultado)
            {
                htmlCode += "<tr>" +
                                "<td>" + per.idMedida + "</td>" +
                                "<td>" + per.fecha.ToString("MM/dd/yyyy") + "</td>" +
                                "<td class=\"text - right\" > " +
                                    "<a href=\"#\" class=\"btn btn-simple btn-warning btn-icon\" data-toggle=\"modal\" data-target=\"#myModal22\" ><i class=\"fa fa-edit\" ></i></a>" +
                                    "<a href = \"#\" onclick=\"warninMessage(); EliminarCargar('" + per.idMedida + "')\" class=\"btn btn-simple btn-danger btn-icon\"><i class=\"fa fa-times\" ></i></a>" +
                                "</td>" +
                            "<tr>";
            }
            string Nombre = string.Empty;
            PersonaGym gym = personaBLL.BuscarPorId(idPersona);
            Nombre += string.Format("{0} {1} {2}", gym.nombre, gym.apellido1, gym.apellido2);
            return Json(new { result = htmlCode, nombre = Nombre }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ingreso(string fechaing, string pechoing, string espaldaing, string cinturaing, string caderaing, string piernaing, string pantorrillaing,
                                        string brazoing, string antebrazoing, string tricepsing, string abdominaling, string supralliacoing, string subscapularing,
                                        string pesoing, string estaturaing, string imging, string grasaing, string idPersona)
        {
            medidasBLL = new PersonaMedidasBLLImp();
            string idMedidasString = fb.ConsecutivoModifica("MED");
            PersonaMedidasGym medidas = new PersonaMedidasGym()
            {
                idPersona = idPersona,
                fecha = DateTime.Parse(fb.fechaConvertidor(fechaing)),
                pecho = double.Parse(pechoing),
                espalda = double.Parse(espaldaing),
                cintura = double.Parse(cinturaing),
                cadera = double.Parse(caderaing),
                pierna = double.Parse(piernaing),
                pantorrilla = double.Parse(pantorrillaing),
                brazo = double.Parse(brazoing),
                antebrazo = double.Parse(antebrazoing),
                triceps = double.Parse(tricepsing),
                abdominal = double.Parse(abdominaling),
                supraIliaco = double.Parse(subscapularing),
                subscapular = double.Parse(subscapularing),
                peso = double.Parse(pesoing),
                estatura = double.Parse(estaturaing),
                IMC = double.Parse(imging),
                porcentajeGrasa = double.Parse(grasaing),
                idMedida = idMedidasString
            };
            medidasBLL.Agregar(medidas);
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Grafico(string medidaGraf, string idPersona)
        {
            string htmlCode = string.Empty;
            switch (medidaGraf)
            {
                case "pecho":
                    htmlCode = Pecho(idPersona);
                    grafica = htmlCode;
                    return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);

                case "espalda":
                    htmlCode = Espalda(idPersona);
                    grafica = htmlCode;
                    return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
                case "peso":
                    htmlCode = Peso(idPersona);
                    grafica = htmlCode;
                    return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);

            }
            return Json(new { result = htmlCode }, JsonRequestBehavior.AllowGet);
        }

        string Pecho(string id)
        {
            medidasBLL = new PersonaMedidasBLLImp();
            string HtmlX = medidasBLL.getMeses(id);
            string htmlData = string.Empty;
            using (GymContext Context = new GymContext())
            {
                List<double> p = Context.PersonaMedidasGyms.Where(v => v.idPersona == id).Select(v => v.pecho.Value).ToList();
                foreach (double d in p)
                {
                    htmlData += d.ToString() + ", ";
                }
                htmlData = htmlData.Remove(htmlData.Length - 2);
            }
            string htmlCode2 = "<script>function graf(){Highcharts.chart('container', { chart: { type: 'line' }, title: { text: 'Grafca de Pecho' }, subtitle: { text: 'Graficas' }, " +
               "xAxis: { categories: [" + HtmlX + "] }, yAxis: { title: { text: 'Medida(cm)' } }," +
               "plotOptions: { line: { dataLabels: { enabled: true }, enableMouseTracking: false } }, series: [{ name: 'Pecho', data: [" + htmlData + "] }]});}</script>";
            return htmlCode2;
        }

        string Espalda(string id)
        {
            medidasBLL = new PersonaMedidasBLLImp();
            string HtmlX = medidasBLL.getMeses(id);
            string htmlData = string.Empty;
            using (GymContext Context = new GymContext())
            {
                List<double> p = Context.PersonaMedidasGyms.Where(v => v.idPersona == id).Select(v => v.espalda.Value).ToList();
                foreach (double d in p)
                {
                    htmlData += d.ToString() + ", ";
                }
                htmlData = htmlData.Remove(htmlData.Length - 2);
            }
            string htmlCode2 = "<script>function graf(){Highcharts.chart('container', { chart: { type: 'line' }, title: { text: 'Grafca de Espalda' }, subtitle: { text: 'Graficas' }, " +
               "xAxis: { categories: [" + HtmlX + "] }, yAxis: { title: { text: 'Medida(cm)' } }," +
               "plotOptions: { line: { dataLabels: { enabled: true }, enableMouseTracking: false } }, series: [{ name: 'Espalda', data: [" + htmlData + "] }]});}</script>";
            return htmlCode2;
        }

        string Peso(string id)
        {
            medidasBLL = new PersonaMedidasBLLImp();
            string HtmlX = medidasBLL.getMeses(id);
            string htmlData = string.Empty;
            using (GymContext Context = new GymContext())
            {
                List<double> p = Context.PersonaMedidasGyms.Where(v => v.idPersona == id).Select(v => v.peso.Value).ToList();
                foreach (double d in p)
                {
                    htmlData += d.ToString() + ", ";
                }
                htmlData = htmlData.Remove(htmlData.Length - 2);
            }
            string htmlCode2 = "<script>function graf(){Highcharts.chart('container', { chart: { type: 'line' }, title: { text: 'Grafca de Peso' }, subtitle: { text: 'Graficas' }, " +
               "xAxis: { categories: [" + HtmlX + "] }, yAxis: { title: { text: 'Medida(Kg)' } }," +
               "plotOptions: { line: { dataLabels: { enabled: true }, enableMouseTracking: false } }, series: [{ name: 'Espalda', data: [" + htmlData + "] }]});}</script>";
            return htmlCode2;
        }

        public ActionResult Editar(string id)
        {
            medidasBLL = new PersonaMedidasBLLImp();
            PersonaMedidasGym m = medidasBLL.BuscarPorId(id);
            return Json(new
            {
                fechaing = m.fecha,
                pechoing = m.pecho,
                espaldaing = m.espalda,
                cinturaing = m.cintura,
                caderaing = m.cadera,
                piernaing = m.pierna,
                pantorrillaing = m.pantorrilla,
                brazoing = m.brazo,
                antebrazoing = m.antebrazo,
                tricepsing = m.triceps,
                abdominaling = m.abdominal,
                supralliacoing = m.supraIliaco,
                subscapularing = m.subscapular,
                pesoing = m.peso,
                estaturaing = m.estatura,
                imging = m.IMC,
                grasaing = m.porcentajeGrasa,
                idpersona = m.idPersona
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Eliminar(string id)
        {
            medidasBLL = new PersonaMedidasBLLImp();
            bool a = medidasBLL.Eliminar(id);
            return Json(new { bol = a }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GuardarEdicion(string fechaing, string pechoing, string espaldaing, string cinturaing, string caderaing, string piernaing, string pantorrillaing,
                                            string brazoing, string antebrazoing, string tricepsing, string abdominaling, string supralliacoing, string subscapularing,
                                            string pesoing, string estaturaing, string imging, string grasaing, string idPersona, string idMedidas)
        {
            medidasBLL = new PersonaMedidasBLLImp();
            PersonaMedidasGym medidas = new PersonaMedidasGym()
            {
                idPersona = idPersona,
                fecha = DateTime.Parse(fb.fechaConvertidor(fechaing)),
                pecho = double.Parse(pechoing),
                espalda = double.Parse(espaldaing),
                cintura = double.Parse(cinturaing),
                cadera = double.Parse(caderaing),
                pierna = double.Parse(piernaing),
                pantorrilla = double.Parse(pantorrillaing),
                brazo = double.Parse(brazoing),
                antebrazo = double.Parse(antebrazoing),
                triceps = double.Parse(tricepsing),
                abdominal = double.Parse(abdominaling),
                supraIliaco = double.Parse(subscapularing),
                subscapular = double.Parse(subscapularing),
                peso = double.Parse(pesoing),
                estatura = double.Parse(estaturaing),
                IMC = double.Parse(imging),
                porcentajeGrasa = double.Parse(grasaing),
                idMedida = idMedidas
            };
            medidasBLL.Modificar(medidas);
            return Json(new { result = string.Empty }, JsonRequestBehavior.AllowGet);

        }
    }
}
