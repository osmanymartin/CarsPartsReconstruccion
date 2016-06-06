using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarsPartsReconstruccion.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "The way you can come true your dreams.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "With us you can recover your Car or Part as new.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Keep in touch all the time.";

            return View();
        }

        public ActionResult Catalogs()
        {
            ViewBag.Message = "Set Up the Site.";

            return View();
        }
    }
}
