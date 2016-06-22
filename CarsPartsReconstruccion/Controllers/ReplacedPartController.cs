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
    public class ReplacedPartController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /ReplacedPart/

        public ActionResult Index()
        {
            var replacedparts = db.ReplacedParts.Include(r => r.Catalog).Include(r => r.ServicePart).Include(r => r.SupplierPart).Include(r => r.ServiceCar);
            return View(replacedparts.ToList());
        }

        public ActionResult ReplacedParts(int serviceCarId)
        {
            var replacedparts = db.ReplacedParts.Where(rp => rp.serviceCarId == serviceCarId)
                .Include(r => r.Catalog).Include(r => r.ServicePart).Include(r => r.SupplierPart).Include(r => r.ServiceCar);

            return PartialView("_ReplacedPart", replacedparts.ToList());
        }

        //
        // GET: /ReplacedPart/Details/5

        public ActionResult Details(int id = 0)
        {
            ReplacedPart replacedpart = db.ReplacedParts.Find(id);
            if (replacedpart == null)
            {
                return HttpNotFound();
            }
            return View(replacedpart);
        }

        //
        // GET: /ReplacedPart/Create

        public ActionResult Create()
        {
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue");
            ViewBag.servicePartId = new SelectList(db.ServiceParts, "servicePartId", "servicePartDescription");
            ViewBag.supplierId = new SelectList(db.SupplierParts, "supplierId", "supplierId");
            ViewBag.serviceCarId = new SelectList(db.ServiceCars, "serviceCarId", "serviceCarDescription");
            return View();
        }

        //
        // POST: /ReplacedPart/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReplacedPart replacedpart)
        {
            if (ModelState.IsValid)
            {
                db.ReplacedParts.Add(replacedpart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", replacedpart.statusId);
            ViewBag.servicePartId = new SelectList(db.ServiceParts, "servicePartId", "servicePartDescription", replacedpart.servicePartId);
            ViewBag.supplierId = new SelectList(db.SupplierParts, "supplierId", "supplierId", replacedpart.supplierId);
            ViewBag.serviceCarId = new SelectList(db.ServiceCars, "serviceCarId", "serviceCarDescription", replacedpart.serviceCarId);
            return View(replacedpart);
        }

        //
        // GET: /ReplacedPart/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ReplacedPart replacedpart = db.ReplacedParts.Find(id);
            if (replacedpart == null)
            {
                return HttpNotFound();
            }
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", replacedpart.statusId);
            ViewBag.servicePartId = new SelectList(db.ServiceParts, "servicePartId", "servicePartDescription", replacedpart.servicePartId);
            ViewBag.supplierId = new SelectList(db.SupplierParts, "supplierId", "supplierId", replacedpart.supplierId);
            ViewBag.serviceCarId = new SelectList(db.ServiceCars, "serviceCarId", "serviceCarDescription", replacedpart.serviceCarId);
            return View(replacedpart);
        }

        //
        // POST: /ReplacedPart/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReplacedPart replacedpart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(replacedpart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", replacedpart.statusId);
            ViewBag.servicePartId = new SelectList(db.ServiceParts, "servicePartId", "servicePartDescription", replacedpart.servicePartId);
            ViewBag.supplierId = new SelectList(db.SupplierParts, "supplierId", "supplierId", replacedpart.supplierId);
            ViewBag.serviceCarId = new SelectList(db.ServiceCars, "serviceCarId", "serviceCarDescription", replacedpart.serviceCarId);
            return View(replacedpart);
        }

        //
        // GET: /ReplacedPart/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ReplacedPart replacedpart = db.ReplacedParts.Find(id);
            if (replacedpart == null)
            {
                return HttpNotFound();
            }
            return View(replacedpart);
        }

        //
        // POST: /ReplacedPart/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReplacedPart replacedpart = db.ReplacedParts.Find(id);
            db.ReplacedParts.Remove(replacedpart);
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