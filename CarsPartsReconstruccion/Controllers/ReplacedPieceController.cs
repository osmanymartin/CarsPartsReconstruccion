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
    public class ReplacedPieceController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /ReplacedPiece/

        public ActionResult Index()
        {
            var replacedpieces = db.ReplacedPieces
                .Include(r => r.ServicePart).Include(r => r.SupplierPiece).Include(r => r.Catalog);

            return View(replacedpieces.ToList());
        }

        public ActionResult ReplacedPieces(int servicePartId)
        {
            var replacedpieces = db.ReplacedPieces.Where(rp => rp.servicePartId == servicePartId)
                .Include(r => r.ServicePart).Include(r => r.SupplierPiece).Include(r => r.Catalog);

            return PartialView("_ReplacedPiece", replacedpieces.ToList());
        }

        //
        // GET: /ReplacedPiece/Details/5

        public ActionResult Details(int id = 0)
        {
            ReplacedPiece replacedpiece = db.ReplacedPieces.Find(id);
            if (replacedpiece == null)
            {
                return HttpNotFound();
            }
            return View(replacedpiece);
        }

        //
        // GET: /ReplacedPiece/Create

        public ActionResult Create()
        {
            ViewBag.servicePartId = new SelectList(db.ServiceParts, "servicePartId", "servicePartDescription");
            ViewBag.supplierPieceId = new SelectList(db.SupplierPieces, "supplierPieceId", "supplierPieceId");
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue");
            return View();
        }

        //
        // POST: /ReplacedPiece/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReplacedPiece replacedpiece)
        {
            if (ModelState.IsValid)
            {
                db.ReplacedPieces.Add(replacedpiece);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.servicePartId = new SelectList(db.ServiceParts, "servicePartId", "servicePartDescription", replacedpiece.servicePartId);
            ViewBag.supplierPieceId = new SelectList(db.SupplierPieces, "supplierPieceId", "supplierPieceId", replacedpiece.supplierPieceId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", replacedpiece.statusId);
            return View(replacedpiece);
        }

        //
        // GET: /ReplacedPiece/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ReplacedPiece replacedpiece = db.ReplacedPieces.Find(id);
            if (replacedpiece == null)
            {
                return HttpNotFound();
            }
            ViewBag.servicePartId = new SelectList(db.ServiceParts, "servicePartId", "servicePartDescription", replacedpiece.servicePartId);
            ViewBag.supplierPieceId = new SelectList(db.SupplierPieces, "supplierPieceId", "supplierPieceId", replacedpiece.supplierPieceId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", replacedpiece.statusId);
            return View(replacedpiece);
        }

        //
        // POST: /ReplacedPiece/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReplacedPiece replacedpiece)
        {
            if (ModelState.IsValid)
            {
                db.Entry(replacedpiece).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.servicePartId = new SelectList(db.ServiceParts, "servicePartId", "servicePartDescription", replacedpiece.servicePartId);
            ViewBag.supplierPieceId = new SelectList(db.SupplierPieces, "supplierPieceId", "supplierPieceId", replacedpiece.supplierPieceId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", replacedpiece.statusId);
            return View(replacedpiece);
        }

        //
        // GET: /ReplacedPiece/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ReplacedPiece replacedpiece = db.ReplacedPieces.Find(id);
            if (replacedpiece == null)
            {
                return HttpNotFound();
            }
            return View(replacedpiece);
        }

        //
        // POST: /ReplacedPiece/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReplacedPiece replacedpiece = db.ReplacedPieces.Find(id);
            db.ReplacedPieces.Remove(replacedpiece);
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