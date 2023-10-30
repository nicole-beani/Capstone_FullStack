using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    public class AuthController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User u)
        {
            User user = db.User.SingleOrDefault(x => x.Username == u.Username);
            if (user.Username != null && user.Password == u.Password)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Index", "Home");
            }
            else return View();
        }

        public ActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignIn(User u)
        {
            if (ModelState.IsValid)
            {
                User user = db.User.SingleOrDefault(x => x.Username == u.Username);
                if (user == null)
                {
                    u.Ruolo = "User";
                    db.User.Add(u);
                    db.SaveChanges();
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Già utilizzato";
                    return View();
                }
            }
            else return View();
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Remove("Carrello");
            return RedirectToAction("Login", "Auth");
        }
    }
}