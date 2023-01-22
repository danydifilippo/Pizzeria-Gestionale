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
    public class OrdiniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        [Authorize(Roles = "Admin")]
        // GET: Ordini
        public ActionResult Index()
        {
                      
            return View(db.Ordini.ToList());
        }

        public ActionResult Lista()
        {
            Utenti u = db.Utenti.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            return View(db.Ordini.Include(x=>x.Utenti).Where(x => x.IdUtente == u.IdUtente).ToList());
        }

        // GET: Ordinis/Details/5
        public ActionResult Details(int? id)
        {
            List<Articoli_Ordine> o = db.Articoli_Ordine.Where(x => x.IdOrdine == id).ToList();
            return View(o);
        }

        // GET: Ordinis/Create
        public ActionResult Create()
        {
            Ordini o = new Ordini();
            Utenti u = db.Utenti.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<Articoli_Ordine> articoli = db.Articoli_Ordine.Where(x => x.IdOrdine == null && x.IdUtente == u.IdUtente).ToList();
            o.Totale = articoli.Sum(x => x.Prezzo_Tot);
            int nr = articoli.Sum(x => x.Quantita);
            ViewBag.totPizze = nr;
            o.IdUtente = u.IdUtente;
            o.Articoli_Ordine = articoli;
            o.Utenti = u;


            return View(o);
        }

        // POST: Ordinis/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ordini o)
        {
            
            Utenti u = db.Utenti.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<Articoli_Ordine> articoli = db.Articoli_Ordine.Where(x => x.IdOrdine == null && x.IdUtente == u.IdUtente).ToList();
            
            o.Totale = articoli.Sum(x => x.Prezzo_Tot);
            o.StatoOrdine = "Confermato";
                db.Ordini.Add(o);
                db.SaveChanges();
            int id = o.IdOrdine;
            
            foreach(var a in articoli)
            {
                a.IdOrdine = id;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
            }
            
            return RedirectToAction("Lista");
        }

        [Authorize(Roles="Admin")]
        // GET: Ordinis/Edit/5
        public ActionResult Edit(int? id)
        {
            List<SelectListItem> listaStato = new List<SelectListItem>();

            SelectListItem LS1 = new SelectListItem { Text = "Confermato", Value = "Confermato" };
            SelectListItem LS2 = new SelectListItem { Text = "Spedito", Value = "Spedito" };
            SelectListItem LS3 = new SelectListItem { Text = "Evaso", Value = "Evaso" };

            listaStato.Add(LS1);
            listaStato.Add(LS2);
            listaStato.Add(LS3);

            ViewBag.Stato = listaStato;

            return View(db.Ordini.Find(id));
        }

        // POST: Ordinis/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ordini ordini)
        {
        

                db.Entry(ordini).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

        }

        // GET: Ordinis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordini ordini = db.Ordini.Include(x=>x.Articoli_Ordine).Include(x => x.Utenti).Where(x=>x.IdOrdine == id).FirstOrDefault();
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
