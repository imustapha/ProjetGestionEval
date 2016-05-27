using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class CritereController : Controller
    {
        bd_gestionEntities bd = new bd_gestionEntities();
        // GET: Critere
        public ActionResult Index()
        {
            return View(bd.critere.ToList());
        }

        // GET: Critere/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Critere/Create
        public ActionResult Create()
        {
            ViewBag.IdCritereFamille = new SelectList(bd.famillecritere, "IDFAMILLECRITERE", "NOMCRITERE");
            return View();
        }

        // POST: Critere/Create
        [HttpPost]
        public ActionResult Create(int? id, critere Critere)
        {
            ViewBag.IdCritereFamille = new SelectList(bd.famillecritere, "IDFAMILLECRITERE", "NOMCRITERE");
            try
            {
                if (ModelState.IsValid) {
                    bd.critere.Add(Critere);
                    bd.SaveChanges();
                return View("Index");
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Critere/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Critere/Edit/5
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

        // GET: Critere/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Critere/Delete/5
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
