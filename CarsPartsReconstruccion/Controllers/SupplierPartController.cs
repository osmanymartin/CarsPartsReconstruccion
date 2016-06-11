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
    public class SupplierPartController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /SupplierPart/

        public ActionResult Index()
        {
            var supplierparts = db.SupplierParts.Include(s => s.Part).Include(s => s.Supplier);
            return View(supplierparts.ToList());
        }

        //
        // GET: /SupplierPart/Details/5

        public ActionResult Details(int idSupplier, int idPart)
        {
            SupplierPart supplierpart = db.SupplierParts.Where(sp => sp.partId == idPart && sp.supplierId == idSupplier).FirstOrDefault();
            if (supplierpart == null)
            {
                return HttpNotFound();
            }
            return View(supplierpart);
        }

        //
        // GET: /SupplierPart/Create

        public ActionResult Create()
        {
            ViewBag.partId = new SelectList(db.Parts, "partId", "partName");
            ViewBag.supplierId = new SelectList(db.Suppliers, "supplierId", "supplierName");
            return View();
        }

        //
        // POST: /SupplierPart/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SupplierPart supplierpart)
        {
            if (ModelState.IsValid)
            {
                db.SupplierParts.Add(supplierpart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.partId = new SelectList(db.Parts, "partId", "partName", supplierpart.partId);
            ViewBag.supplierId = new SelectList(db.Suppliers, "supplierId", "supplierName", supplierpart.supplierId);
            return View(supplierpart);
        }

        //
        // GET: /SupplierPart/Edit/5

        public ActionResult Edit(int idSupplier, int idPart)
        {
            SupplierPart supplierpart = db.SupplierParts.Where(sp => sp.partId == idPart && sp.supplierId == idSupplier).FirstOrDefault();
            if (supplierpart == null)
            {
                return HttpNotFound();
            }
            ViewBag.partId = new SelectList(db.Parts, "partId", "partName", supplierpart.partId);
            ViewBag.supplierId = new SelectList(db.Suppliers, "supplierId", "supplierName", supplierpart.supplierId);
            return View(supplierpart);
        }

        //
        // POST: /SupplierPart/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SupplierPart supplierpart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplierpart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.partId = new SelectList(db.Parts, "partId", "partName", supplierpart.partId);
            ViewBag.supplierId = new SelectList(db.Suppliers, "supplierId", "supplierName", supplierpart.supplierId);
            return View(supplierpart);
        }

        //
        // GET: /SupplierPart/Delete/5

        public ActionResult Delete(int idSupplier, int idPart)
        {
            SupplierPart supplierpart = db.SupplierParts.Where(sp => sp.partId == idPart && sp.supplierId == idSupplier).FirstOrDefault();
            if (supplierpart == null)
            {
                return HttpNotFound();
            }
            return View(supplierpart);
        }

        //
        // POST: /SupplierPart/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int idSupplier, int idPart)
        {
            SupplierPart supplierpart = db.SupplierParts.Where(sp => sp.partId == idPart && sp.supplierId == idSupplier).FirstOrDefault();
            db.SupplierParts.Remove(supplierpart);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}