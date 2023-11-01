using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    public class DetPrenotazioneController : Controller
    {
        private ModelDBContext db = new ModelDBContext();
        // GET: DetPrenotazione
        public ActionResult Index()
        {
            User user = db.User.FirstOrDefault(u => u.Username == User.Identity.Name);
            Prenotazione prenotazione = db.Prenotazione.FirstOrDefault(x => x.IdUser == user.IdUser);
            if (prenotazione == null)
            {
                ViewBag.Error = "Non ci sono";
            }
            else
            {
                List<DetPrenotazione> details = db.DetPrenotazione.Where(x => x.IdPrenotazione == prenotazione.IdPrenotazione).ToList();
                ViewBag.Prenotazione = details[0].Prenotazione;
                return View(details);
            }
            return View();
        }
    }
}