using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsPartsReconstruccion.Models;

namespace CarsPartsReconstruccion.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class CatalogController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /Catalog/
        public ActionResult Index(string catalogName)
        {
            List<Catalog> CatalogList = db.Catalogs.Include(c => c.Catalog2).ToList();
            List<Catalog> model = new List<Catalog>();

            if (CatalogList != null && CatalogList.Count() > 0)
            {
                List<String> CatalogNameList = CatalogList.Select(c => c.catalogName).Distinct().OrderBy(name => name).ToList();
                ViewBag.CatalogNameList = CatalogNameList;

                if (string.IsNullOrEmpty(catalogName) || !CatalogNameList.Any(c => c == catalogName))
                {
                    catalogName = CatalogNameList.First();
                }
                ViewBag.selectedCatalogName = catalogName;
                model = CatalogList.Where(c => c.catalogName == catalogName || String.IsNullOrEmpty(catalogName)).OrderBy(cat => cat.parentCatalog).ToList();

                ////model = model.Select(cat => { cat.Catalog2 = CatalogList.Find(c => c.catalogId == cat.parentCatalog); return cat; }).ToList();
            }
            else
            {
                ViewBag.CatalogNameList = null;
                ViewBag.selectedCatalogName = null;
            }

            return View(model);
        }

        //
        // GET: /Catalog/Details/5
        public ActionResult Details(int id = 0)
        {
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        //
        // GET: /Catalog/Create
        public ActionResult Create(string catalogName)
        {
            ViewBag.parentCatalog = new SelectList(db.Catalogs.Where(c => c.catalogName != catalogName), "catalogId", "catalogValue");

            var model = new Catalog();
            model.catalogName = catalogName;
            return View(model);
        }

        //
        // POST: /Catalog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                db.Catalogs.Add(catalog);
                db.SaveChanges();
                return RedirectToAction("Index", new { catalogName = catalog.catalogName });
            }

            ViewBag.parentCatalog = new SelectList(db.Catalogs.Where(c => c.catalogName != catalog.catalogName), "catalogId", "catalogValue", catalog.parentCatalog);
            return View(catalog);
        }

        //
        // GET: /Catalog/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            ViewBag.parentCatalog = new SelectList(db.Catalogs.Where(c => c.catalogName != catalog.catalogName), "catalogId", "catalogValue", catalog.parentCatalog);
            return View(catalog);
        }

        //
        // POST: /Catalog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Catalog catalog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(catalog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { catalogName = catalog.catalogName });
            }
            ViewBag.parentCatalog = new SelectList(db.Catalogs.Where(c => c.catalogName != catalog.catalogName), "catalogId", "catalogValue", catalog.parentCatalog);
            return View(catalog);
        }

        //
        // GET: /Catalog/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Catalog catalog = db.Catalogs.Find(id);
            if (catalog == null)
            {
                return HttpNotFound();
            }
            return View(catalog);
        }

        //
        // POST: /Catalog/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Catalog catalog = db.Catalogs.Find(id);
            db.Catalogs.Remove(catalog);
            db.SaveChanges();
            return RedirectToAction("Index", new { catalogName = catalog.catalogName });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}