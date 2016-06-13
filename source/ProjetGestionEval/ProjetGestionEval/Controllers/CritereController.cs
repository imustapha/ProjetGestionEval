using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class CritereController : Controller
    {
        bd_gestionEntities bd = new bd_gestionEntities();
        // GET: Critere
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View(bd.critere.ToList());
        }

        // GET: Critere/Details/5
        [Authorize(Roles = "admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Critere/Create
        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            ViewBag.IdCritereFamille = new SelectList(bd.famillecritere, "IDFAMILLECRITERE", "NOMFAMILLECRITERE");
            return View();
        }

        // POST: Critere/Create
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Create(critere Critere)
        {
            ViewBag.IdCritereFamille = new SelectList(bd.famillecritere, "IDFAMILLECRITERE", "NOMFAMILLECRITERE");
            try
            {
                Critere.famillecritere = bd.famillecritere.Find(Critere.IDFAMILLECRITERE);
                if (ModelState.IsValid) {
                    bd.critere.Add(Critere);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                // TODO: Add insert logic here

                return View("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Critere/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            ViewBag.IdCritereFamille = new SelectList(bd.famillecritere, "IDFAMILLECRITERE", "NOMFAMILLECRITERE");
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            critere Cri = bd.critere.Find(id);
            if (Cri == null)
                return HttpNotFound();
            return View(Cri);
        }

        // POST: Critere/Edit/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id, critere Cri)
        {
            ViewBag.IdCritereFamille = new SelectList(bd.famillecritere, "IDFAMILLECRITERE", "NOMFAMILLECRITERE");
            critere Crit = bd.critere.Find(id);
            Crit.IDFAMILLECRITERE = Cri.IDFAMILLECRITERE;
            Crit.NOMCRITERE = Cri.NOMCRITERE;
            Crit.famillecritere = Cri.famillecritere;
            try
            {
                if (ModelState.IsValid)
                {
                    bd.Entry(Crit).State = System.Data.Entity.EntityState.Modified;

                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }

                // TODO: Add update logic here

                return View(Crit);
            }
            catch
            {
                return View();
            }
        }

        // GET: Critere/Delete/5
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            // TODO: Add delete logic here
            critere Cri = new critere();
            Cri = bd.critere.Find(id);
            if (Cri == null)
                return HttpNotFound();
            return View(Cri);
        }

        // POST: Critere/Delete/5
        [HttpPost]
        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id, critere Critere)
        {
            try
            {

                if (ModelState.IsValid) { 
                if (id == null)
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                // TODO: Add delete logic here
                critere Cri = new critere();
                Cri = bd.critere.Find(id);
                if (Cri == null)
                    return HttpNotFound();
                bd.critere.Remove(Cri);
                bd.SaveChanges();
                return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
    }
}
