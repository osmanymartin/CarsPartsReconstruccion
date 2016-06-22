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
    public class ServiceCarController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /ServiceCar/

        public ActionResult Index()
        {
            var servicecars = db.ServiceCars.Include(s => s.Catalog).Include(s => s.Catalog1).Include(s => s.Catalog2).Include(s => s.Service);
            return View(servicecars.ToList());
        }

        public ActionResult ServiceCars(int serviceId)
        {
            var servicecars = db.ServiceCars.Where(sercar => sercar.serviceId == serviceId)
                .Include(s => s.Catalog).Include(s => s.Catalog1).Include(s => s.Catalog2).Include(s => s.Service);

            return PartialView("_ServiceCar", servicecars.ToList());
        }


        //
        // GET: /ServiceCar/Details/5

        public ActionResult Details(int id = 0)
        {
            ServiceCar servicecar = db.ServiceCars.Find(id);
            if (servicecar == null)
            {
                return HttpNotFound();
            }
            return View(servicecar);
        }

        //
        // GET: /ServiceCar/Create

        public ActionResult Create()
        {
            ViewBag.carBrandId = new SelectList(db.Catalogs, "catalogId", "catalogValue");
            ViewBag.carModelId = new SelectList(db.Catalogs, "catalogId", "catalogValue");
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue");
            ViewBag.serviceId = new SelectList(db.Services, "serviceId", "serviceDescription");
            return View();
        }

        //
        // POST: /ServiceCar/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServiceCar servicecar)
        {
            if (ModelState.IsValid)
            {
                db.ServiceCars.Add(servicecar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.carBrandId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.carBrandId);
            ViewBag.carModelId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.carModelId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.statusId);
            ViewBag.serviceId = new SelectList(db.Services, "serviceId", "serviceDescription", servicecar.serviceId);
            return View(servicecar);
        }

        //
        // GET: /ServiceCar/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ServiceCar servicecar = db.ServiceCars.Find(id);
            if (servicecar == null)
            {
                return HttpNotFound();
            }
            ViewBag.carBrandId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.carBrandId);
            ViewBag.carModelId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.carModelId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.statusId);
            ViewBag.serviceId = new SelectList(db.Services, "serviceId", "serviceDescription", servicecar.serviceId);
            return View(servicecar);
        }

        //
        // POST: /ServiceCar/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServiceCar servicecar)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicecar).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.carBrandId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.carBrandId);
            ViewBag.carModelId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.carModelId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicecar.statusId);
            ViewBag.serviceId = new SelectList(db.Services, "serviceId", "serviceDescription", servicecar.serviceId);
            return View(servicecar);
        }

        //
        // GET: /ServiceCar/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ServiceCar servicecar = db.ServiceCars.Find(id);
            if (servicecar == null)
            {
                return HttpNotFound();
            }
            return View(servicecar);
        }

        //
        // POST: /ServiceCar/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceCar servicecar = db.ServiceCars.Find(id);
            db.ServiceCars.Remove(servicecar);
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