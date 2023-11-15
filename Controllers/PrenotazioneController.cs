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



        //// GET: Prenotazione
        //public ActionResult AddPrenotazione()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult AddData(Cart c)
        //{
        //    List<Cart> lista = Session["Carrello"] as List<Cart>; 

        //    foreach (Cart item in lista)
        //    {
        //        if (item.IdGrotte == c.IdGrotte)
        //        {
        //            item.Data = c.Data;
        //        }
        //    }
        //  Session["Carrello"] = lista;
        //    return RedirectToAction("AddPrenotazione");
        //}

        //[HttpPost]
        //public ActionResult AddPrenotazione(Prenotazione prenotazione)
        //{
        //    ViewBag.Carrello = Session["Carrello"];
        //    prenotazione.Data = DateTime.Now;


        //    prenotazione.Importo = Cart.CalcoloCostoTotale(ViewBag.Carrello);
        //    User user = db.User.FirstOrDefault(u => u.Username == User.Identity.Name);
        //    prenotazione.IdUser = user.IdUser;

        //   if (ModelState.IsValid)
        //    {


        //        foreach (Cart item in ViewBag.Carrello)
        //        {
        //            Prenotazione d = new Prenotazione(item.Data, item.Importo, item.IdUser, item.IdOrari, item.Quantita, item.IdGrotte);
        //            db.Prenotazione.Add(d);
        //        }
                    
        //        Session.Remove("Carrello");
        //        return RedirectToAction("Index", " Home");
        //    }
        //    else { return View(); }
          
        //}
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
        // GET: Prenotazione/AddPrenotazione
        public ActionResult AddPrenotazione()
        {
            
            ViewBag.Grotte = db.Grotte.ToList();
            ViewBag.Orari = db.Orari.ToList();

            decimal prezzoPrimaGrotta = db.Grotte.FirstOrDefault()?.Prezzo ?? 0;

            // Inizializza un nuovo oggetto Prenotazione con le proprietà necessarie
            Prenotazione modelloPrenotazione = new Prenotazione
            {
                // Assegna valori di default o appropriati alle proprietà necessarie
                Data = DateTime.Now, // Ad esempio, la data odierna
                Quantita = 1, // Imposta una quantità di default
                              // ...

                // Calcola l'importo iniziale sulla base del prezzo della prima grotta disponibile
                Importo = prezzoPrimaGrotta,
            };

            // Imposta il modello nella vista
            return View(modelloPrenotazione);
        }
        [HttpPost]
        public ActionResult AddPrenotazione(Prenotazione prenotazione)
        {
            // Ottenere il prezzo della grotta
            decimal prezzoGrotta = db.Grotte.Find(prenotazione.IdGrotte).Prezzo;

            // Calcolare l'importo totale
            prenotazione.Importo = prenotazione.Quantita * prezzoGrotta;

            // ... altre logiche ...

            // Salva la prenotazione nel database
            Prenotazione nuovaPrenotazione = new Prenotazione
            {
                // Popola gli altri campi della prenotazione
                Data = prenotazione.Data,
                IdGrotte = prenotazione.IdGrotte,
                IdOrari = prenotazione.IdOrari,
                IdUser = prenotazione.IdUser,
                Quantita = prenotazione.Quantita,
                Importo = prenotazione.Importo  // Assegna l'importo calcolato
            };

            db.Prenotazione.Add(nuovaPrenotazione);
            db.SaveChanges();

            return RedirectToAction("ConfermaPrenotazione", new { idPrenotazione = nuovaPrenotazione.IdPrenotazione });
        }
        public ActionResult ConfermaPrenotazione(int idPrenotazione)
        {
            Prenotazione prenotazione = db.Prenotazione.Find(idPrenotazione);

            // Assicurati che la prenotazione non sia null e poi crea una lista contenente solo quella prenotazione
            //List<Prenotazione> listaPrenotazioni = prenotazione != null ? new List<Prenotazione> { prenotazione } : new List<Prenotazione>();

            //// Passa la lista di prenotazioni alla vista ConfermaPrenotazione
            //return View(listaPrenotazioni);
            if (prenotazione == null)
            {
                return HttpNotFound(); // o gestisci l'errore nel modo che preferisci
            }

            // Effettua ulteriori operazioni se necessario, ad esempio, salvare la prenotazione in qualche modo

            // Reindirizza l'utente alla pagina di effettuazione del pagamento con l'ID della prenotazione
            return RedirectToAction("EffettuaPagamento", new { idPrenotazione = prenotazione.IdPrenotazione });
        }
        [HttpGet]
        public ActionResult EffettuaPagamento(int idPrenotazione)
        {
            // Ottieni la prenotazione dal database in base all'idPrenotazione
            //Prenotazione prenotazione = db.Prenotazione.Find(idPrenotazione);
            Prenotazione prenotazione = db.Prenotazione.Include("User").FirstOrDefault(p => p.IdPrenotazione == idPrenotazione);

            if (prenotazione == null)
            {
                // Prenotazione non trovata, gestisci l'errore
                return HttpNotFound();
            }

            // Passa la prenotazione alla vista di pagamento
            return View(prenotazione);
        }

        [HttpPost]
        public ActionResult EffettuaPagamento(int idPrenotazione, DatiPagamentoViewModel datiPagamento)
        {
            // Ottieni la prenotazione dal database in base all'idPrenotazione
            Prenotazione prenotazione = db.Prenotazione.Find(idPrenotazione);

            if (prenotazione == null)
            {
                // Prenotazione non trovata, gestisci l'errore
                return HttpNotFound();
            }

            // Implementa la logica di pagamento qui usando i datiPagamento

            // Esempio: Aggiorna lo stato della prenotazione dopo il pagamento
            prenotazione.StatoPagamento = "Pagato";
            db.Entry(prenotazione).State = EntityState.Modified;
            db.SaveChanges();

          

            return RedirectToAction("Index", "Home");
        }
    }
}