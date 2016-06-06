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

    public class CustomerController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /Customer/
        public ActionResult Index()
        {
            List<Customer> customers;

            if (!User.IsInRole("Admin") && !User.IsInRole("Employee"))
            {
                customers = db.Customers.Where(cu => cu.userLogin == User.Identity.Name).ToList();
                if (!customers.Any())
                {
                    ViewBag.NoElementsAdvise = "Provide the Customer information.";
                }
            }
            else
            {
                customers = db.Customers.ToList();
            }

            return View(customers);
        }

        //
        // GET: /Customer/Details/5

        public ActionResult Details(int id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // GET: /Customer/Create

        public ActionResult Create()
        {
            var model = new Customer();
            if (!db.Customers.Any(cu => cu.userLogin == User.Identity.Name))
            {
                model.userLogin = User.Identity.Name;

                ViewBag.userLogin = new SelectList(db.UserProfiles.
                Where(us => !db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName)).
                Select(u => u.UserName).Distinct().OrderBy(name => name).ToList(), model.userLogin);
            }
            else
            {
                ViewBag.userLogin = new SelectList(db.UserProfiles.
                    Where(us => !db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName)).
                    Select(u => u.UserName).Distinct().OrderBy(name => name).ToList());
            }

            return View(model);
        }

        //
        // POST: /Customer/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userLogin = new SelectList(db.UserProfiles.
                Where(us => !db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName)).
                Select(u => u.UserName).Distinct().OrderBy(name => name).ToList(), customer.userLogin);

            return View(customer);
        }

        //
        // GET: /Customer/Edit/5
        public ActionResult Edit(int id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            ViewBag.userLogin = new SelectList(db.UserProfiles.
                Where(us => (us.UserName == customer.userLogin) || (!db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName))).
                Select(u => u.UserName).Distinct().OrderBy(name => name).ToList(), customer.userLogin);

            return View(customer);
        }

        //
        // POST: /Customer/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userLogin = new SelectList(db.UserProfiles.
               Where(us => (us.UserName == customer.userLogin) || (!db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName))).
               Select(u => u.UserName).Distinct().OrderBy(name => name).ToList(), customer.userLogin);
            return View(customer);
        }

        //
        // GET: /Customer/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        //
        // POST: /Customer/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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