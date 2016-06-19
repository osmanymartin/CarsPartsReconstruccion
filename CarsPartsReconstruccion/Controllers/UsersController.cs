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
    public class UsersController : Controller
    {
        private db_cars_parts_reconstructionStrConn db = new db_cars_parts_reconstructionStrConn();

        //
        // GET: /Users/

        public ActionResult Index()
        {
            var model = db.UserProfiles.Include(us => us.webpages_Roles).ToList();
            return View(model);
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id = 0)
        {
            UserProfile userprofile = db.UserProfiles.Find(id);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile, string RoleName)
        {
            if (ModelState.IsValid)
            {
                webpages_Roles role;

                role = db.webpages_Roles.Where(ro => ro.RoleName == RoleName).FirstOrDefault();
                if (role != null)
                {
                    var user = db.UserProfiles.Single(u => u.UserId == userprofile.UserId);

                    var roleInUser = user.webpages_Roles.Single(ro => ro.RoleId == role.RoleId);

                    if (!user.webpages_Roles.Remove(roleInUser))
                    {
                        user.webpages_Roles.Add(role);
                    }

                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}