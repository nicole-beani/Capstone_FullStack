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
                    db.SaveChanges();
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

       
        
    }
}