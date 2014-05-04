using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;

namespace MvcApplication1.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/
        IProductRepository productPool;

        public ProductController(IProductRepository pp)
        {
            productPool = pp;
        }

        public ActionResult ViewAll()
        {

            List<Product> pl = productPool.getAll();
            return View(pl);
        }
        
        public ActionResult ViewProduct(int id)
        {
            Product p = productPool.get(id);
            ViewBag.Company = productPool.getCompany(p.CompanyId);
            return View(p);
        }

        public ActionResult Search(string search)
        {
            ViewBag.term = search;
            return View();
        }
        public JsonResult SearchJson(string search)
        {
            List<Product> pl = productPool.search(search);
            return Json(pl.Select(x=>new { Id = x.Id,Name=x.Name}), JsonRequestBehavior.AllowGet);
        }

    }
}
