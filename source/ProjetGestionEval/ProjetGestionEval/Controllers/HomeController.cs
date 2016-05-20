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
        static string ConnectionString = "database=bd_gestion;server=localhost;uid=root";
        MySqlConnection Connection = new MySqlConnection(ConnectionString);
        public ActionResult Accueil()
        {

            //Connection.Open();
            //string req = "SELECT (SELECT COUNT(*) FROM collaborateurpe), (SELECT COUNT(*) FROM collaborateurtitulaire)";
            //MySqlCommand cmd = new MySqlCommand(req, Connection);
            //MySqlDataReader dr = cmd.ExecuteReader();
            //string s1 = null, s2 = null;
            //while (dr.Read())
            //{
            //    s1 = dr[0].ToString();
            //    s2 = dr[1].ToString();
            //}




            //bd_evaluationEntities3 bd = new bd_evaluationEntities3();
            //ViewBag.cc = s2;
            //ViewBag.bb = s1;
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