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
        public ActionResult ResocontoDetPrenotazione()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.IdGrotte = new SelectList(db.Grotte, "IdGrotte", "Nome");
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetPrenotazione,Quantita, IdGrotte")] DetPrenotazione dp)
        {
            if (ModelState.IsValid)
            {
                db.DetPrenotazione.Add(dp);
                db.SaveChanges();
                return RedirectToAction("ResocontoDetPrenotazione", "DetPrenotazione");
            }

            return View(dp);
        }
    }
}