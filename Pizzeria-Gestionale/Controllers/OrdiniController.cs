using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pizzeria_Gestionale.Model;

namespace Pizzeria_Gestionale.Controllers
{
    public class OrdiniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Ordini
        public ActionResult Index()
        {
            var ordini = db.Ordini.Include(o => o.Articoli_Ordine).Include(o => o.Utenti);
            return View(ordini.ToList());
        }

        // GET: Ordinis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            return View(ordini);
        }

        // GET: Ordinis/Create
        public ActionResult Create()
        {
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Ruolo");
            return View();
        }

        // POST: Ordinis/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdOrdine,Totale,StatoOrdine,IdUtente")] Ordini ordini)
        {
            if (ModelState.IsValid)
            {
                db.Ordini.Add(ordini);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Ruolo", ordini.IdUtente);
            return View(ordini);
        }

        // GET: Ordinis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Ruolo", ordini.IdUtente);
            return View(ordini);
        }

        // POST: Ordinis/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrdine,Totale,StatoOrdine,IdUtente")] Ordini ordini)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordini).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Ruolo", ordini.IdUtente);
            return View(ordini);
        }

        // GET: Ordinis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Find(id);
            if (ordini == null)
            {
                return HttpNotFound();
            }
            return View(ordini);
        }

        // POST: Ordinis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordini ordini = db.Ordini.Find(id);
            db.Ordini.Remove(ordini);
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

// GET: Ordini/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Ordini ordini = db.Ordini.Find(id);
//            if (ordini == null)
//            {
//                return HttpNotFound();
//            }
//            return View(ordini);
//        }

//        // GET: Ordini/Create
//        public ActionResult Create()
//        {
//            ViewBag.IdDettaglio = new SelectList(db.Articoli_Ordine, "IdDettaglio", "Quantita");
//            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Ruolo");
//            return View();
//        }

//        // POST: Ordini/Create
//        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
//        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include = "IdOrdine,Totale,StatoOrdine,IdDettaglio,IdUtente")] Ordini ordini)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Ordini.Add(ordini);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.IdDettaglio = new SelectList(db.Articoli_Ordine, "IdDettaglio", "Quantita", ordini.IdDettaglio);
//            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Ruolo", ordini.IdUtente);
//            return View(ordini);
//        }

//        // GET: Ordini/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Ordini ordini = db.Ordini.Find(id);
//            if (ordini == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.IdDettaglio = new SelectList(db.Articoli_Ordine, "IdDettaglio", "Quantita", ordini.IdDettaglio);
//            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Ruolo", ordini.IdUtente);
//            return View(ordini);
//        }

//        // POST: Ordini/Edit/5
//        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
//        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "IdOrdine,Totale,StatoOrdine,IdDettaglio,IdUtente")] Ordini ordini)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(ordini).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.IdDettaglio = new SelectList(db.Articoli_Ordine, "IdDettaglio", "Quantita", ordini.IdDettaglio);
//            ViewBag.IdUtente = new SelectList(db.Utenti, "IdUtente", "Ruolo", ordini.IdUtente);
//            return View(ordini);
//        }

//        // GET: Ordini/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Ordini ordini = db.Ordini.Find(id);
//            if (ordini == null)
//            {
//                return HttpNotFound();
//            }
//            return View(ordini);
//        }

//        // POST: Ordini/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Ordini ordini = db.Ordini.Find(id);
//            db.Ordini.Remove(ordini);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
