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
                    Prenotazione d = new Prenotazione(item.Data, item.Importo, item.IdUser, item.IdOrari, item.Quantita, item.IdGrotte);
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
        public ActionResult ResocontoPrenotazione( string IdUser, string IdOrari, string IdGrotte)
        {
            var prenotazione = db.Prenotazione.Include(Prenotazione => Prenotazione.IdUser).Include(Prenotazione => Prenotazione.IdOrari).Include(Prenotazione => Prenotazione.IdGrotte).ToList();
            ViewData["IdUser"] = new SelectList(db.User, "IdUser", "Nome");
            ViewData["IdOrari"] = new SelectList(db.Orari, "IdOrari", "OrariGrotte");
            ViewData["IdGrotte"] = new SelectList(db.Grotte, "IdGrotte", "Nome");
            return View(db.Prenotazione.ToList());
        }
        public ActionResult CreateResocontoPrenotazione()
        {
            ViewBag.IdUser = new SelectList(db.User, "IdUser", "Nome");
            ViewBag.IdOrari = new SelectList(db.Orari, "IdOrari", "OrariGrotte");
            ViewBag.IdGrotte = new SelectList(db.Grotte, "IdGrotte", "Nome");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrenotazione,Data, Importo,Quantita,IdGrotte, IdOrari, IdUser")] Prenotazione p)
        {
            if (ModelState.IsValid)
            {
                db.Prenotazione.Add(p);
                db.SaveChanges();
                return RedirectToAction("ResocontoPrenotazione","Prenotazione");
            }

            return View(p);
        }

        [HttpPost]
        public ActionResult AggiungiAlCarrello(int idGrotte, int quantita)
        {
            List<Cart> carrello = Session["Carrello"] as List<Cart> ?? new List<Cart>();

            // Recupera i dettagli della grotta dal database
            Grotte grotta = db.Grotte.Find(idGrotte);

            if (grotta != null)
            {
                // Ottieni la lista degli orari disponibili
                var orariDisponibili = db.Orari.Where(o => o.GrotteId == idGrotte)  // Modifica questa parte per adattarla alla tua relazione
                                                .Select(o => new SelectListItem
                                                {
                                                    Value = o.IdOrari.ToString(),
                                                    Text = o.OrariGrotte
                                                })
                                                .ToList();

                // Aggiungi al carrello
                carrello.Add(new Cart
                {
                    Quantita = quantita,
                    Nome = grotta.Nome,
                    CostoGrotta = grotta.Prezzo,
                    IdGrotte = grotta.IdGrotte,
                    OrariDisponibili = orariDisponibili,
                });

                Session["Carrello"] = carrello;
            }

            return RedirectToAction("Carrello");
        }



    }
}