using ProjetGestionEval;
using ProjetGestionEval.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MySql.AspNet.Identity;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Owin;

namespace ProjetGestionEval.Controllers
{
    public class CollaborateurController : Controller
    {
        private ApplicationUserManager _userManager;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, await user.GenerateUserIdentityAsync(UserManager));
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        bd_gestionEntities bd = new bd_gestionEntities();
        // GET: Collaborateur
        public ActionResult Index()
        {
            return View(bd.collaborateur.ToList());
        }

        // GET: Collaborateur/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Collaborateur/Create
        public ActionResult Create()
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
            return View();
        }

        // POST: Collaborateur/Create
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Exclude = "DATESORTIE,IdUser")]RegisterViewModel model)
        {
            ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
            if (model.TYPECOLLABORATEUR == "Periode d'esseai" || model.TYPECOLLABORATEUR == "Freelance")
                model.FLAGEVAL = false;
            try
            {
                // Convertir l'image
                if (model.File.ContentLength > (2 * 1024 * 1024))
                {
                    ModelState.AddModelError("CustomError", "File est plus 2mb");
                    return View();
                }
                if (!(model.File.ContentType == "image/jpeg" || model.File.ContentType == "image/gif"))
                {
                    ModelState.AddModelError("CustomError", "File alloued for jpeg and gif");
                    return View();

                }
                byte[] data = new byte[model.File.ContentLength];
                model.File.InputStream.Read(data, 0, model.File.ContentLength);
                model.IMAGE = data;
                //formulaire methode poste
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
                    var resulte = await UserManager.CreateAsync(user,model.Password);
                        //await UserManager.CreateAsync(user, model.Password);

                    var userToInsert = bd.aspnetusers.Where(i => i.UserName == user.UserName).FirstOrDefault();
                    if (userToInsert != null)
                    {

                        var CollT = new collaborateur() { IdUser = userToInsert.Id, NOM = model.NOM, PRENOM = model.PRENOM, IDFONCTION = model.IDFONCTION, IMAGE = model.IMAGE, FLAGEVAL = model.FLAGEVAL,DATEEMBAUCHE=model.DATEEMBAUCHE,DATESORTIE=model.DATESORTIE,TYPECOLLABORATEUR=model.TYPECOLLABORATEUR };
                        if (resulte.Succeeded)
                        {
                            //var currentUser = UserManager.FindByName(user.UserName);
                            //if (model.FLAGEVAL == true)
                            //{ 
                            //    var roleresult = UserManager.AddToRole(currentUser.Id, "superuser"); }
                            //else { var roleresult = UserManager.AddToRole(currentUser.Id, "viewt"); }
                            //await SignInAsync(user, isPersistent: false);
                            bd.collaborateur.Add(CollT);
                            bd.SaveChanges();
                            return RedirectToAction("Index");

                        }
                    }

                }
                else if (!ModelState.IsValid)
                {

                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.IDFONCTION = new SelectList(bd.fonction, "IDFONCTION", "NOMFONCTION");
                string tst = ex.Message;
                return View();
            }

            return View();
        }

        // GET: Collaborateur/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Collaborateur/Edit/5
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

        // GET: Collaborateur/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            collaborateur CollT = bd.collaborateur.Find(id);
            if (CollT == null)
            {
                return HttpNotFound();
            }
            return View(CollT);
        }

        // POST: Collaborateur/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, collaborateur Coll)
        {
            try
            {
                collaborateur CollT = new collaborateur();
                aspnetusers asps = new aspnetusers();
                // TODO: Add delete logic here
                if (ModelState.IsValid)
                {
                    if (id == null)
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    CollT = bd.collaborateur.Find(id);
                    asps = bd.aspnetusers.Find(Coll.IdUser);
                    if (CollT == null && asps == null)
                        return HttpNotFound();
                    bd.collaborateur.Remove(CollT);
                    bd.aspnetusers.Remove(asps);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(CollT);
            }
            catch
            {
                return View();
            }
        }
    }
}