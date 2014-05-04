using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /User/
        IUserRepository userPool;

        public AccountController(IUserRepository iu)
        {
            userPool = iu;
        }

        public ActionResult Authenticate()
        {
            if (Session["User"] != null || Session["Company"] != null)
                return HttpNotFound();

            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(Account user)
        {
            if (!ModelState.IsValidField("Username") || !ModelState.IsValidField("Password")) return View();
            Account u = userPool.isUserExist(user);
            if(u!=null)
            {
                Session["User"] = u;
                return RedirectToAction("Profile");
            }
            ViewBag.Error = "No Username/Password Exists";
            return View();
        }

        public ActionResult Create()
        {
            if (Session["User"] != null || Session["Company"] != null)
                return HttpNotFound();

            return View();
        }

        [HttpPost]
        public ActionResult Create(Account user)
        {
            if (ModelState.IsValid && userPool.check(user.Username))
            {
                user = userPool.Add(user);
                Session["User"] = user;
                return RedirectToAction("Profile");
            }
            else
            {
                ViewBag.Error = "Error";
                return View();
            }
        }

        public ActionResult Profile()
        {
            if (Session["User"] == null)
                return RedirectToAction("Authenticate");

            Account user =(Account) Session["User"];
            return View(user);
        }

        public ActionResult Edit()
        {
            if (Session["User"] == null)
                return HttpNotFound();
         
            return View(Session["User"]);
        }

        [HttpPost]
        public ActionResult Edit(Account user)
        {
            var u =userPool.ChangeUser(user);
            Session["User"] = u;
            return RedirectToAction("Profile");
        }
   
        public JsonResult CheckUsername(String username)
        {
            if(!userPool.check(username) || username=="")
                return this.Json(false,JsonRequestBehavior.AllowGet);
            else
                return this.Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
