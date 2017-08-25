using System.Web;
using Gym5BLL.Implementations;
using System.Web.Mvc;
using System.Web.Security;
using Gymm1._0.Clases;

namespace Gymm1._0.Controllers
{
    public class HomeController : Controller
    {
        private ILogEmpresaBLL empBLL;
        private ILogUsuarioBLL userBLL;
        FuncionesBasicas fb = new FuncionesBasicas();

        public ActionResult Login1()
        {
            return View();
        }

        public ActionResult Login2()
        {
            if (Session["empresa"] == null)
            {
                return RedirectToAction("Login1", "Home");
            }
            return View();
        }

        public ActionResult LoginEmp(string user, string pass)
        {
            empBLL = new LogEmpresaBLLImp();
            bool b = empBLL.getUser(user, fb.Encripta(pass));
            Session["empresa"] = user;
            return Json(new { boole = b}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LoginEmp2(string user, string pass)
        {
            userBLL = new LogUsuarioBLLImp();
            string[] respsuesta = new string[2];
            respsuesta = userBLL.getUser(user, fb.Encripta(pass));
            if (respsuesta == null) return Json(new { boole = false }, JsonRequestBehavior.AllowGet);
            else
            {
                Session["usuario"] = respsuesta[0];
                Session["permiso"] = respsuesta[1];
                var authTicket = new FormsAuthenticationTicket(respsuesta[0], true, 100000);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName,
                                            FormsAuthentication.Encrypt(authTicket));
                Response.Cookies.Add(cookie);
                return Json(new { boole = true }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            Session.Contents.RemoveAll();
            return Json(new { boole = true }, JsonRequestBehavior.AllowGet);
        }
    }
}