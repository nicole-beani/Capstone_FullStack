using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class PrenotazioneController : Controller
    {
        private ModelDBContext db = new ModelDBContext();



       
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View(db.Prenotazione.ToList());
        }

      
       
        
        // GET: Prenotazione/AddPrenotazione
        public ActionResult AddPrenotazione()
        {
            
            ViewBag.Grotte = db.Grotte.ToList();
            ViewBag.Orari = db.Orari.ToList();

            decimal prezzoPrimaGrotta = db.Grotte.FirstOrDefault()?.Prezzo ?? 0;

          
            Prenotazione modelloPrenotazione = new Prenotazione
            {
               
                Data = DateTime.Now, 
                Quantita = 1, 
                Importo = prezzoPrimaGrotta,
            };

          
            return View(modelloPrenotazione);
        }
        [HttpPost]
        public ActionResult AddPrenotazione(Prenotazione prenotazione)
        {
           
            decimal prezzoGrotta = db.Grotte.Find(prenotazione.IdGrotte).Prezzo;

         
            prenotazione.Importo = prenotazione.Quantita * prezzoGrotta;

         
            Prenotazione nuovaPrenotazione = new Prenotazione
            {
                
                Data = prenotazione.Data,
                IdGrotte = prenotazione.IdGrotte,
                IdOrari = prenotazione.IdOrari,
                IdUser = prenotazione.IdUser,
                Quantita = prenotazione.Quantita,
                Importo = prenotazione.Importo 
            };

            db.Prenotazione.Add(nuovaPrenotazione);
            db.SaveChanges();

            return RedirectToAction("ConfermaPrenotazione", new { idPrenotazione = nuovaPrenotazione.IdPrenotazione });
        }
        public ActionResult ConfermaPrenotazione(int idPrenotazione)
        {
            Prenotazione prenotazione = db.Prenotazione.Find(idPrenotazione);

            
            if (prenotazione == null)
            {
                return HttpNotFound(); 
            }

           
            return RedirectToAction("EffettuaPagamento", new { idPrenotazione = prenotazione.IdPrenotazione });
        }
        [HttpGet]
        public ActionResult EffettuaPagamento(int idPrenotazione)
        {
            
            Prenotazione prenotazione = db.Prenotazione.Include("User").FirstOrDefault(p => p.IdPrenotazione == idPrenotazione);

            if (prenotazione == null)
            {
               
                return HttpNotFound();
            }

         
            return View(prenotazione);
        }

        [HttpPost]
        public ActionResult EffettuaPagamento(int idPrenotazione, DatiPagamentoViewModel datiPagamento)
        {
            Prenotazione prenotazione = db.Prenotazione.Find(idPrenotazione);

            if (prenotazione == null)
            {
                return HttpNotFound();
            }

            prenotazione.StatoPagamento = "Pagato";
            db.Entry(prenotazione).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
        public ActionResult TutteLePrenotazioni()
        {
            var tutteLePrenotazioni = db.Prenotazione.ToList();
            return View(tutteLePrenotazioni);
        }
    }
}