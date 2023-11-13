using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
            return View();
        }
        [HttpPost]
        public ActionResult AddData(Cart c)
        {
            List<Cart> lista = Session["Carrello"] as List<Cart>; 

            foreach (Cart item in lista)
            {
                if (item.IdGrotte == c.IdGrotte)
                {
                    item.Data = c.Data;
                }
            }
          Session["Carrello"] = lista;
            return RedirectToAction("AddPrenotazione");
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
                    Prenotazione d = new Prenotazione(item.Data, item.Importo, item.IdUser);
                    db.Prenotazione.Add(d);
                }
                    
                Session.Remove("Carrello");
                return RedirectToAction("Index", " Home");
            }
            else { return View(); }
          
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Prenotazione.ToList());
        }

       public ActionResult Pagamento()
        { 
        return View();
                }
        public ActionResult ResocontoPrenotazione( string IdUser, string IdOrari)
        {
            var prenotazione = db.Prenotazione.Include(Prenotazione => Prenotazione.IdUser).Include(Prenotazione => Prenotazione.IdOrari);
            ViewData["IdUser"] = new SelectList(db.User, "Nome", "Nome");
            ViewData["IdOrari"] = new SelectList(db.Orari, "OrariGrotte", "OrariGrotte");
            return View(db.Prenotazione.ToList());
        }
        public ActionResult CreateResocontoPrenotazione()
        {
            ViewBag.IdUser = new SelectList(db.User, "IdUser", "Nome");
            ViewBag.IdOrari = new SelectList(db.Orari, "IdOrari", "OrariGrotte");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrenotazione,Data, Importo, IdOrari, IdUser")] Prenotazione p)
        {
            if (ModelState.IsValid)
            {
                db.Prenotazione.Add(p);
                db.SaveChanges();
                return RedirectToAction("ResocontoPrenotazione","Prenotazione");
            }

            return View(p);
        }
    }
}