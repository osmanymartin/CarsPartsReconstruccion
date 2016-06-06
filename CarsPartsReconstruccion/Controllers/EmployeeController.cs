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
    public class EmployeeController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();
        ////private UsersContext dbUser = new UsersContext();

        //
        // GET: /Employee/

        public ActionResult Index()
        {
            List<Employee> employees;

            if (!User.IsInRole("Admin"))
            {
                employees = db.Employees.Where(em => em.userLogin == User.Identity.Name).Include(e => e.Catalog).ToList();
                if (!employees.Any())
                {
                    ViewBag.NoElementsAdvise = "Provide your Employee information.";
                }
            }
            else
            {
                employees = db.Employees.Include(e => e.Catalog).ToList();
            }

            return View(employees);
        }

        //
        // GET: /Employee/Details/5

        public ActionResult Details(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // GET: /Employee/Create

        public ActionResult Create()
        {
            var model = new Customer();
            if (!db.Employees.Any(cu => cu.userLogin == User.Identity.Name))
            {
                model.userLogin = User.Identity.Name;
            }

            ViewBag.positionId = new SelectList(db.Catalogs.Where(c => c.catalogName == "Employee Position"), "catalogId", "catalogValue");
            ViewBag.userLogin = new SelectList(db.UserProfiles.
                Where(us => !db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName)).
                Select(u => u.UserName).Distinct().OrderBy(name => name).ToList());

            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.positionId = new SelectList(db.Catalogs.Where(c => c.catalogName == "Employee Position"), "catalogId", "catalogValue", employee.positionId);
            ViewBag.userLogin = new SelectList(db.UserProfiles.
                Where(us => !db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName)).
                Select(u => u.UserName).Distinct().OrderBy(name => name).ToList(), employee.userLogin);
            return View(employee);
        }

        //
        // GET: /Employee/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.positionId = new SelectList(db.Catalogs.Where(c => c.catalogName == "Employee Position"), "catalogId", "catalogValue", employee.positionId);
            ViewBag.userLogin = new SelectList(db.UserProfiles.
                Where(us => (us.UserName == employee.userLogin) || (!db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName))).
                Select(u => u.UserName).Distinct().OrderBy(name => name).ToList(), employee.userLogin);
            return View(employee);
        }

        //
        // POST: /Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.positionId = new SelectList(db.Catalogs.Where(c => c.catalogName == "Employee Position"), "catalogId", "catalogValue", employee.positionId);
            ViewBag.userLogin = new SelectList(db.UserProfiles.
                Where(us => (us.UserName == employee.userLogin) || (!db.Employees.Any(em => em.userLogin == us.UserName) && !db.Customers.Any(cu => cu.userLogin == us.UserName))).
                Select(u => u.UserName).Distinct().OrderBy(name => name).ToList(), employee.userLogin);
            return View(employee);
        }

        //
        // GET: /Employee/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id = 0)
        {
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        //
        // POST: /Employee/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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