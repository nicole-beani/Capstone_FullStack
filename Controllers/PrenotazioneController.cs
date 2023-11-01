using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PrenotazioneController : Controller
    {
        private ModelDBContext db = new ModelDBContext();



        // GET: Prenotazione
        public ActionResult AddPrenotazione()
        {
            ViewBag.Carrello = Session["Carrello"];
          
            return View();
        }

        [HttpPost]
        public ActionResult AddPrenotazione(Prenotazione prenotazione)
        {
            ViewBag.Carrello = Session["Carrello"];
            prenotazione.Data = DateTime.Now;


            prenotazione.Importo = Cart.CalcoloCostoTotale(ViewBag.Carrello);
            User user = db.User.FirstOrDefault(u => u.Username == User.Identity.Name);
            prenotazione.IdUser = user.IdUser;

            if (ModelState.IsValid)
            {


                foreach (Cart item in ViewBag.Carrello)
                {
                    Prenotazione d = new Prenotazione(item.Data, item.Importo, item.IdOrari, item.IdUser);
                    db.Prenotazione.Add(d);
                    db.SaveChanges();
                }
                Session.Remove("Carrello");
                return RedirectToAction("Index", "DetPrenotazione");
            }
            else { return View(); }
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Prenotazione.ToList());
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(db.Prenotazione.Find(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Prenotazione p)
        {
            if (ModelState.IsValid)
            {
                ModelDBContext db1 = new ModelDBContext();
                db1.Entry(p).State = EntityState.Modified;
                db1.SaveChanges();
                return RedirectToAction("Index", "Prenotazione");
            }
            return View(p);
        }
    }
}