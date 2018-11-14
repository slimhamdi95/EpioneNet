using Epione.Domain.Entities;
using Epione.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epione.Web.Controllers
{
    public class AvailabilityController : Controller
    {
        GestionAvailability Ga = new GestionAvailability();
        GestionUser gu = new GestionUser();
        // GET: Availability
        public ActionResult Index()
        {
            string name = User.Identity.Name;
            string ADocFK = gu.RetournerByCondition(x => x.UserName == name).First().Id;
            var Liste = Ga.RetournerByCondition(x => x.DocFK == ADocFK);
            return View(Liste);
        }

        // GET: Availability/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Availability/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Availability/Create
        [HttpPost]
        public ActionResult Create(Availability A)
        {

            if (ModelState.IsValid)
            {
                string name = User.Identity.Name;
                A.DocFK = gu.RetournerByCondition(x => x.UserName == name).First().Id;
                Ga.Ajouter(A);
                Ga.Commit();

            }
            return RedirectToAction("Index");
        }

        // GET: Availability/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Availability/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Availability/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Availability/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
