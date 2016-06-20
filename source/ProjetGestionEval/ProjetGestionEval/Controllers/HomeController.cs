using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class HomeController : Controller
    {
        bd_gestionEntities bd = new bd_gestionEntities();
        static string ConnectionString = "database=bd_gestion;server=localhost;uid=root";
        MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public ActionResult Accueil()
        {
         var asp = bd.aspnetusers.Where(i => i.Email == User.Identity.Name).FirstOrDefault();
         var ctn = bd.collaborateur.Single(m => m.IdUser == asp.Id);
         var id = ctn.IDCOLLABORATEUR;

            Connection.Open();
            string req = "SELECT (SELECT COUNT(*) FROM collaborateur where TYPECOLLABORATEUR='P.E'), (SELECT COUNT(*) FROM collaborateur where TYPECOLLABORATEUR='Titulaire'),(SELECT COUNT(*) FROM projet where IDPROJET IN (SELECT IDPROJET FROM administrer where IDCOLLABORATEUR=" + id + ")),(SELECT COUNT(*) FROM tache where IDCOLLABORATEUR=" + id + "),(SELECT COUNT(*) FROM client)";
            MySqlCommand cmd = new MySqlCommand(req, Connection);
            MySqlDataReader dr = cmd.ExecuteReader();
            string s1 = null, s2 = null,s3=null,s4=null,s5=null;
            while (dr.Read())
            {
                s1 = dr[0].ToString();
                s2 = dr[1].ToString();
                s3 = dr[2].ToString();
                s4 = dr[3].ToString();
                s5 = dr[4].ToString();

            }
            ViewBag.cc = s2;
            ViewBag.bb = s1;
            ViewBag.pp = s3;
            ViewBag.tt = s4;
            ViewBag.aa = s5;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}