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
    public class SupplierController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /Supplier/
        public ActionResult Index()
        {
            var suppliers = db.Suppliers.Include(s => s.Employee);
            return View(suppliers.ToList());
        }

        //
        // GET: /Supplier/Details/5

        public ActionResult Details(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // GET: /Supplier/Create

        public ActionResult Create()
        {
            ViewBag.idRepresentative = new SelectList(db.Employees, "employeeId", "employeeName");
            return View();
        }

        //
        // POST: /Supplier/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.idRepresentative = new SelectList(db.Employees, "employeeId", "employeeName", supplier.idRepresentative);
            return View(supplier);
        }

        //
        // GET: /Supplier/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            ViewBag.idRepresentative = new SelectList(db.Employees, "employeeId", "employeeName", supplier.idRepresentative);
            return View(supplier);
        }

        //
        // POST: /Supplier/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idRepresentative = new SelectList(db.Employees, "employeeId", "employeeName", supplier.idRepresentative);
            return View(supplier);
        }

        //
        // GET: /Supplier/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Supplier supplier = db.Suppliers.Find(id);
            if (supplier == null)
            {
                return HttpNotFound();
            }
            return View(supplier);
        }

        //
        // POST: /Supplier/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(supplier);
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