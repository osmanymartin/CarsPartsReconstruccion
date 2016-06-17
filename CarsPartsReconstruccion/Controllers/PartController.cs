using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsPartsReconstruccion.Models;
using System.Configuration;

namespace CarsPartsReconstruccion.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class PartController : Controller
    {
        int CarPartReconstructionId = Convert.ToInt32(ConfigurationManager.AppSettings["CarPartReconstructionId"]);
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /Part/
        public ActionResult Index()
        {
            var parts = db.Parts.ToList();

            var partsAvg = parts.Select(part =>
            {
                part.AverageSuppliersPrice = db.SupplierParts.Where(sp => sp.partId == part.partId && sp.supplierId != CarPartReconstructionId).Average(spa => (decimal?)spa.price); return part;
            }).ToList();

            return View(partsAvg);
        }

        //
        // GET: /Part/Details/5

        public ActionResult Details(int id = 0)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        //
        // GET: /Part/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Part/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Part part)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(part);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(part);
        }

        //
        // GET: /Part/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Part part = db.Parts.Find(id);
            part.AverageSuppliersPrice = db.SupplierParts.Where(sp => sp.partId == part.partId && sp.supplierId != CarPartReconstructionId).Average(spa => (decimal?)spa.price);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        //
        // POST: /Part/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Part part)
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
        // GET: /Part/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Part part = db.Parts.Find(id);
            if (part == null)
            {
                return HttpNotFound();
            }
            return View(part);
        }

        //
        // POST: /Part/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Part part = db.Parts.Find(id);
            db.Parts.Remove(part);
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