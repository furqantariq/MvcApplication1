using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class CartController : Controller
    {
        //
        // GET: /Cart/
       IProductRepository prodPool;
        public CartController(IProductRepository ip)
        {
             prodPool = ip;
        }
        public ActionResult Index()
        {
            if (Session["User"] == null) return RedirectToAction("Authenticate","Account");
            if (Session["Cart"] == null)
                Session["Cart"] = new List<Product>();   
            return View(Session["Cart"]);
        }
        public ActionResult Clear()
        {
            if (Session["User"] == null) return RedirectToAction("Authenticate", "Account");
            Session["Cart"] = null;
            return RedirectToAction("Index");
        }
        public ActionResult AddCart(int id)
        {

            if (Session["User"] == null) return RedirectToAction("Authenticate", "Account");
            if (Session["Cart"] == null)
                Session["Cart"] = new List<Product>();               
    
            ((List<Product>)Session["Cart"]).Add(prodPool.get(id));
            return RedirectToAction("ViewAll","Product");
        }
    }
}
