using Epione.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epione.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Chat()
        {
            GestionUser gu = new GestionUser();
            ViewBag.Message = "Your contact page.";
            ViewBag.hdnCurrentUserName =User.Identity.Name;
            ViewBag.hdnCurrentUserID = gu.RetournerByCondition(x=>x.UserName==User.Identity.Name).First().Id ;
            return View();
        }
        public ActionResult Chatt()
        {
            GestionUser gu = new GestionUser();
            ViewBag.Message = "Your contact page.";
            ViewBag.hdnCurrentUserName = User.Identity.Name;
            ViewBag.hdnCurrentUserID = gu.RetournerByCondition(x => x.UserName == User.Identity.Name).First().Id;
            return View();
        }
    }
}