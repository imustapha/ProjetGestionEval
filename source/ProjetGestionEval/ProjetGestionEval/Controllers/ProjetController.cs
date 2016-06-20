using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class ProjetController : Controller
    {
        static string ConnectionString = "database=bd_gestion;server=localhost;uid=root";
        MySqlConnection Connection = new MySqlConnection(ConnectionString);
        bd_gestionEntities bd = new bd_gestionEntities();
        // GET: Projet
        public ActionResult Index()
        {
            return View(bd.projet.ToList());
        }
        public ActionResult IndexPr()
        {
            var asp = bd.aspnetusers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
            var ctn = bd.collaborateur.Single(m => m.IdUser == asp.Id);
            var id = ctn.IDCOLLABORATEUR;
            Connection.Open();
            string req = "SELECT IDPROJET FROM administrer where IDCOLLABORATEUR=" + id ;
            //SELECT * FROM projet where IDPROJET IN (SELECT IDPROJET FROM administrer where IDCOLLABORATEUR=" + id+")
            MySqlCommand cmd = new MySqlCommand(req, Connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            
              List<String> s1=new List<string>();
          
            while (dr.Read())
            {
                s1.Add(dr[0].ToString());
                
            }
            
            //int s = 0;
            
            //if (s1 != null) { s = int.Parse(s1); }
            ViewBag.ss=s1.Select(int.Parse).ToList();


            return View(bd.projet.ToList());
        }
        // GET: Projet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
            projet pr = bd.projet.Find(id);
            if (pr == null)
                return HttpNotFound();


            return View(pr);
        }

        // GET: Projet/Create
        public ActionResult Create()
        {
            //.Where(m => m.TYPECOLLABORATEUR=="collabpe")
            ViewBag.collaborateur = new SelectList(bd.collaborateur.Where(m => m.TYPECOLLABORATEUR == "Titulaire"), "IDCOLLABORATEUR", "NOM");
            ViewBag.collaborateurpe = new SelectList(bd.collaborateur.Where(m => m.TYPECOLLABORATEUR == "P.E"), "IDCOLLABORATEUR", "NOM");
            ViewBag.client = new SelectList(bd.client, "IDCLIENT", "ABREVIATION");

            return View();
        }

        // POST: Projet/Create
        [HttpPost]
        public ActionResult Create([Bind(Exclude = "collaborateur")]projet Projet, FormCollection fc)
        {
            var testid = fc["collaborateur"];
            string[] testids = testid.Split(',');
            foreach (var item in testids)
            {
                int b = int.Parse(item);
                Projet.collaborateur.Add(bd.collaborateur.Where(m => m.IDCOLLABORATEUR == b).FirstOrDefault());
            }

            ViewBag.collaborateur = new MultiSelectList(bd.collaborateur, "IDCOLLABORATEUR", "NOM");
            
            ViewBag.client = new SelectList(bd.client, "IDCLIENT", "ABREVIATION");
            Projet.client = bd.client.Single(m => m.IDCLIENT == Projet.IDCLIENT);
            try
            {

                if (ModelState.IsValid)
                {

                    var y = bd.projet.Add(Projet);
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

        // GET: Projet/Edit/5
        public ActionResult Edit(int? id)
        {
           
           ViewBag.collaborateur = new SelectList(bd.collaborateur.Where(m => m.TYPECOLLABORATEUR == "Titulaire"), "IDCOLLABORATEUR", "NOM");
           ViewBag.collaborateurpe = new SelectList(bd.collaborateur.Where(m => m.TYPECOLLABORATEUR == "P.E"), "IDCOLLABORATEUR", "NOM");

           List<String> ct=new List<string>();
           var tab = bd.projet.Find(id).collaborateur.Where(m=>m.TYPECOLLABORATEUR=="Titulaire");
           foreach (var cc in tab)
           {
               ct.Add(cc.NOM);
               
           }
           ViewBag.ct = ct;
           List<String> cp = new List<string>();
           var tab2 = bd.projet.Find(id).collaborateur.Where(m => m.TYPECOLLABORATEUR == "P.E");
           foreach (var cc in tab2)
           {
               cp.Add(cc.NOM);

           }
           ViewBag.cp = cp;

            //ViewBag.collaborateur = new MultiSelectList(vv, "IDCOLLABORATEUR", "NOM");
           
            ViewBag.IDCLIENT = new SelectList(bd.client, "IDCLIENT", "ABREVIATION", bd.projet.Find(id).client.IDCLIENT);


            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            projet col = bd.projet.Find(id);

            if (col == null)
                return HttpNotFound();
            return View(col);
        }

        // POST: Projet/Edit/5
        [HttpPost]
        public ActionResult Edit(int? id, [Bind(Exclude = "collaborateur")]projet pro, FormCollection fc)
        {
            ViewBag.collaborateurtitulaire = new MultiSelectList(bd.collaborateur, "IDCOLLABORATEUR", "NOM");
           
            ViewBag.IDCLIENT = new SelectList(bd.client, "IDCLIENT", "ABREVIATION");

            projet p = bd.projet.Find(id);
            p.IDPROJET = (int)id;
            p.NOMPROJET = pro.NOMPROJET;
            p.DATEDEBUT = pro.DATEDEBUT;
            p.DATEFIN = pro.DATEFIN;
            p.TYPE = pro.TYPE;
            p.IDCLIENT = pro.IDCLIENT;
            p.client = bd.client.Single(m => m.IDCLIENT == pro.IDCLIENT);
            p.FLAGTYPE = pro.FLAGTYPE;
            var testid = fc["collaborateur"];
            string[] testids = testid.Split(',');
            p.collaborateur.Clear();
            foreach (var item in testids)
            {
                int b = int.Parse(item);
                p.collaborateur.Add(bd.collaborateur.Where(m => m.IDCOLLABORATEUR == b).FirstOrDefault());
            }
           

            try
            {
                if (ModelState.IsValid)
                {
                    bd.Entry(p).State = System.Data.Entity.EntityState.Modified;



                    bd.SaveChanges();
                    return RedirectToAction("Index");


                }
                return View(p);
            }
            catch
            {
                return View();
            }
        }

        // GET: Projet/Delete/5
        public ActionResult Delete(int? id)
        {
            projet bb = bd.projet.Find(id);
            var x = "";
            if (bb.TYPE == 2) { x = "Régie"; }
            else if (bb.TYPE == 1) { x = "TMA"; }
            else { x = "Forfait"; }

            ViewBag.Type = x;
            var z = "";
            if (bb.FLAGTYPE == true)
            {
                z = "Interne";
            }
            else
            {
                z = "Externe";
            } ViewBag.flag = z;

            ViewBag.datedebut = bb.DATEDEBUT.Value.Day.ToString() + "/" + bb.DATEDEBUT.Value.Month.ToString() + "/" + bb.DATEDEBUT.Value.Year.ToString();
            ViewBag.datedefin = bb.DATEFIN.Value.Day.ToString() + "/" + bb.DATEFIN.Value.Month.ToString() + "/" + bb.DATEFIN.Value.Year.ToString();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            projet colpe = bd.projet.Find(id);
            if (colpe == null)
            {
                return HttpNotFound();
            }
            return View(colpe);
        }

        // POST: Projet/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, projet pro)
        {
            Connection.Open();
            string req = "Delete From administrer Where IDPROJET=" + id+"; Delete From tache Where IDPROJET=" + id;
            MySqlCommand cmd = new MySqlCommand(req, Connection);
           
            cmd.ExecuteNonQuery();
            
           
            
            try
            {
                projet colpe = new projet();

                //if (ModelState.IsValid)
                //{
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    colpe = bd.projet.Find(id);

                    if (colpe == null)
                        return HttpNotFound();


                    bd.projet.Remove(colpe);
                

                    bd.SaveChanges();
                    return RedirectToAction("Index");

                //} return View(colpe);
            }

            catch(Exception ex){
                ex.ToString();
                return View();
            }
        }
    }
}
