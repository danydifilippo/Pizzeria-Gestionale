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

    [Authorize]
    public class Articoli_OrdineController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        [AllowAnonymous]

        // GET: Articoli_Ordine
        public ActionResult Index(int id, int quantity, string note)
        {

            if (User.Identity.IsAuthenticated)
            {
                Utenti u = db.Utenti.Where(x => x.Username == User.Identity.Name).FirstOrDefault();

                if (id > 0)
                {

                    Articoli_Ordine a = new Articoli_Ordine();
                    a.IdProdotto = id;
                    a.IdUtente = u.IdUtente;
                    a.Quantita = quantity;
                    Prodotti p = db.Prodotti.Find(id);
                    a.Prezzo_Tot = p.Prezzo * a.Quantita;
                    a.Note = note;

                    db.Articoli_Ordine.Add(a);
                    db.SaveChanges();

                }
                return View(db.Articoli_Ordine.Where(x => x.IdOrdine == null && x.IdUtente == u.IdUtente).ToList());
            }
            else { return Redirect("/Utenti/Login"); }
            
        }

        public ActionResult cart()
        {
            Utenti u = db.Utenti.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<Articoli_Ordine> a = db.Articoli_Ordine.Include(x => x.Prodotti).Where(x => x.IdUtente == u.IdUtente).ToList();

            foreach(Articoli_Ordine item in a)
            {
                if(item.IdOrdine == null)
                {
                    return View(db.Articoli_Ordine.Include(x => x.Prodotti).Where(x => x.IdOrdine == null && x.IdUtente == u.IdUtente).ToList());

                }
            }
            ViewBag.msgEmpty = "Carrello vuoto";
            return View();
            

        }



        // GET: Articoli_Ordine/Create
        public ActionResult Create()
        {
            ViewBag.IdOrdine = new SelectList(db.Ordini, "IdOrdine", "StatoOrdine");
            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "NomeProdotto");
            return View();
        }

        // POST: Articoli_Ordine/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDettaglio,IdProdotto,Quantita,Prezzo_Tot,Note,IdOrdine")] Articoli_Ordine articoli_Ordine)
        {
            if (ModelState.IsValid)
            {
                db.Articoli_Ordine.Add(articoli_Ordine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOrdine = new SelectList(db.Ordini, "IdOrdine", "StatoOrdine", articoli_Ordine.IdOrdine);
            ViewBag.IdProdotto = new SelectList(db.Prodotti, "IdProdotto", "NomeProdotto", articoli_Ordine.IdProdotto);
            return View(articoli_Ordine);
        }

        // GET: Articoli_Ordine/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articoli_Ordine articoli_Ordine = db.Articoli_Ordine.Find(id);
            if (articoli_Ordine == null)
            {
                return HttpNotFound();
            }
           
            return View(articoli_Ordine);
        }

        // POST: Articoli_Ordine/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Articoli_Ordine d)
        {
            if (ModelState.IsValid)
            {
                Articoli_Ordine dettaglio = db.Articoli_Ordine.Find(d.IdDettaglio);
                d.Prezzo_Tot = dettaglio.Prodotti.Prezzo * d.Quantita;
                ModelDbContext db1 = new ModelDbContext();
                db1.Entry(d).State = EntityState.Modified;
                db1.SaveChanges();
                
            }

            return RedirectToAction("Cart");
        }

        // GET: Articoli_Ordine/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Articoli_Ordine articoli_Ordine = db.Articoli_Ordine.Find(id);
            if (articoli_Ordine == null)
            {
                return HttpNotFound();
            }
            return View(articoli_Ordine);
        }

        // POST: Articoli_Ordine/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Articoli_Ordine articoli_Ordine = db.Articoli_Ordine.Find(id);
            db.Articoli_Ordine.Remove(articoli_Ordine);
            db.SaveChanges();
            return RedirectToAction("Cart");
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

