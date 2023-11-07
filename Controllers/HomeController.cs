﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaveTheCave.Models;

namespace WaveTheCave.Controllers
{
    
    
        [Authorize(Roles = "Admin,User")]
        public class HomeController : Controller
        {
            private static ModelDBContext db = new ModelDBContext();
        public ActionResult Index2()
        {

            ViewBag.Title = "Home Page";

            return View();
        }
        public List<SelectListItem> a
        {
            get
            {
                List<Orari> orari = db.Orari.ToList();
                List<SelectListItem> aList = new List<SelectListItem>();
                foreach (Orari agg in orari)
                {
                    SelectListItem item = new SelectListItem { Text = agg.OrariGrotte, Value = agg.IdOrari.ToString() };
                    aList.Add(item);
                }
                return aList;
            }
        }
        public ActionResult Index()
        {
           
            ViewBag.IdGrotte = new SelectList(db.Grotte, "IdGrotte", "Nome");
            ViewBag.Title = "Home Page";
            ViewBag.IdOrari = a;
            ViewBag.Carrello = Session["Carrello"];
            return View(db.Grotte.ToList());
        }
        public ActionResult AddToCart(int IdGrotte, int Quantita, int IdOrari)
        {
            Grotte g = db.Grotte.Find(IdGrotte);
            Orari o = db.Orari.Find(IdOrari);
            Cart cartItem = new Cart(Quantita, g.Nome, g.Prezzo, IdGrotte, IdOrari, o.OrariGrotte);
            List<Cart> carrello = Session["Carrello"] as List<Cart> ?? new List<Cart>();
            carrello.Add(cartItem);
            Session["Carrello"] = carrello;
            return RedirectToAction("Index");
        }
        public ActionResult Remove(int id)
        {
            List<Cart> carrello = Session["Carrello"] as List<Cart>;
            carrello.RemoveAt(id);
            Session["Carrello"] = carrello;
            return RedirectToAction("Index");
        }

    }
}
