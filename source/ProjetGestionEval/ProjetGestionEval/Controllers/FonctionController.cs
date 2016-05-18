using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class FonctionController : Controller
    {
        // GET: Fonction
        public ActionResult Index()
        {
            return View();
        }

        // GET: Fonction/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Fonction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fonction/Create
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

        // GET: Fonction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Fonction/Edit/5
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

        // GET: Fonction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Fonction/Delete/5
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
