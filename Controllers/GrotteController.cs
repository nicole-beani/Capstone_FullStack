using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    public class GrotteController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Grotte
        public ActionResult Index()
        {
            return View(db.Grotte.ToList());
        }
        public ActionResult LeGrotte()
        {
            return View();
        }
        public ActionResult GrottedelVento()
        {
            return View();
        }
        public ActionResult DetailsGrottedelVento()
        {
            return View();
        }
        public ActionResult GrottediBorgioVerezzi()
        {
            return View();
        }
        public ActionResult DetailsGrottediBorgioVerezzi()
        {
            return View();
        }
        public ActionResult GrottediCastellana()
        {
            return View();
        }
        public ActionResult DetailsGrottediCastellana()
        {
            return View();
        }
        public ActionResult GrottediToirano()
        {
            return View();
        }
        public ActionResult DetailsGrottediToirano()
        {
            return View();
        }
        public ActionResult GrottediPertosaAuletta()
        {
            return View();
        }
        public ActionResult DetailsGrottediPertosaAuletta()
        {
            return View();
        }
        public ActionResult GrottediStiffe()
        {
            return View();
        }
        public ActionResult DetailsGrottediStiffe()
        {
            return View();
        }
        // GET: Grotte/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grotte grotte = db.Grotte.Find(id);
            if (grotte == null)
            {
                return HttpNotFound();
            }
            return View(grotte);
        }

        // GET: Grotte/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grotte/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Grotte g, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {


                if (Foto != null && Foto.ContentLength > 0)
                {
                    string nomeFile = Foto.FileName;
                    string path = Path.Combine(Server.MapPath("~/Content/assets"), nomeFile);
                    Foto.SaveAs(path);
                    g.Foto = nomeFile;
                }
                else
                {
                    g.Foto = "";

                }
                db.Grotte.Add(g);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Error = "Per favore fai le cose per bene";

                return View();
            }

        }

        // GET: Grotte/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grotte grotte = db.Grotte.Find(id);
            if (grotte == null)
            {
                return HttpNotFound();
            }
            return View(grotte);
        }

        // POST: Grotte/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdGrotte,Nome,Descrizione,Foto,Prezzo")] Grotte grotte)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grotte).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grotte);
        }

        // GET: Grotte/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grotte grotte = db.Grotte.Find(id);
            if (grotte == null)
            {
                return HttpNotFound();
            }
            return View(grotte);
        }

        // POST: Grotte/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grotte grotte = db.Grotte.Find(id);
            db.Grotte.Remove(grotte);
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
