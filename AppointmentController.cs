using Epione.Data;
using Epione.Domain.Entities;
using Epione.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Epione.Web.Controllers
{
    public class AppointmentController : Controller
    {
        List<Appointment> malist;
        public AppointmentController()
        {
            malist = new List<Appointment>();
        }
        private EpioneContext ec = new EpioneContext();
        GestionAppointment Ga = new GestionAppointment();
        GestionUser gu = new GestionUser();
        // GET: Appointment
        public ActionResult Index()
        {
            string name = User.Identity.Name;
            string ADocFK = gu.RetournerByCondition(x => x.UserName == name).First().Id;
           var Liste = Ga.RetournerByCondition(x=>x.DocFK== ADocFK, x => x.statut ==2);

            var lista = from d in Liste
                        where d.statut == 1
                        select d;

            ViewBag.events = lista;
                return View(lista);
        }
        public ActionResult Index2()
        {
            string name = User.Identity.Name;
            string ADocFK = gu.RetournerByCondition(x => x.UserName == name).First().Id;
            malist = ec.Appointments.Where(x => x.DocFK == ADocFK && x.statut == 2).ToList();
            //var Liste = Ga.RetournerByCondition(x => x.DocFK == ADocFK, x => x.statut == 2);
            //var lista = from d in Liste
                      //  where d.statut == 2
                       // select d;

            //malist = lista.ToList();

            //ViewBag.events = lista;
            return View(malist);
        }
        // GET: Appointment //sinda
        [Authorize(Roles = "Patient")]
        public ActionResult Index3()
        {
            var liste = Ga.RetournerByCondition(null, null);
            return View(liste);
        }
        [HttpPost] //sinda
        public ActionResult Index3(string SearchString)
        {
            var liste = Ga.RetournerByCondition(null, null);
            return View(liste);
        }
        public JsonResult GetEvents()
        {
            string name = User.Identity.Name;
            string ADocFK = gu.RetournerByCondition(x => x.UserName == name).First().Id;
            var Liste = Ga.RetournerByCondition();

            return new JsonResult { Data = Liste, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

        }

        // GET: Appointment/Details/5
       // [Authorize(Roles = "Patient")]
        public ActionResult Details(int id)
        {
            Appointment a = Ga.RetournerById(id);
            return View(a);
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public ActionResult Create(Appointment a)
        {
            if (ModelState.IsValid)
            {
                string namePat = User.Identity.Name;
                a.PatFK = gu.RetournerByCondition(x => x.UserName == namePat).First().Id;
                Ga.Ajouter(a);
                Ga.Commit();
                RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        // GET: Appointment/Edit/5
        //[Authorize(Roles = "Patient")]

        public ActionResult Edit(int id)
        {
            Appointment A = Ga.RetournerById(id);
            return View(A);
        }

        // POST: Appointment/Edit/5
        [HttpPost]
        public ActionResult Edit(Appointment a)
        {
            Appointment A = Ga.RetournerById(a.AppointmentId);

            if (A.date.Equals(a.date) && A.time.Equals(a.time))
            {
                A.message = a.message;
                A.objectAppointment = a.objectAppointment;
                if (ModelState.IsValid)
                {
                    Ga.MisAjour(A);
                    Ga.Commit();
                }
            }
            else
            {
                Ga.Supprimer(A);
                Ga.Ajouter(a);
                Ga.Commit();
            }

            return RedirectToAction("Index3");
        }

        // GET: Appointment/Delete/5
        //[Authorize(Roles = "Patient")]
        public ActionResult Delete(int id)
        {
            Appointment a;
            a = Ga.RetournerById(id);
            Ga.Supprimer(a);
            Ga.Commit();
            return RedirectToAction("Index3");
        }

        // POST: Appointment/Delete/5
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
        public ActionResult Approuver(int? id)
        {
            // Appointment A = Ga.RetournerById(id);
            // A.statut = 1;
            Appointment A = ec.Appointments.Find(id);
            ec.Appointments.Attach(A);
            A.statut = 1;
            ec.SaveChanges();
            return RedirectToAction("Index2");
        }
        
    }
}
