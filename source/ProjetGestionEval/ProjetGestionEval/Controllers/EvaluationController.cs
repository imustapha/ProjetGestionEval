using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class EvaluationController : Controller
    {
        // GET: Evaluation

        static string ConnectionString = "database=bd_gestion;server=localhost;uid=root";
        MySqlConnection Connection = new MySqlConnection(ConnectionString);
        bd_gestionEntities bd = new bd_gestionEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult IndexCol()
        {
            return View(bd.collaborateur.Where(m=>m.FLAGEVAL==false).ToList());
        }
        // GET: Evaluation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Evaluation/Create
        public ActionResult Create(int? id)
        {
            ViewBag.id = id;
            return View();
        }

        // POST: Evaluation/Create
        [HttpPost]
        public ActionResult Create(int? id, [Bind(Exclude = "")]evaluation ev, FormCollection collection)
        {
            var test = collection["Memo"];
            var bb = bd.tache.Where(m => m.IDCOLLABORATEUR == id).ToList();
            int i = 0;
            foreach (var item in bb) {
                

                ++i;
            }
            try
            {
                if (ModelState.IsValid)
                {

                    var y = bd.evaluation.Add(ev);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evaluation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Evaluation/Edit/5
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

        // GET: Evaluation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Evaluation/Delete/5
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
