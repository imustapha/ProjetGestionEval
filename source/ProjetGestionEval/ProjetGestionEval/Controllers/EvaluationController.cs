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

        //static string ConnectionString = "database=bd_gestion;server=localhost;uid=root";
        //MySqlConnection Connection = new MySqlConnection(ConnectionString);
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
            var dt= DateTime.Now;
            ev.DATEEVALUATION = dt;
            ev.DATENEXTEVALUATION = dt.AddMonths(15);
            var id1 = collection["IDEVALUATEUR"];
            var id2 = collection["IDCOLLABORATEUR"];
          
            var bb = bd.tache.Where(m => m.IDCOLLABORATEUR == id).ToList();
            int i = 0;
            foreach (var item in bb) {
                string f = i.ToString();
                item.NoteTache = collection[f];

                bd.Entry(item).State = System.Data.Entity.EntityState.Modified;
                

                ++i;
            }
            int j = 0;
            var fm = bd.famillecritere.ToList();
            foreach (var it in fm)
            {
                var cr = bd.critere.Where(m => m.IDFAMILLECRITERE == it.IDFAMILLECRITERE);

                foreach (var item in cr)
                {string d=j.ToString();
                    item.NoteCritere = collection[d];
                    //bd.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    ev.critere.Add(item);
                    j++;
                }
            }
            var col = bd.collaborateur.Find(id);
            col.Evaluer = true;
            if (ev.STATUT == "titulaire")
            {
                
                col.TYPECOLLABORATEUR = "Titulaire";
                

            }
            bd.Entry(col).State = System.Data.Entity.EntityState.Modified;
            try
            {
                //if (ModelState.IsValid)
                //{

                    bd.evaluation.Add(ev);
                    bd.SaveChanges();
                //    return RedirectToAction("IndexCol");
                //}
                return RedirectToAction("IndexCol");
            }
            catch(Exception ex)
            {
                ex.ToString();
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
