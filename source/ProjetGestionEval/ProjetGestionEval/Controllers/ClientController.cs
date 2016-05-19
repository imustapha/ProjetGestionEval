using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjetGestionEval.Controllers
{
    public class ClientController : Controller
    {

        bd_gestionEntities bd = new bd_gestionEntities();
        // GET: Client
        public ActionResult Index()
        {
            return View(bd.client.ToList());
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(client cl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bd.client.Add(cl);
                    bd.SaveChanges();
                    return RedirectToAction("Index");

                } return View(cl);
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            client cl = bd.client.Find(id);
            if (cl == null)
                return HttpNotFound();
            return View(cl);
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(client cl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bd.Entry(cl).State = System.Data.Entity.EntityState.Modified;
                    bd.SaveChanges();
                    return RedirectToAction("Index");


                }
                return View(cl);
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            client cl = bd.client.Find(id);
            if (cl == null)
            {
                return HttpNotFound();
            }
            return View(cl);
        }

        // POST: Client/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, client clt)
        {
            try
            {
                client cl = new client();
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

                    cl = bd.client.Find(id);
                    if (cl == null)
                        return HttpNotFound();


                    bd.client.Remove(cl);
                    bd.SaveChanges();
                    return RedirectToAction("Index");

                } return View(cl);


            }
            catch
            {
                return View();
            }
        }
    }
}
