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
    public class ServicePartController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /ServicePart/

        public ActionResult Index()
        {
            var serviceparts = db.ServiceParts
                .Include(s => s.Part).Include(s => s.Catalog).Include(s => s.Service);
            return View(serviceparts.ToList());
        }

        public ActionResult ServiceParts(int serviceId)
        {
            var serviceparts = db.ServiceParts
                .Where(sercar => sercar.serviceId == serviceId)
                .Include(s => s.Part).Include(s => s.Catalog).Include(s => s.Service);

            return PartialView("_ServicePart", serviceparts.ToList());
        }

        //
        // GET: /ServicePart/Details/5

        public ActionResult Details(int id = 0)
        {
            ServicePart servicepart = db.ServiceParts.Find(id);
            if (servicepart == null)
            {
                return HttpNotFound();
            }
            return View(servicepart);
        }

        //
        // GET: /ServicePart/Create

        public ActionResult Create()
        {
            ViewBag.partId = new SelectList(db.Parts, "partId", "partName");
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue");
            ViewBag.serviceId = new SelectList(db.Services, "serviceId", "serviceDescription");
            return View();
        }

        //
        // POST: /ServicePart/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ServicePart servicepart)
        {
            if (ModelState.IsValid)
            {
                db.ServiceParts.Add(servicepart);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.partId = new SelectList(db.Parts, "partId", "partName", servicepart.partId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicepart.statusId);
            ViewBag.serviceId = new SelectList(db.Services, "serviceId", "serviceDescription", servicepart.serviceId);
            return View(servicepart);
        }

        //
        // GET: /ServicePart/Edit/5

        public ActionResult Edit(int id = 0)
        {
            ServicePart servicepart = db.ServiceParts.Find(id);
            if (servicepart == null)
            {
                return HttpNotFound();
            }
            ViewBag.partId = new SelectList(db.Parts, "partId", "partName", servicepart.partId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicepart.statusId);
            ViewBag.serviceId = new SelectList(db.Services, "serviceId", "serviceDescription", servicepart.serviceId);
            return View(servicepart);
        }

        //
        // POST: /ServicePart/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServicePart servicepart)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servicepart).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.partId = new SelectList(db.Parts, "partId", "partName", servicepart.partId);
            ViewBag.statusId = new SelectList(db.Catalogs, "catalogId", "catalogValue", servicepart.statusId);
            ViewBag.serviceId = new SelectList(db.Services, "serviceId", "serviceDescription", servicepart.serviceId);
            return View(servicepart);
        }

        //
        // GET: /ServicePart/Delete/5

        public ActionResult Delete(int id = 0)
        {
            ServicePart servicepart = db.ServiceParts.Find(id);
            if (servicepart == null)
            {
                return HttpNotFound();
            }
            return View(servicepart);
        }

        //
        // POST: /ServicePart/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServicePart servicepart = db.ServiceParts.Find(id);
            db.ServiceParts.Remove(servicepart);
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