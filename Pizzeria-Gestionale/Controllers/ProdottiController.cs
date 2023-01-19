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
    public class ProdottiController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        //[Authorize]
        // GET: Prodotti
        public ActionResult Index()
        {
            return View(db.Prodotti.ToList());
        }

        public ActionResult Menu()
        {
            return View(db.Prodotti.ToList());
        }


        // GET: Prodotti/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // GET: Prodotti/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prodotti/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Prodotti prodotti, HttpPostedFileBase Upload)
        {
            if (ModelState.IsValid)
            {
                if (Upload != null)
                {
                    string Path = Server.MapPath("/Content/img/" + Upload.FileName);
                    Upload.SaveAs(Path);
                    prodotti.Foto = Upload.FileName;
                }
                else { prodotti.Foto = "NonDisponibile.png"; }

                

                db.Prodotti.Add(prodotti);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(prodotti);
        }

        // GET: Prodotti/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Prodotti prodotti, HttpPostedFileBase Upload)
        {
            if (Upload != null)
            {
                string Path = Server.MapPath("/Content/img/" + Upload.FileName);
                Upload.SaveAs(Path);
                prodotti.Foto = Upload.FileName;
            }
            else {
                Prodotti p = db.Prodotti.Find(prodotti.IdProdotto);
                prodotti.Foto = p.Foto;
            }
            ModelDbContext db1 = new ModelDbContext();
            db1.Entry(prodotti).State = EntityState.Modified;
            db1.SaveChanges();

            return RedirectToAction("Index");

        }

        // GET: Prodotti/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prodotti prodotti = db.Prodotti.Find(id);
            if (prodotti == null)
            {
                return HttpNotFound();
            }
            return View(prodotti);
        }

        // POST: Prodotti/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prodotti prodotti = db.Prodotti.Find(id);
            db.Prodotti.Remove(prodotti);
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
