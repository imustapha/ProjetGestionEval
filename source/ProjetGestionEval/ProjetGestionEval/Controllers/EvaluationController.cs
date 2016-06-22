using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ProjetGestionEval.Controllers
{
    public class EvaluationController : Controller
    {
        public static string[][] chartRes = new string[3][];
        public static int max = 0;
        static string ConnectionString = "database=bd_gestion;server=localhost;uid=root";
        MySqlConnection Connection = new MySqlConnection(ConnectionString);
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
            ViewBag.id = id;
            
            string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "Aout", "September", "Octore", "Novemre", "Decemre" };
            Connection.Open();
            for (int i = 0; i < 3; i++)
            {chartRes[i] = new string[12];}
            MySqlCommand cmd = new MySqlCommand("SELECT month(t.DATEFINTACHE),Year(t.DATEFINTACHE),COUNT(*) tache from tache t where IdCollaborateur = @col and t.DATEFINTACHE between(select DateEvaluation from Evaluation where IdCollaborateur = @col) and (select DateNextEvaluation from Evaluation where IdCollaborateur = @col) group by Year(t.DATEFINTACHE),month(t.DATEFINTACHE)", Connection);
            cmd.Parameters.AddWithValue("@col", id);
            MySqlCommand cmd1 = new MySqlCommand("SELECT month(p.DATEFIN),Year(p.DATEFIN),COUNT(*) from projet p where Idprojet in (select Idprojet from administrer where IdCollaborateur = @col) and p.DATEFIN between(select DateEvaluation from Evaluation where IdCollaborateur = @col) and (select DateNextEvaluation from Evaluation where IdCollaborateur = @col)  group by Year(p.DATEFIN),month(p.DATEFIN)", Connection);
            cmd1.Parameters.AddWithValue("@col", id);
            MySqlDataReader mdr = cmd.ExecuteReader();
            while(mdr.Read())
            {

                chartRes[0][int.Parse(mdr[0].ToString()) - 1] = months[int.Parse(mdr[0].ToString()) - 1];//mdr[0].ToString();
                chartRes[1][int.Parse(mdr[0].ToString()) - 1] = mdr[2].ToString();

            }
            string tst = null, tst1 = null;
            mdr.Close();
            cmd.Dispose();
            mdr = cmd1.ExecuteReader();
            while (mdr.Read())
            {
                tst = mdr[0].ToString();
                tst1 = mdr[1].ToString();
                if (chartRes[0][int.Parse(mdr[0].ToString()) - 1] == null) chartRes[0][int.Parse(mdr[0].ToString()) - 1] = months[int.Parse(mdr[0].ToString()) - 1];
                chartRes[2][int.Parse(mdr[0].ToString()) - 1] = mdr[2].ToString();

            }
            for (int i = 0; i < 12;i++)
            {
                if (chartRes[0][i] == null) chartRes[0][i] =  months[i];
                if (chartRes[1][i] == null) chartRes[1][i] = (0).ToString();
                if (chartRes[2][i] == null) chartRes[2][i] = (0).ToString();
            }
            return View(bd.evaluation.Where(e => e.IDCOLLABORATEUR == id).OrderByDescending(e => e.DATEEVALUATION).First());
        }
        public ActionResult getData()
        {
            for (int i = 1;i<3;i++)
            {
                for (int y = 0; y < 12; y++)
                {
                    if (int.Parse(chartRes[i][y]) > max) max = int.Parse(chartRes[i][y]);
                }
            }
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            var resultData = new
            {
                Content = chartRes,
                maxVal = max
            };
            var result = new ContentResult
            {
                Content = serializer.Serialize(resultData),
                ContentType = "application/json"
            };

            return result;
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
