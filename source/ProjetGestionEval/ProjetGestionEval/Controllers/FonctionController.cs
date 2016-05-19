using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class FonctionController : Controller
    {
        bd_gestionEntities bd = new bd_gestionEntities();
        // GET: Fonction
        public ActionResult Index()
        {
            return View(bd.fonction.ToList());
        }

        // GET: Fonction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            fonction fonc = bd.fonction.Find(id);
            if (fonc == null)
                return HttpNotFound();


            return View(fonc);
        }

        // GET: Fonction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Fonction/Create
        [HttpPost]
        public ActionResult Create(fonction fonct)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bd.fonction.Add(fonct);
                    bd.SaveChanges();
                    return RedirectToAction("Index");

                } return View(fonct);
            }
            catch
            {
                return View();
            }
        }

        // GET: Fonction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           fonction fon = bd.fonction.Find(id);
            if (fon == null)
                return HttpNotFound();
            return View(fon);
        }

        // POST: Fonction/Edit/5
        [HttpPost]
        public ActionResult Edit(fonction fon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bd.Entry(fon).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return RedirectToAction("Index");


                }
                return View(fon);
            }
            catch
            {
                return View();
            }
        }

        // GET: Fonction/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            fonction fon = bd.fonction.Find(id);
            if (fon == null)
            {
                return HttpNotFound();
            }
            return View(fon);
        }

        // POST: Fonction/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, fonction fonct)
        {
            try
            {
                fonction fon = new fonction();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    fon = bd.fonction.Find(id);
                    if (fon == null)
                        return HttpNotFound();


                    bd.fonction.Remove(fon);
                    bd.SaveChanges();
                    return RedirectToAction("Index");

                } return View(fon);


            }
            catch
            {
                return View();
            }
        }
    }
}
