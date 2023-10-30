using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    public class OrariController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Orari
        public ActionResult Index()
        {
            return View(db.Orari.ToList());
        }

        // GET: Orari/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orari orari = db.Orari.Find(id);
            if (orari == null)
            {
                return HttpNotFound();
            }
            return View(orari);
        }

        // GET: Orari/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orari/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrari,OrariGrotte")] Orari orari)
        {
            if (ModelState.IsValid)
            {
                db.Orari.Add(orari);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orari);
        }

        // GET: Orari/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orari orari = db.Orari.Find(id);
            if (orari == null)
            {
                return HttpNotFound();
            }
            return View(orari);
        }

        // POST: Orari/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrari,OrariGrotte")] Orari orari)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orari).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orari);
        }

        // GET: Orari/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orari orari = db.Orari.Find(id);
            if (orari == null)
            {
                return HttpNotFound();
            }
            return View(orari);
        }

        // POST: Orari/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orari orari = db.Orari.Find(id);
            db.Orari.Remove(orari);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
