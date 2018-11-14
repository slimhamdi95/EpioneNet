using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Epione.Service;
using System.Net;
using Epione.Domain.Entities;

using MailKit.Net.Smtp;
using MimeKit;

namespace Epione.Web.Controllers
{
    public class DoctorController : Controller
    {
        GestionDoctor gd = new Service.GestionDoctor();

        // GET: Doctor
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var liste = gd.RetournerByCondition(null, null);
            return View(liste);
        }
        [HttpPost]
        public ActionResult Index(string validate, string specialité, string ville)
        {
            var liste = gd.RetournerByCondition(x => x.validation.ToString() == validate && x.Specialty.Contains(specialité) && x.Ville.Contains(ville));
            ViewBag.liste1 = gd.RetournerByCondition(null, null);
            return View(liste);

        }
        // GET: Doctor
        public ActionResult Index1()
        {
            var liste = gd.RetournerByCondition(null, null);
            return View(liste);
        }

        [HttpPost]
        public ActionResult Index1(string SearchString, string SearchString2)
        {
            var liste = gd.RetournerByCondition(x => (x.Specialty.Contains(SearchString) || x.FirstName.Contains(SearchString) || x.LastName.Contains(SearchString)) && x.Ville.Contains(SearchString2));
            //var liste2 = servDoc.RetournerByCondition(x => x.Ville.Contains(SearchString2));

            return View(liste);
        }

        // GET: Doctor/Details/5
        public ActionResult Details1(string id)
        {
            Doctor d = gd.RetournerById(id);
            return View(d);
        }
        // GET: Doctor/Details/5
        public ActionResult Details(string id)
        {

            Doctor d = gd.RetournerById(id);
            return View(d);
        }

        // GET: Doctor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Doctor/Edit/5
        public ActionResult Edit(string id)
        {
            var liste = gd.RetournerByCondition(null, null);

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor d = null;
            d = gd.RetournerById(id);
            if (d == null)
            {
                return HttpNotFound();
            }
            gd.ValiderDoctor(d);
            gd.Commit();


            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("test", "sindazehani08@gmail.com"));
            message.To.Add(new MailboxAddress("anis.bougatef@esprit.tn"));
            message.Subject = "test mail";
            message.Body = new TextPart("plain")
            {

                Text = "your request has been accepted in our platform map levio "
            };
            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("sindazehani08@gmail.com", "sinda1234");
                client.Send(message);
                client.Disconnect(true);
                return RedirectToAction("Index");
                // return View(liste);

            } }

        // POST: Doctor/Edit/5
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

        // GET: Doctor/Delete/5
        public ActionResult Delete(string id)
        {
            Doctor d;
            d = gd.RetournerById(id);
            gd.Supprimer(d);
            gd.Commit();
            return RedirectToAction("Index");
        }

        // POST: Doctor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Details(string id, string name)
        {
            int x;
            Doctor d;
            //name = "Dr Anne Mansoux";
            d = gd.RetournerById(id);
            name = "Dr " + d.FirstName + " " + d.LastName + " " + d.Specialty;
            x = gd.ComparerDoctor(name);
            if (x == 1)
            {
                gd.ValiderDoctor(d);
                gd.Commit();
            }
            return View(d);

        }
    }
}

