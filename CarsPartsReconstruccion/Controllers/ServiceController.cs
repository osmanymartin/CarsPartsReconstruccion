﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarsPartsReconstruccion.Models;
using System.Configuration;
using PagedList;

namespace CarsPartsReconstruccion.Controllers
{
    public class ServiceController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();


        public ActionResult Autocomplete(string term)
        {
            var model = db.Services.Where(ser => ser.Customer.customerName.Contains(term))
                            .Select(r => new { label = r.Customer.customerName })
                            .Distinct()
                            .Take(10);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Service/

        public ActionResult Index(string searchValue = null, int page = 1)
        {
            IPagedList<Service> model;

            if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
            {
                model = db.Services.Where(se => se.Customer.userLogin == User.Identity.Name).
                    Include(s => s.Catalog).Include(s => s.Customer).Include(s => s.Employee)
                    .OrderByDescending(ser => ser.serviceDate)
                    .ToPagedList(page, 8);
                if (!model.Any())
                {
                    ViewBag.NoElementsAdvise = "You don't have any Restaurations avilable.";
                }
            }
            else
            {
                model = db.Services
                    .Where(ser => searchValue == null || ser.Customer.customerName.Contains(searchValue))
                    .Include(s => s.Catalog).Include(s => s.Customer).Include(s => s.Employee)
                    .OrderByDescending(ser => ser.serviceDate)
                    .ToPagedList(page, 8);
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ServiceList", model);
            }

            return View(model);
        }

        //
        // GET: /Service/Details/5

        public ActionResult Details(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // GET: /Service/Create

        public ActionResult Create()
        {
            ViewBag.statusId = new SelectList(db.Catalogs.Where(cat => cat.catalogName == "Reconstruction Status"), "catalogId", "catalogValue");
            ViewBag.customerId = new SelectList(db.Customers, "customerId", "customerName");
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "employeeName");

            var model = new Service();
            model.serviceDate = DateTime.Now;
            model.statusId = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultCreateReconstructionStatus"]);
            model.statusObservations = "The reconstructión was created";
            return View(model);
        }

        //
        // POST: /Service/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Exclude = "realPrice,estimatedPrice")] Service service)
        {
            if (ModelState.IsValid)
            {
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.statusId = new SelectList(db.Catalogs.Where(cat => cat.catalogName == "Reconstruction Status"), "catalogId", "catalogValue", service.statusId);
            ViewBag.customerId = new SelectList(db.Customers, "customerId", "customerName", service.customerId);
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "employeeName", service.employeeId);
            return View(service);
        }

        //
        // GET: /Service/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }

            ViewBag.statusId = new SelectList(db.Catalogs.Where(cat => cat.catalogName == "Reconstruction Status"), "catalogId", "catalogValue", service.statusId);
            ViewBag.customerId = new SelectList(db.Customers, "customerId", "customerName", service.customerId);
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "employeeName", service.employeeId);
            return View(service);
        }

        //
        // POST: /Service/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Service service)
        {
            if (ModelState.IsValid)
            {
                db.Entry(service).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.statusId = new SelectList(db.Catalogs.Where(cat => cat.catalogName == "Reconstruction Status"), "catalogId", "catalogValue", service.statusId);
            ViewBag.customerId = new SelectList(db.Customers, "customerId", "customerName", service.customerId);
            ViewBag.employeeId = new SelectList(db.Employees, "employeeId", "employeeName", service.employeeId);
            return View(service);
        }

        //
        // GET: /Service/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Service service = db.Services.Find(id);
            if (service == null)
            {
                return HttpNotFound();
            }
            return View(service);
        }

        //
        // POST: /Service/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
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