using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class CollaborateurPEController : Controller
    {
        bd_gestionEntities bd =new bd_gestionEntities();
        // GET: CollaborateurPE
        public ActionResult Index()
        {
            return View(bd.collaborateur.ToList());
        }

        // GET: CollaborateurPE/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CollaborateurPE/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CollaborateurPE/Create
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

        // GET: CollaborateurPE/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CollaborateurPE/Edit/5
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

        // GET: CollaborateurPE/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CollaborateurPE/Delete/5
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
