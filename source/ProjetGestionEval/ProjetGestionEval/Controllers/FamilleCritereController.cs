using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class FamilleCritereController : Controller
    {            bd_gestionEntities bd = new bd_gestionEntities();
        // GET: FamilleCritere
        public ActionResult Index()
        {

            return View(bd.famillecritere.ToList());
        }

        // GET: FamilleCritere/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            famillecritere FC = bd.famillecritere.Single(m=>m.IDFAMILLECRITERE==id);
            return View(FC);
        }

        // GET: FamilleCritere/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FamilleCritere/Create
        [HttpPost]
        public ActionResult Create( famillecritere FC)
        {
            try
            {
                
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {

                    bd.famillecritere.Add(FC);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Create",FC);
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: FamilleCritere/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            famillecritere FC = bd.famillecritere.Find(id);
            if (FC == null)
                return HttpNotFound();

            return View(FC);
        }

        // POST: FamilleCritere/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, famillecritere FC)
        {
            try
            {
                // TODO: Add update logic here
                famillecritere Fmc = bd.famillecritere.Find(id);
                Fmc.NOMFAMILLECRITERE = FC.NOMFAMILLECRITERE;
                bd.Entry(Fmc).State = System.Data.Entity.EntityState.Modified;
                bd.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: FamilleCritere/Delete/5
        public ActionResult Delete(int? id)
        {
            famillecritere FC = bd.famillecritere.Find(id); 
            return View(FC);
        }

        // POST: FamilleCritere/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id ,famillecritere FC)
        {
            try
            {famillecritere FmC=new famillecritere();
                if (ModelState.IsValid)
                {
                    FmC = bd.famillecritere.Find(id);
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    if (FmC == null)
                        return HttpNotFound();
                    // TODO: Add delete logic here
                    bd.famillecritere.Remove(FmC);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(FC);
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
