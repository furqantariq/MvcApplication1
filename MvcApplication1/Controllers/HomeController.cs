using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        IProductRepository prodPool;

        public HomeController(IProductRepository ip)
        {
            prodPool = ip;
        }

        public ActionResult Index()
        {
             return View();
        }
        public JsonResult getRecent()
        {
            return Json(prodPool.getRecent().Select(x => new { id=x.Id,name=x.Name,price=x.Price}), JsonRequestBehavior.AllowGet);
        }
        public JsonResult getBrands()
        {
               return Json(prodPool.getBrands().Select(x => new { id = x.Id, name = x.Name }), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
           Session["Company"] = null;
            return RedirectToAction("Index");
        }
    }
}
