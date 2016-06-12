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

        public ActionResult Index(int supplierId)
        {
            var supplierparts = db.SupplierParts
                                .Where(sp => sp.supplierId == supplierId)
                                .Include(s => s.Part).Include(s => s.Supplier).ToList();

            var supplier = db.Suppliers.Find(supplierId);
            if (supplier != null)
            {
                ViewBag.SupplierName = supplier.supplierName;
                ViewBag.SupplierId = supplier.supplierId;
            }
            return View(supplierparts);
        }

        //
        // GET: /SupplierPart/Details/5

        public ActionResult Details(int supplierId, int partId)
        {
            SupplierPart supplierpart = db.SupplierParts.Where(sp => sp.partId == partId && sp.supplierId == supplierId).FirstOrDefault();
            if (supplierpart == null)
            {
                return HttpNotFound();
            }
            return View(supplierpart);
        }

        //
        // GET: /SupplierPart/Create

        public ActionResult Create(int supplierId)
        {
            var model = new SupplierPart() { supplierId = supplierId };

            ViewBag.partId = new SelectList(
                db.Parts.Where(pa => !db.SupplierParts.Any(sp => sp.supplierId == supplierId && sp.partId == pa.partId))
                , "partId", "partName");

            ViewBag.supplierId = new SelectList(
                db.Suppliers.Where(su => su.supplierId == supplierId), "supplierId", "supplierName", supplierId);
            return View(model);
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

                //Find the Part
                var part = db.Parts.Find(supplierpart.partId);

                //Obtain the average price
                var average = (decimal)db.SupplierParts.Where(sp => sp.partId == supplierpart.partId).Average(sp => sp.price);

                //Verify if the difference between reference price and the average of the suppliers prices exceed the ten percent
                var tenPercent = (decimal)(part.partPrice * 0.1m);
                if (Math.Abs((decimal)(part.partPrice - average)) > tenPercent)
                {
                    var referencePrice = part.partPrice;
                    part.partPrice = Math.Round(average, 2);

                    return RedirectToAction("UpdatePartPrice",
                        new
                        {
                            part = new Part() { partId = part.partId, partName = part.partName, partDescription = part.partDescription, partPrice = part.partPrice },
                            supplierId = supplierpart.supplierId,
                            partPrice = referencePrice
                        });
                }

                //Set the average price
                //part.partPrice = db.SupplierParts.Where(sp => sp.partId == supplierpart.partId).Average(sp => sp.price);
                //db.Entry(part).State = EntityState.Modified;
                //db.SaveChanges();

                return RedirectToAction("Index", new { supplierId = supplierpart.supplierId });
            }

            ViewBag.partId = new SelectList(
                db.Parts.Where(pa => !db.SupplierParts.Any(sp => sp.supplierId == supplierpart.supplierId && sp.partId == pa.partId))
                , "partId", "partName", supplierpart.partId);

            ViewBag.supplierId = new SelectList(
                db.Suppliers.Where(su => su.supplierId == supplierpart.supplierId), "supplierId", "supplierName", supplierpart.supplierId);

            return View(supplierpart);
        }

        public ActionResult UpdatePartPrice(Part part, int supplierId, decimal partPrice)
        {
            ViewBag.PartPrice = partPrice;
            ViewBag.supplierId = supplierId;
            return View(part);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePartPrice(Part part)
        {
            if (ModelState.IsValid)
            {
                db.Entry(part).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(part);
        }

        //
        // GET: /SupplierPart/Edit/5

        public ActionResult Edit(int supplierId, int partId)
        {
            SupplierPart supplierpart = db.SupplierParts.Where(sp => sp.partId == partId && sp.supplierId == supplierId).FirstOrDefault();
            if (supplierpart == null)
            {
                return HttpNotFound();
            }

            ViewBag.partId = new SelectList(
                db.Parts.Where(pa => pa.partId == partId || !db.SupplierParts.Any(sp => sp.supplierId == supplierpart.supplierId && sp.partId == pa.partId))
                , "partId", "partName", supplierpart.partId);

            ViewBag.supplierId = new SelectList(
                db.Suppliers.Where(su => su.supplierId == supplierpart.supplierId), "supplierId", "supplierName", supplierpart.supplierId);

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
                return RedirectToAction("Index", new { SupplierId = supplierpart.supplierId });
            }

            ViewBag.partId = new SelectList(
                db.Parts.Where(pa => pa.partId == supplierpart.partId || !db.SupplierParts.Any(sp => sp.supplierId == supplierpart.supplierId && sp.partId == pa.partId))
                , "partId", "partName", supplierpart.partId);

            ViewBag.supplierId = new SelectList(
                db.Suppliers.Where(su => su.supplierId == supplierpart.supplierId), "supplierId", "supplierName", supplierpart.supplierId);

            return View(supplierpart);
        }

        //
        // GET: /SupplierPart/Delete/5

        public ActionResult Delete(int supplierId, int partId)
        {
            SupplierPart supplierpart = db.SupplierParts.Where(sp => sp.partId == partId && sp.supplierId == supplierId).FirstOrDefault();
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
        public ActionResult DeleteConfirmed(int supplierId, int partId)
        {
            SupplierPart supplierpart = db.SupplierParts.Where(sp => sp.partId == partId && sp.supplierId == supplierId).FirstOrDefault();
            db.SupplierParts.Remove(supplierpart);
            db.SaveChanges();
            return RedirectToAction("Index", new { SupplierId = supplierpart.supplierId });
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}