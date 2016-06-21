using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class TacheController : Controller
    {
        static string ConnectionString = "database=bd_gestion;server=localhost;uid=root";
        MySqlConnection Connection = new MySqlConnection(ConnectionString);
        bd_gestionEntities bd = new bd_gestionEntities();
        // GET: Tache
        public ActionResult Index()
        {
            return View(bd.tache.ToList());
        }
        public ActionResult IndexTa()
        {
            var asp = bd.aspnetusers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var ctn = bd.collaborateur.Single(m => m.IdUser == asp.Id);
            var id = ctn.IDCOLLABORATEUR;
            //Connection.Open();
            //string req = "SELECT IDTACHE FROM tache where IDCOLLABORATEUR=" + id;
            ////SELECT * FROM projet where IDPROJET IN (SELECT IDPROJET FROM administrer where IDCOLLABORATEUR=" + id+")
            //MySqlCommand cmd = new MySqlCommand(req, Connection);
            //MySqlDataReader dr = cmd.ExecuteReader();

            //List<String> s1 = new List<string>();

            //while (dr.Read())
            //{
            //    s1.Add(dr[0].ToString());

            //}

            //int s = 0;

            //if (s1 != null) { s = int.Parse(s1); }
            //ViewBag.ss = s1.Select(int.Parse).ToList();


            return View(bd.tache.Where(m=>m.IDCOLLABORATEUR==id).ToList());
        }

        // GET: Tache/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            tache Tache = new tache();

            Tache = bd.tache.Find(id);
            ViewBag.datedebut = Tache.DATEDEBUTTACHE.Value.Day.ToString() + "/" + Tache.DATEDEBUTTACHE.Value.Month.ToString() + "/" + Tache.DATEDEBUTTACHE.Value.Year.ToString();
            ViewBag.datefin = Tache.DATEFINTACHE.Value.Day.ToString() + "/" + Tache.DATEFINTACHE.Value.Month.ToString() + "/" + Tache.DATEFINTACHE.Value.Year.ToString();
            return View(Tache);
        }

        // GET: Tache/Create
        public ActionResult Create()
        {
            var asp = bd.aspnetusers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var ctn = bd.collaborateur.Single(m => m.IdUser == asp.Id);
            if (User.IsInRole("superuser") || User.IsInRole("admin"))
            {
                ViewBag.Collaborateur = new SelectList(bd.collaborateur, "IDCOLLABORATEUR", "NOM");
            }
            else { ViewBag.pr = ctn.NOM; }
            ViewBag.Projet = new SelectList(bd.projet, "IDPROJET", "NOMPROJET");
            return View();
        }

        // POST: Tache/Create
        [HttpPost]
        //[Authorize(Roles="superuser,viewp,viewt,admin")]
        public ActionResult Create(tache Tache, FormCollection fc)
        {
            if (Tache.IDCOLLABORATEUR == 0 )
            { 
            var asp = bd.aspnetusers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var ctn = bd.collaborateur.Single(m => m.IdUser == asp.Id);
            Tache.collaborateur = ctn;
            }
            var testid = fc["DATEFINTACHE"];


            try
            {
                // TODO: Add insert logic here

                ViewBag.Collaborateur = new SelectList(bd.collaborateur, "IDCOLLABORATEUR", "NOM");
                ViewBag.Projet = new SelectList(bd.projet, "IDPROJET", "NOMPROJET");
                if (ModelState.IsValid)
                {
                    bd.tache.Add(Tache);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(Tache);
            }
            catch
            {
                return View();
            }
        }

        // GET: Tache/Edit/5
        public ActionResult Edit(int? id)
        {
            ViewBag.Collaborateur = new SelectList(bd.collaborateur, "IDCOLLABORATEUR", "NOM");
            ViewBag.Projet = new SelectList(bd.projet, "IDPROJET", "NOMPROJET");
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            tache Tache = bd.tache.Find(id);
            if (Tache == null)
                return HttpNotFound();
            return View(Tache);
        }

        // POST: Tache/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, tache Tache)
        {
            ViewBag.Collaborateur = new SelectList(bd.collaborateur, "IDCOLLABORATEUR", "NOM");
            ViewBag.Projet = new SelectList(bd.projet, "IDPROJET", "NOMPROJET");
            tache T = bd.tache.Find(id);
            T.IDTACHE = id;
            T.NOMTACHE = Tache.NOMTACHE;
            T.DATEDEBUTTACHE = Tache.DATEDEBUTTACHE;
            T.DATEFINTACHE = Tache.DATEFINTACHE;
            T.IDPROJET = Tache.IDPROJET;
            T.IDCOLLABORATEUR = Tache.IDCOLLABORATEUR;
            T.collaborateur = bd.collaborateur.Single(m => m.IDCOLLABORATEUR == Tache.IDCOLLABORATEUR);
            T.projet = bd.projet.Single(m => m.IDPROJET == Tache.IDPROJET);
            try
            {

                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    bd.Entry(T).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(Tache);
            }
            catch
            {
                return View();
            }
        }

        // GET: Tache/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tache Tache = bd.tache.Find(id);
            if (Tache == null)
            {
                return HttpNotFound();
            }
            ViewBag.datedebut = Tache.DATEDEBUTTACHE.Value.Day.ToString() + "/" + Tache.DATEDEBUTTACHE.Value.Month.ToString() + "/" + Tache.DATEDEBUTTACHE.Value.Year.ToString();
            ViewBag.datefin = Tache.DATEFINTACHE.Value.Day.ToString() + "/" + Tache.DATEFINTACHE.Value.Month.ToString() + "/" + Tache.DATEFINTACHE.Value.Year.ToString();
            return View(Tache);
        }

        // POST: Tache/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, tache T)
        {
            try
            {
                tache Tache = new tache();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    Tache = bd.tache.Find(id);
                    if (Tache == null)
                        return HttpNotFound();
                    bd.tache.Remove(Tache);
                    bd.SaveChanges();
                    // TODO: Add delete logic here
                    return RedirectToAction("Index");
                }
                return View(Tache);
            }

            catch
            {
                return View();
            }
        }
    }
}
